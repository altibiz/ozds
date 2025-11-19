using System.Collections.Concurrent;
using Ozds.Caching.Configuration;
using Ozds.Caching.Policies;
using Ozds.Caching.Profiles.Abstractions;

namespace Ozds.Caching.Profiles;

public class ProfileRegistry(
  IReadOnlyList<IProfile> profiles
)
{
  private readonly ConcurrentDictionary<Type, CacheConfiguration>
    configurations =
      new();

  private readonly IReadOnlyList<IProfile> profiles = profiles;

  public CacheConfiguration GetConfiguration(Type type)
  {
    return configurations
      .GetOrAdd(
        type,
        type =>
        {
          var configuration = profiles
            .Where(profile => type.IsAssignableTo(profile.Type))
            .Select(profile => profile.Configuration)
            .Merge();
          if (configuration.Policies.Count == 0)
          {
            configuration.Policies.Add(new DefaultPolicy());
          }

          return configuration;
        });
  }
}
