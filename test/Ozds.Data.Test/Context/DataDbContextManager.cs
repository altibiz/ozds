using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;

namespace Ozds.Data.Test.Context;

public sealed class DataDbContextManager(
  IDbContextFactory<DataDbContext> factory,
  ILogger<EphemeralDataDbContextManager> logger
) : IAsyncDisposable
{
  private readonly SemaphoreSlim _semaphore = new(1, 1);

  private DataDbContext? context;

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

      await context.DisposeAsync();
    }
    finally
    {
      _semaphore.Release();
      _semaphore.Dispose();
    }
  }

  public async Task<DataDbContext> GetContext(
    CancellationToken cancellationToken = default
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
        break;
      }
      catch (Exception exception)
      {
        logger.LogError(exception, "Failed to get context");
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
}
