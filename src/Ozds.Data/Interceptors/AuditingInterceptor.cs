using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Interceptors;

public class AuditingInterceptor : ServedSaveChangesInterceptor
{
  public AuditingInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    Audit(eventData);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    Audit(eventData);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public void Audit(DbContextEventData eventData)
  {
    if (eventData.Context is null)
    {
      return;
    }

    var userId = GetUserId();
    var now = DateTimeOffset.UtcNow;

    var entries = eventData.Context.ChangeTracker
      .Entries<AuditableEntity>()
      .ToList();

    foreach (EntityEntry<AuditableEntity> auditable in entries)
    {
      if (auditable.State is EntityState.Added)
      {
        auditable.Entity.CreationDate = now;
        if (userId is not null)
        {
          auditable.Entity.CreatedBy = new RepresentativeEntity { Id = userId };
          if (IsDevelopment())
          {
            eventData.Context.Add(new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Creation,
              Description = CreateAddedMessage(auditable)
            });
          }
        }

      }

      if (auditable.State is EntityState.Modified)
      {
        auditable.Entity.LastUpdateDate = now;
        if (userId is not null)
        {
          auditable.Entity.LastUpdatedBy = new RepresentativeEntity { Id = userId };
          if (IsDevelopment())
          {
            eventData.Context.Add(new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Description = CreateModifiedMessage(auditable)
            });
          }
        }
        else
        {
          auditable.Entity.LastUpdatedBy = null;
        }
      }

      if (auditable.State is EntityState.Deleted)
      {
        auditable.State = EntityState.Modified;
        auditable.Entity.IsDeleted = true;
        auditable.Entity.DeletionDate = now;
        if (userId is not null)
        {
          auditable.Entity.DeletedBy = new RepresentativeEntity { Id = userId };
          if (IsDevelopment())
          {
            eventData.Context.Add(new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Deletion,
              Description = CreateDeletedMessage(auditable)
            });
          }
        }
        else
        {
          auditable.Entity.DeletedBy = null;
        }
      }
    }
  }

  private string? GetUserId()
  {
    if (_serviceProvider.GetService(typeof(IHttpContextAccessor)) is IHttpContextAccessor httpContextAccessor)
    {
      return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    return null;
  }

  private bool IsDevelopment()
  {
    return _serviceProvider.GetService(typeof(IHostEnvironment)) is IHostEnvironment webHostEnvironment
      && webHostEnvironment.IsDevelopment();
  }

  private static string CreateAddedMessage(EntityEntry entry) =>
    entry.Properties.Aggregate(
      $"Inserting {entry.Metadata.DisplayName()} with ",
      (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

  private static string CreateModifiedMessage(EntityEntry entry) =>
    entry.Properties.Where(property => property.IsModified || property.Metadata.IsPrimaryKey()).Aggregate(
      $"Updating {entry.Metadata.DisplayName()} with ",
      (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

  private static string CreateDeletedMessage(EntityEntry entry) =>
    entry.Properties.Where(property => property.Metadata.IsPrimaryKey()).Aggregate(
      $"Deleting {entry.Metadata.DisplayName()} with ",
      (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");
}
