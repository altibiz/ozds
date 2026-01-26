using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Observers.Abstractions;
using Ozds.Caching.Observers.EventArgs;
using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Reactors.Base;

namespace Ozds.Caching.Reactors.Implementations;

public class CacheReactor(
  IServiceProvider serviceProvider
) : Reactor<
  CacheEventArgs,
  ICacheSubscriber,
  CacheHandler>(serviceProvider)
{
}

public class CacheHandler(
  IServiceProvider serviceProvider
) : ReactorHandler<CacheEventArgs>
{
  public override async Task Handle(
    CacheEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var logger = serviceProvider.GetRequiredService<ILogger<CacheHandler>>();
    var factory = serviceProvider.GetRequiredService<IPolicyCacheFactory>();
    var cache = factory.Create();
    var policyContext = eventArgs is CreateCacheEventArgs createCacheEventArgs
      ? new CreateCacheEventPolicyContext(
        serviceProvider, cache, createCacheEventArgs)
      : new CacheEventPolicyContext(serviceProvider, cache, eventArgs);
    await Task.WhenAll(
      eventArgs.CacheConfiguration.Policies
        .Select(async policy =>
        {
          try
          {
            await policy.HandleCacheEvent(policyContext, cancellationToken);
          }
          catch (Exception ex)
          {
            logger.LogError(
              ex,
              "Policy {Policy} failed with {Key}",
              policy.GetType().Name,
              eventArgs.Key);
          }
        }));
  }
}
