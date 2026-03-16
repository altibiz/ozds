using Ozds.Caching.Observers.Abstractions;
using Ozds.Caching.Observers.EventArgs;

namespace Ozds.Caching.Test.Services;

public class TestReactorDrainService : IDisposable
{
  private readonly object lockObj = new();
  private readonly List<TaskCompletionSource> waiters = new();
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

    Interlocked.Increment(ref processed);
    lock (lockObj)
    {
      var current = Volatile.Read(ref processed);
      var target = Volatile.Read(ref published);
      if (current >= target)
      {
        foreach (var waiter in waiters)
        {
          waiter.TrySetResult();
        }
        waiters.Clear();
      }
    }
  }

  public Task DrainAsync(CancellationToken cancellationToken)
  {
    ObjectDisposedException.ThrowIf(Volatile.Read(ref disposed), nameof(TestReactorDrainService));

    lock (lockObj)
    {

      if (Volatile.Read(ref processed) >= Volatile.Read(ref published))
      {
        return Task.CompletedTask;
      }

      var tcs = new TaskCompletionSource(
        TaskCreationOptions.RunContinuationsAsynchronously
      );
      waiters.Add(tcs);

      return tcs.Task.WaitAsync(cancellationToken);
    }
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

    lock (lockObj)
    {
      foreach (var waiter in waiters)
      {
        waiter.TrySetCanceled();
      }
      waiters.Clear();
    }
  }
}
