using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Test.Base;

public partial class OzdsCachingTestBase
{
  protected async Task Wait(
    CancellationToken cancellationToken,
    TimeSpan? timeout = null
  )
  {
    await Task.Delay(timeout ?? Constants.DefaultTimeout, cancellationToken);
  }

  protected bool NotNull<T>(T? value, DateTimeOffset _)
  {
    return value is null;
  }

  protected bool Null<T>(T? value, DateTimeOffset _)
  {
    return value is not null;
  }

  protected async Task<T?> WaitFor<T>(
    Func<T?, DateTimeOffset, bool> predicate,
    Type type,
    string id,
    CancellationToken cancellationToken,
    TimeSpan? timeout = null
  )
    where T : IEntity
  {
    timeout ??= Constants.DefaultTimeout;

    Func<Task<T?>> update =
      typeof(T).IsAssignableTo(typeof(IIdentifiableEntity))
        ? async () =>
          (T?)await IdentifiableQueries.Read(type, id, cancellationToken)
      : typeof(T).IsAssignableTo(typeof(ICompositeEntity))
        ? async () =>
          (T?)await CompositeQueries.Read(type, id, cancellationToken)
      : typeof(T).IsAssignableTo(typeof(IJoinEntity))
        ? async () => (T?)await JoinQueries.Read(type, id, cancellationToken)
      : async () => (T?)await EntityQueries.Read(type, id, cancellationToken);

    var result = await update();
    var start = DateTimeOffset.UtcNow;
    var now = start;
    while (predicate(result, now) && now - start < timeout)
    {
      result = await update();
      now = DateTimeOffset.UtcNow;
    }

    return result;
  }
}
