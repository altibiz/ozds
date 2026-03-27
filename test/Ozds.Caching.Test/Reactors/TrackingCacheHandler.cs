using Ozds.Caching.Observers.EventArgs;
using Ozds.Caching.Reactors.Implementations;
using Ozds.Caching.Test.Services;

namespace Ozds.Caching.Test.Reactors;

public class TrackingCacheHandler(
  IServiceProvider serviceProvider,
  TestReactorDrainService drain
) : CacheHandler(serviceProvider)
{
  public override async Task Handle(
    CacheEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    await base.Handle(eventArgs, cancellationToken);
    drain.NotifyProcessed();
  }
}
