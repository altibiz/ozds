using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Reflection;

namespace Ozds.Data.Interceptors;

public class AuditingInterceptor(IServiceProvider serviceProvider)
  : EntityChangesInterceptor(serviceProvider)
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
    var baseResult = base.SavingChanges(eventData, result);

    var context = eventData.Context;
    if (context is null)
    {
      return baseResult;
    }

    var state = State(context);
    Audit(state);

    return baseResult;
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    var baseResult = await base
      .SavingChangesAsync(eventData, result, cancellationToken);

    var context = eventData.Context;
    if (context is null)
    {
      return baseResult;
    }

    var state = AsyncState(context);
    Audit(state);

    return baseResult;
  }

  public override int SavedChanges(
    SaveChangesCompletedEventData eventData,
    int result
  )
  {
    var context = eventData.Context;
    if (context is null)
    {
      return base.SavedChanges(eventData, result);
    }

    var state = State(context);
    Task.Run(
      () => Audited(serviceProvider, state, CancellationToken.None),
      CancellationToken.None);

    return base.SavedChanges(eventData, result);
  }

  public override ValueTask<int> SavedChangesAsync(
    SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default
  )
  {
    var context = eventData.Context;
    if (context is null)
    {
      return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    var state = AsyncState(context);
    Task.Run(
      () => Audited(serviceProvider, state, CancellationToken.None),
      CancellationToken.None);

    return base.SavedChangesAsync(eventData, result, cancellationToken);
  }

  private static void Audit(EntityChangesInterceptorState state)
  {
    foreach (var entry in state.Entries)
    {
      if (entry.Entity is IAuditableEntity auditable)
      {
        var representativeId = auditable.AuditingRepresentativeId;
        if (entry.State is EntityState.Added)
        {
          if (
            auditable is ITrackableEntity trackableRestoring
            && trackableRestoring.Restore && trackableRestoring.IsDeleted)
          {
            entry.Original.State =
              Microsoft.EntityFrameworkCore.EntityState.Modified;
            trackableRestoring.IsDeleted = false;
            trackableRestoring.DeletedOn = null;
            trackableRestoring.DeletedById = null;
          }
          else
          {
            auditable.CreatedOn = state.Now;
            if (representativeId is not null)
            {
              auditable.CreatedById = representativeId;
            }
            else
            {
              auditable.CreatedById = null;
            }
          }
        }
        else if (entry.State is EntityState.Modified
          && auditable is ITrackableEntity trackableModifying)
        {
          trackableModifying.LastUpdatedOn = state.Now;
          if (representativeId is not null)
          {
            trackableModifying.LastUpdatedById = representativeId;
          }
          else
          {
            trackableModifying.LastUpdatedById = null;
          }
        }
        else if (entry.State is EntityState.Deleted
          && auditable is ITrackableEntity trackableDeleting
          && !(trackableDeleting.Forget || trackableDeleting.IsDeleted))
        {
          entry.Original.State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
          trackableDeleting.IsDeleted = true;
          trackableDeleting.DeletedOn = state.Now;
          if (representativeId is not null)
          {
            trackableDeleting.DeletedById = representativeId;
          }
          else
          {
            trackableDeleting.DeletedById = null;
          }
        }
      }

      if (entry.Entity is IFinancialEntity financial)
      {
        var representativeId = financial.AuditingRepresentativeId;
        if (entry.State is EntityState.Added)
        {
          financial.IssuedOn = state.Now;
          financial.IssuedById = representativeId;
        }
      }

      if (entry.Entity is IResolvableNotificationEntity resolvableNotification)
      {
        var representativeId = resolvableNotification.AuditingRepresentativeId;
        if (entry.State is EntityState.Modified)
        {
          resolvableNotification.ResolvedById = representativeId;
          resolvableNotification.ResolvedOn = state.Now;
        }
      }
    }
  }

  private static async Task Audited(
    IServiceProvider serviceProvider,
    EntityChangesInterceptorState state,
    CancellationToken cancellationToken
  )
  {
    var entityReflector = serviceProvider
      .GetRequiredService<EntityReflector>();

    var factory = serviceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>();

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    foreach (var entry in state.Entries)
    {
      if (entry.Entity is IAuditableEntity auditable
        && auditable is not ITrackableEntity)
      {
        if (entry.State is EntityState.Added)
        {
          context.Add(
            CreateEvent(
              entityReflector,
              entry,
              auditable,
              AuditEntity.Creation,
              state.Now));
        }
        else if (entry.State is EntityState.Deleted)
        {
          context.Add(
            CreateEvent(
              entityReflector,
              entry,
              auditable,
              AuditEntity.Deletion,
              state.Now));
        }
      }

      if (entry.Entity is ITrackableEntity trackable)
      {
        if (entry.State is EntityState.Added)
        {
          if (trackable.Restore)
          {
            context.Add(
              CreateEvent(
                entityReflector,
                entry,
                trackable,
                AuditEntity.Restoration,
                state.Now));
          }
          else
          {
            context.Add(
              CreateEvent(
                entityReflector,
                entry,
                trackable,
                AuditEntity.Creation,
                state.Now));
          }
        }
        else if (entry.State is EntityState.Modified)
        {
          context.Add(
            CreateEvent(
              entityReflector,
              entry,
              trackable,
              AuditEntity.Modification,
              state.Now));
        }
        else if (entry.State is EntityState.Deleted)
        {
          if (trackable.Forget)
          {
            context.Add(
              CreateEvent(
                entityReflector,
                entry,
                trackable,
                AuditEntity.Forgetting,
                state.Now));
          }
          else
          {
            context.Add(
              CreateEvent(
                entityReflector,
                entry,
                trackable,
                AuditEntity.Deletion,
                state.Now));
          }
        }
      }
    }

    if (context.ChangeTracker.Entries().Any())
    {
      await context.SaveChangesAsync(cancellationToken);
    }
  }

  private static IAuditEventEntity CreateEvent(
    EntityReflector entityReflector,
    EntityChangesEntry entry,
    IAuditableEntity auditable,
    AuditEntity audit,
    DateTimeOffset now
  )
  {
    var type = audit switch
    {
      AuditEntity.Creation => "Created",
      AuditEntity.Modification => "Modified",
      AuditEntity.Deletion => "Deleted",
      AuditEntity.Restoration => "Restored",
      AuditEntity.Forgetting => "Forgotten",
      _ => throw new ArgumentOutOfRangeException(nameof(audit))
    };

    var content = new AuditContent(
      type,
      entry.Properties
    );

    // NOTE: https://stackoverflow.com/a/73048230
    var contentJson = JsonSerializer
      .SerializeToNode(content)!
      .Deserialize<JsonDocument>()!;

    var representativeId = auditable.AuditingRepresentativeId;

    if (representativeId is not null)
    {
      return new RepresentativeAuditEventEntity
      {
        Timestamp = now,
        Title =
          $"{type} {auditable.GetType().Name} {auditable.AuditingTitle}",
        RepresentativeId = representativeId,
        Level = LevelEntity.Debug,
        Audit = audit,
        Content = contentJson,
        AuditableEntityId = auditable.AuditingId,
        AuditableEntityType = entityReflector
            .ResolveEntityName(auditable.GetType())
          ?? throw new InvalidOperationException(
            $"No type name found for {auditable.GetType()}"),
        AuditableEntityTable = entityReflector
            .ResolveEntityTable(auditable.GetType())
          ?? throw new InvalidOperationException(
            $"No table found for {auditable.GetType()}"),
        Categories = [CategoryEntity.All, CategoryEntity.Audit]
      };
    }

    return new SystemAuditEventEntity
    {
      Timestamp = now,
      Title =
        $"{type} {auditable.GetType().Name} {auditable.AuditingTitle}",
      Level = LevelEntity.Debug,
      Audit = audit,
      Content = contentJson,
      AuditableEntityId = auditable.AuditingId,
      AuditableEntityType = entityReflector
          .ResolveEntityName(auditable.GetType())
        ?? throw new InvalidOperationException(
          $"No type name found for {auditable.GetType()}"),
      AuditableEntityTable = entityReflector
          .ResolveEntityTable(auditable.GetType())
        ?? throw new InvalidOperationException(
          $"No table found for {auditable.GetType()}"),
      Categories = [CategoryEntity.All, CategoryEntity.Audit]
    };
  }

  private sealed record AuditContent(string Type, EntityProperty[] Properties);
}
