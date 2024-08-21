using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

// TODO: audit db errors and such
// TODO: check if user is representative

namespace Ozds.Business.Interceptors;

public class AuditingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
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
            eventData.Context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Created {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Creation,
                Content = CreateAddedMessage(auditable)
              });
          }
        }
        else if (isDevelopment)
        {
          auditable.Entity.CreatedBy = null;
          eventData.Context.Add(
            new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Created {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Creation,
              Content = CreateAddedMessage(auditable)
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
            eventData.Context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Updated {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Modification,
                Content = CreateModifiedMessage(auditable)
              });
          }
        }
        else
        {
          auditable.Entity.LastUpdatedBy = null;
          if (isDevelopment)
          {
            eventData.Context.Add(
              new SystemAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Updated {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Modification,
                Content = CreateModifiedMessage(auditable)
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
            eventData.Context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Deleted {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Deletion,
                Content = CreateDeletedMessage(auditable)
              });
          }
        }
        else
        {
          auditable.Entity.DeletedById = null;
          if (isDevelopment)
          {
            eventData.Context.Add(
              new SystemAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Deleted {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Deletion,
                Content = CreateDeletedMessage(auditable)
              });
          }
        }
      }
    }
  }

  private string? GetRepresentativeId()
  {
    if (serviceProvider.GetService<IHttpContextAccessor>() is not null)
    {
      return null;
    }

    return null;
  }

  private bool IsDevelopment()
  {
    return serviceProvider.GetService<IHostEnvironment>() is
    { } hostEnvironment
      && hostEnvironment.IsDevelopment();
  }

  private static JsonObject CreateAddedMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Added");
  }

  private static JsonObject CreateModifiedMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Modified");
  }

  private static JsonObject CreateDeletedMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Deleted");
  }

  private static JsonObject CreateMessage(EntityEntry entry, string type)
  {
    var properties = entry.Properties
      .Where(property => property.OriginalValue != property.CurrentValue)
      .Select(property => new AuditProperty(
        property.Metadata.Name,
        property.OriginalValue?.ToString(),
        property.CurrentValue?.ToString()
      ))
      .ToArray();

    var message = new AuditMessage(
      type,
      properties
    );

    return (JsonObject)JsonSerializer.SerializeToNode(message)!;
  }

  private sealed record AuditMessage(string Type, AuditProperty[] Properties);

  private sealed record AuditProperty(string Name, string? OldValue, string? NewValue);
}
