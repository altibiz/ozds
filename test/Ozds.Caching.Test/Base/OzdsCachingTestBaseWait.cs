using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Services;

namespace Ozds.Caching.Test.Base;

public partial class OzdsCachingTestBase
{
  protected async Task Wait(CancellationToken cancellationToken)
  {
    await DrainReactor(cancellationToken);
  }

  protected Task DrainReactor(CancellationToken cancellationToken)
  {
    return Services
      .GetRequiredService<TestReactorDrainService>()
      .DrainAsync(cancellationToken);
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

    var start = DateTimeOffset.UtcNow;
    await DrainReactor(cancellationToken);
    var result = await update();

    return result;
  }
}
