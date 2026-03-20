using System.Collections.Concurrent;
using Ozds.Caching.Observers.Abstractions;
using Ozds.Caching.Observers.EventArgs;

namespace Ozds.Caching.Test.Services;

public class TestReactorDrainService : IDisposable
{
  private readonly ConcurrentQueue<(TaskCompletionSource tcs, long target)> waiters =
    new();
  private readonly ICacheSubscriber subscriber;

  private long published = 0;
  private long processed = 0;
  private bool disposed = false;

  private void OnPublished(object? sender, CacheEventArgs _)
  {
    if (Volatile.Read(ref disposed))
    {
      return;
    }

    Interlocked.Increment(ref published);
  }

  public TestReactorDrainService(ICacheSubscriber subscriber)
  {
    this.subscriber = subscriber;
    subscriber.Subscribe(OnPublished);
  }

  public void NotifyProcessed()
  {
    if (Volatile.Read(ref disposed))
    {
      return;
    }

    var current = Interlocked.Increment(ref processed);
    while (
      waiters.TryPeek(out var entry)
      && current >= entry.target
      && waiters.TryDequeue(out var waiter))
    {
      waiter.tcs.TrySetResult();
    }
  }

  public Task DrainAsync(CancellationToken cancellationToken)
  {
    ObjectDisposedException.ThrowIf(
      Volatile.Read(ref disposed),
      nameof(TestReactorDrainService)
    );

    var target = Volatile.Read(ref published);
    if (Volatile.Read(ref processed) >= target)
    {
      return Task.CompletedTask;
    }

    var tcs = new TaskCompletionSource(
      TaskCreationOptions.RunContinuationsAsynchronously
    );
    waiters.Enqueue((tcs, target));

    if (Volatile.Read(ref processed) >= target
      && waiters.TryDequeue(out var w))
    {
      w.tcs.TrySetResult();
    }

    return tcs.Task.WaitAsync(cancellationToken);
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (!disposing)
    {
      return;
    }

    if (!Interlocked.Exchange(ref disposed, true))
    {
      return;
    }

    subscriber.Unsubscribe(OnPublished);

    while (waiters.TryDequeue(out var waiter))
    {
      waiter.tcs.TrySetCanceled();
    }
  }
}
