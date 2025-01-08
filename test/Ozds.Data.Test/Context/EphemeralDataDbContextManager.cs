using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Context;
using Ozds.Data.Extensions;

namespace Ozds.Data.Test.Context;

public sealed class EphemeralDataDbContextManager(
  IDbContextFactory<DataDbContext> factory,
  ILogger<EphemeralDataDbContextManager> logger
) : IAsyncDisposable
{
  private readonly SemaphoreSlim _semaphore = new(1, 1);

  private DataDbContext? context;

  public async Task<DataDbContext> GetContext(
    CancellationToken cancellationToken
  )
  {
    try
    {
      await _semaphore.WaitAsync(cancellationToken);
    }
    catch (ObjectDisposedException)
    {
      throw new ObjectDisposedException(
        "The context manager has been disposed.");
    }

    if (context is not null)
    {
      return context;
    }

    while (true)
    {
      try
      {
        context = await factory.CreateDbContextAsync(cancellationToken);
        await context.Database.BeginTransactionAsync(cancellationToken);
        break;
      }
      catch (Exception exception)
      {
        logger.LogError(exception, "Failed to get context");
      }
    }

    var entityTypes = GetDependencyOrderedEntityTypes(context);
    foreach (var entity in entityTypes)
    {
      while (true)
      {
        try
        {
#pragma warning disable EF1002 // Risk of vulnerability to SQL injection.
          await context.Database.ExecuteSqlRawAsync(
            $"DELETE FROM {entity.GetTableName()} WHERE TRUE;",
            cancellationToken);
#pragma warning restore EF1002 // Risk of vulnerability to SQL injection.
        }
        catch (Exception ex)
        {
          if (ex.InnerException is TimeoutException)
          {
            await Task.Delay(
              Random.Shared.Next(100, 1000),
              cancellationToken
            );
            continue;
          }

          throw;
        }
      }
    }

    try
    {
      _semaphore.Release();
    }
    catch (ObjectDisposedException)
    {
      throw new ObjectDisposedException(
        "The context manager has been disposed.");
    }

    return context;
  }

  public async ValueTask DisposeAsync()
  {
    try
    {
      await _semaphore.WaitAsync();
    }
    catch (ObjectDisposedException)
    {
      return;
    }

    try
    {
      if (context is null)
      {
        return;
      }

      await context.Database.RollbackTransactionAsync();
      await context.DisposeAsync();
    }
    finally
    {
      _semaphore.Release();
      _semaphore.Dispose();
    }
  }

  private static List<IEntityType> GetDependencyOrderedEntityTypes(
    DbContext context
  )
  {
    var hierarchies = context.Model
      .GetEntityTypes()
      .Where(entityType => entityType.BaseType is null)
      .Select(entityType => entityType.GetDerivedTypesInclusive().ToList())
      .ToList();
    var reversedEntityTypes = new List<IEntityType>();
    while (hierarchies.Count > 0)
    {
      var newReversedEntityTypes = new List<IEntityType>();
      foreach (var hierarchy in hierarchies.ToList())
      {
        var foreignKeys = hierarchy
          .SelectMany(entityType => entityType.GetForeignKeys())
          .ToList();
        if (foreignKeys.Count == 0 ||
          foreignKeys
            .TrueForAll(foreignKey =>
              reversedEntityTypes
                .Exists(reversedEntityType =>
                  foreignKey.PrincipalEntityType == reversedEntityType)))
        {
          hierarchies.Remove(hierarchy);
          newReversedEntityTypes.AddRange(hierarchy);
        }
      }
      if (newReversedEntityTypes.Count == 0)
      {
        foreach (var hierarchy in hierarchies.ToList())
        {
          var foreignKeys = hierarchy
            .SelectMany(entityType => entityType.GetForeignKeys())
            .ToList();
          if (foreignKeys.Count == 0 ||
            foreignKeys
              .TrueForAll(foreignKey =>
                !foreignKey.IsRequired ||
                  reversedEntityTypes
                    .Exists(reversedEntityType =>
                      foreignKey.PrincipalEntityType == reversedEntityType)))
          {
            hierarchies.Remove(hierarchy);
            newReversedEntityTypes.AddRange(hierarchy);
          }
        }
      }
      reversedEntityTypes.AddRange(newReversedEntityTypes);
    }
    reversedEntityTypes.Reverse();
    return reversedEntityTypes;
  }
}
