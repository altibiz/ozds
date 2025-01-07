using Microsoft.EntityFrameworkCore;
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

    foreach (var entity in context.Model.GetEntityTypes())
    {
#pragma warning disable EF1002 // Risk of vulnerability to SQL injection.
      await context.Database.ExecuteSqlRawAsync(
        $"DELETE FROM {entity.GetTableName()} WHERE TRUE;",
        cancellationToken);
#pragma warning restore EF1002 // Risk of vulnerability to SQL injection.
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
}
