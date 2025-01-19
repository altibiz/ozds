using System.Security.Claims;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

// TODO: check representative id
// TODO: cascade delete and remove events when forgetting
//       - add interceptor after this one that cascade deletes and events

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
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    var representativeId = GetRepresentativeId();
    var now = DateTimeOffset.UtcNow;

    context.ChangeTracker.DetectChanges();
    var entries = context.ChangeTracker
      .Entries<AuditableEntity>()
      .ToList();

    foreach (var auditable in entries)
    {
      if (auditable.State is EntityState.Added)
      {
        if (auditable.Entity.Restore && auditable.Entity.IsDeleted)
        {
          auditable.State = EntityState.Modified;
          auditable.Entity.IsDeleted = false;
          auditable.Entity.DeletedOn = null;
          auditable.Entity.DeletedById = null;
          if (representativeId is not null)
          {
            context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Restored {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Restoration,
                Content = CreateRestoredMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
          else
          {
            context.Add(
              new SystemAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Restored {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Restoration,
                Content = CreateRestoredMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
        }
        else
        {
          auditable.Entity.CreatedOn = now;
          if (representativeId is not null)
          {
            auditable.Entity.CreatedById = representativeId;
            context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Created {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Creation,
                Content = CreateAddedMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
          else
          {
            auditable.Entity.CreatedById = null;
            context.Add(
              new SystemAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Created {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Creation,
                Content = CreateAddedMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
        }
      }

      if (auditable.State is EntityState.Modified)
      {
        auditable.Entity.LastUpdatedOn = now;
        if (representativeId is not null)
        {
          auditable.Entity.LastUpdatedById = representativeId;
          context.Add(
            new RepresentativeAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Updated {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              RepresentativeId = representativeId,
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Content = CreateModifiedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = context
                .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                ?? throw new InvalidOperationException(
                  $"No type name found for {auditable.Entity.GetType()}"),
              AuditableEntityTable = context
                .GetTableNameFromEntityType(auditable.Entity.GetType())
                ?? throw new InvalidOperationException(
                  $"No table found for {auditable.Entity.GetType()}"),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
        }
        else
        {
          auditable.Entity.LastUpdatedById = null;
          context.Add(
            new SystemAuditEventEntity
            {
              Timestamp = now,
              Title =
                $"Updated {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
              Level = LevelEntity.Debug,
              Audit = AuditEntity.Modification,
              Content = CreateModifiedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = context
                .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                ?? throw new InvalidOperationException(
                  $"No type name found for {auditable.Entity.GetType()}"),
              AuditableEntityTable = context
                .GetTableNameFromEntityType(auditable.Entity.GetType())
                ?? throw new InvalidOperationException(
                  $"No table found for {auditable.Entity.GetType()}"),
              Categories = [CategoryEntity.All, CategoryEntity.Audit]
            });
        }
      }

      if (auditable.State is EntityState.Deleted)
      {
        if (auditable.Entity.Forget || auditable.Entity.IsDeleted)
        {
          if (representativeId is not null)
          {
            context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Forgotten {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Forgetting,
                Content = CreateForgottenMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
          else
          {
            context.Add(
              new SystemAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Forgotten {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Forgetting,
                Content = CreateForgottenMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
        }
        else
        {
          auditable.State = EntityState.Modified;
          auditable.Entity.IsDeleted = true;
          auditable.Entity.DeletedOn = now;
          if (representativeId is not null)
          {
            auditable.Entity.DeletedById = representativeId;
            context.Add(
              new RepresentativeAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Deleted {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                RepresentativeId = representativeId,
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Deletion,
                Content = CreateDeletedMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
          else
          {
            auditable.Entity.DeletedById = null;
            context.Add(
              new SystemAuditEventEntity
              {
                Timestamp = now,
                Title =
                  $"Deleted {auditable.Entity.GetType().Name} {auditable.Entity.Title}",
                Level = LevelEntity.Debug,
                Audit = AuditEntity.Deletion,
                Content = CreateDeletedMessage(auditable),
                AuditableEntityId = auditable.Entity.Id,
                AuditableEntityType = context
                  .GetEntityTypeNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No type name found for {auditable.Entity.GetType()}"),
                AuditableEntityTable = context
                  .GetTableNameFromEntityType(auditable.Entity.GetType())
                  ?? throw new InvalidOperationException(
                    $"No table found for {auditable.Entity.GetType()}"),
                Categories = [CategoryEntity.All, CategoryEntity.Audit]
              });
          }
        }
      }
    }
  }

  private string? GetRepresentativeId()
  {
    if (serviceProvider.GetService<IHttpContextAccessor>() is { } accessor)
    {
      return accessor.HttpContext?.User.FindFirstValue("id");
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

  private static JsonDocument CreateRestoredMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Restored");
  }

  private static JsonDocument CreateForgottenMessage(EntityEntry entry)
  {
    return CreateMessage(entry, "Forgotten");
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
