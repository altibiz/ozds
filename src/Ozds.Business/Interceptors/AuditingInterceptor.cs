using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Interceptors;

public class AuditingInterceptor : ServedSaveChangesInterceptor
{
  public AuditingInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override int Order
  {
    get { return 20; }
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    await Audit(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public async ValueTask Audit(DbContextEventData eventData)
  {
    if (eventData.Context is null)
    {
      return;
    }

    var representativeId = await GetRepresentativeId(eventData);
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
              Description = CreateAddedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = auditable.Entity.GetType().FullName
                ?? throw new InvalidOperationException(
                  "No type name found for auditable entity"),
              AuditableEntityTable = auditable.Metadata.GetTableName()
                ?? throw new InvalidOperationException(
                  "No table name found for auditable entity")
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
              Description = CreateAddedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = auditable.Entity.GetType().FullName
                ?? throw new InvalidOperationException(
                  "No type name found for auditable entity"),
              AuditableEntityTable = auditable.Metadata.GetTableName()
                ?? throw new InvalidOperationException(
                  "No table name found for auditable entity")
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
              Description = CreateModifiedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = auditable.Entity.GetType().FullName
                ?? throw new InvalidOperationException(
                  "No type name found for auditable entity"),
              AuditableEntityTable = auditable.Metadata.GetTableName()
                ?? throw new InvalidOperationException(
                  "No table name found for auditable entity")
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
              Description = CreateModifiedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = auditable.Entity.GetType().FullName
                ?? throw new InvalidOperationException(
                  "No type name found for auditable entity"),
              AuditableEntityTable = auditable.Metadata.GetTableName()
                ?? throw new InvalidOperationException(
                  "No table name found for auditable entity")
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
              Description = CreateDeletedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = auditable.Entity.GetType().FullName
                ?? throw new InvalidOperationException(
                  "No type name found for auditable entity"),
              AuditableEntityTable = auditable.Metadata.GetTableName()
                ?? throw new InvalidOperationException(
                  "No table name found for auditable entity")
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
              Description = CreateDeletedMessage(auditable),
              AuditableEntityId = auditable.Entity.Id,
              AuditableEntityType = auditable.Entity.GetType().FullName
                ?? throw new InvalidOperationException(
                  "No type name found for auditable entity"),
              AuditableEntityTable = auditable.Metadata.GetTableName()
                ?? throw new InvalidOperationException(
                  "No table name found for auditable entity")
            });
        }
      }
    }
  }

  private async Task<string?> GetRepresentativeId(DbContextEventData eventData)
  {
    ClaimsPrincipal? claimsPrincipal = null;
    if (serviceProvider
        .GetService<IHttpContextAccessor>()
        ?.HttpContext is { } httpContext)
    {
      claimsPrincipal = httpContext.User;
    }

    if (claimsPrincipal is null
      && serviceProvider.GetService<AuthenticationStateProvider>()
        is { } authStateProvider)
    {
      claimsPrincipal =
        (await authStateProvider.GetAuthenticationStateAsync()).User;
    }

    if (claimsPrincipal is null)
    {
      return null;
    }

    var id = claimsPrincipal.Claims
      .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(id))
    {
      return null;
    }

    var context = eventData.Context as OzdsDataDbContext;
    if (context is null)
    {
      return null;
    }

    var representative = await context.Representatives
      .Where(context.PrimaryKeyEquals<RepresentativeEntity>(id))
      .FirstOrDefaultAsync();

    return representative?.Id;
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
      .Where(
        property =>
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
