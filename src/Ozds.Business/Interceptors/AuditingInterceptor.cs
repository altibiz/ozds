using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

// TODO: audit db errors and such
// TODO: check if user is representative

namespace Ozds.Business.Interceptors;

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

  public void Audit(DbContextEventData eventData)
  {
    if (eventData.Context is null)
    {
      return;
    }

    var representativeId = GetRepresentativeId();
    var isDevelopment = IsDevelopment();
    var now = DateTimeOffset.UtcNow;

    var entries = eventData.Context.ChangeTracker
      .Entries<AuditableEntity>()
      .ToList();

    foreach (var auditable in entries)
    {
      if (auditable.State is EntityState.Added)
      {
        auditable.Entity.CreatedOn = now;
        if (representativeId is not null)
        {
          auditable.Entity.CreatedById = representativeId;
          if (isDevelopment)
          {
            eventData.Context.Add(new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Created {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Creation,
              Description = CreateAddedMessage(auditable)
            });
          }
        }
        else if (isDevelopment)
        {
          auditable.Entity.CreatedBy = null;
          eventData.Context.Add(new SystemAuditEventEntity
          {
            Timestamp = now,
            Title =
              $"Created {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
            Level = LevelEntity.Debug,
            Audit = AuditEntity.Creation,
            Description = CreateAddedMessage(auditable)
          });
        }
      }

      if (auditable.State is EntityState.Modified)
      {
        auditable.Entity.LastUpdatedOn = now;
        if (representativeId is not null)
        {
          auditable.Entity.LastUpdatedById = representativeId;
          if (isDevelopment)
          {
            eventData.Context.Add(new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Updated {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Description = CreateModifiedMessage(auditable)
            });
          }
        }
        else
        {
          auditable.Entity.LastUpdatedBy = null;
          if (isDevelopment)
          {
            eventData.Context.Add(new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Updated {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Description = CreateModifiedMessage(auditable)
            });
          }
        }
      }

      if (auditable.State is EntityState.Deleted)
      {
        auditable.State = EntityState.Modified;
        auditable.Entity.IsDeleted = true;
        auditable.Entity.DeletedOn = now;
        if (representativeId is not null)
        {
          auditable.Entity.DeletedById = representativeId;
          if (isDevelopment)
          {
            eventData.Context.Add(new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Deleted {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Deletion,
              Description = CreateDeletedMessage(auditable)
            });
          }
        }
        else
        {
          auditable.Entity.DeletedById = null;
          if (isDevelopment)
          {
            eventData.Context.Add(new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Deleted {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Deletion,
              Description = CreateDeletedMessage(auditable)
            });
          }
        }
      }
    }
  }

  private string? GetRepresentativeId()
  {
    if (_serviceProvider.GetService<IHttpContextAccessor>() is not null)
    {
      return null;
      // return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    return null;
  }

  private bool IsDevelopment()
  {
    return _serviceProvider.GetService<IHostEnvironment>() is
    { } hostEnvironment
           && hostEnvironment.IsDevelopment();
  }

  private static string CreateAddedMessage(EntityEntry entry)
  {
    return entry.Properties
      .Aggregate(
        $"Inserting {entry.Metadata.DisplayName()} with ",
        (auditString, property) => auditString +
                                   $"{property.Metadata.Name}: '{property.CurrentValue}' ");
  }

  private static string CreateModifiedMessage(EntityEntry entry)
  {
    return entry.Properties
      .Where(property =>
        property.IsModified || property.Metadata.IsPrimaryKey())
      .Aggregate(
        $"Updating {entry.Metadata.DisplayName()} with ",
        (auditString, property) => auditString +
                                   $"{property.Metadata.Name}: '{property.CurrentValue}' ");
  }

  private static string CreateDeletedMessage(EntityEntry entry)
  {
    return entry.Properties
      .Where(property => property.Metadata.IsPrimaryKey())
      .Aggregate(
        $"Deleting {entry.Metadata.DisplayName()} with ",
        (auditString, property) => auditString +
                                   $"{property.Metadata.Name}: '{property.CurrentValue}' ");
  }
}
