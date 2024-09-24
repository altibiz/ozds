using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

// TODO: audit db errors and such

namespace Ozds.Data.Interceptors;

public class AuditingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override int Order
  {
    get { return 20; }
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    Audit(eventData);
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    Audit(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public void Audit(DbContextEventData eventData)
  {
    if (eventData.Context is null)
    {
      return;
    }

    var representativeId = GetRepresentativeId();
    var now = DateTimeOffset.UtcNow;

    eventData.Context.ChangeTracker.DetectChanges();
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
          eventData.Context.Add(
            new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Created {
                  auditable.Entity.GetType().Name
                } {
                  auditable.Entity.Title
                }",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Creation,
              Content = CreateAddedMessage(auditable),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
        }
        else
        {
          auditable.Entity.CreatedBy = null;
          eventData.Context.Add(
            new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Created {
                  auditable.Entity.GetType().Name
                } {
                  auditable.Entity.Title
                }",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Creation,
              Content = CreateAddedMessage(auditable),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
        }
      }

      if (auditable.State is EntityState.Modified)
      {
        auditable.Entity.LastUpdatedOn = now;
        if (representativeId is not null)
        {
          auditable.Entity.LastUpdatedById = representativeId;
          eventData.Context.Add(
            new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Updated {
                  auditable.Entity.GetType().Name
                } {
                  auditable.Entity.Title
                }",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Content = CreateModifiedMessage(auditable),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
        }
        else
        {
          auditable.Entity.LastUpdatedBy = null;
          eventData.Context.Add(
            new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Updated {
                  auditable.Entity.GetType().Name
                } {
                  auditable.Entity.Title
                }",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Content = CreateModifiedMessage(auditable),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
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
          eventData.Context.Add(
            new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Deleted {
                  auditable.Entity.GetType().Name
                } {
                  auditable.Entity.Title
                }",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Deletion,
              Content = CreateDeletedMessage(auditable),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
        }
        else
        {
          auditable.Entity.DeletedById = null;
          eventData.Context.Add(
            new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Deleted {
                  auditable.Entity.GetType().Name
                } {
                  auditable.Entity.Title
                }",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Deletion,
              Content = CreateDeletedMessage(auditable),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
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

  private static JsonDocument CreateAddedMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Added");
  }

  private static JsonDocument CreateModifiedMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Modified");
  }

  private static JsonDocument CreateDeletedMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Deleted");
  }

  private static JsonDocument CreateMessage(EntityEntry entry, string type)
  {
    var properties = entry.Properties
      .Where(property => property.OriginalValue != property.CurrentValue)
      .Select(
        property => new AuditProperty(
          property.Metadata.Name,
          property.OriginalValue?.ToString(),
          property.CurrentValue?.ToString()
        ))
      .ToArray();

    var message = new AuditMessage(
      type,
      properties
    );

    var node = JsonSerializer.SerializeToNode(message)!;

    // NOTE: https://stackoverflow.com/a/73048230
    return node.Deserialize<JsonDocument>()!;
  }

  private sealed record AuditMessage(string Type, AuditProperty[] Properties);

  private sealed record AuditProperty(
    string Name,
    string? OldValue,
    string? NewValue);
}
