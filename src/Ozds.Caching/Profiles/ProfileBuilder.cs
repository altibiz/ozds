using System.Reflection;
using Ozds.Caching.Configuration;
using Ozds.Caching.Profiles.Abstractions;

namespace Ozds.Caching.Profiles;

public class ProfileBuilder
{
  public ProfileRegistry Build(Assembly assembly)
  {
    var profilers = assembly
      .GetTypes()
      .Where(type => !type.IsAbstract && type.IsAssignableTo(typeof(IProfiler)))
      .Select(Activator.CreateInstance)
      .OfType<IProfiler>()
      .ToList();

    var profiles = profilers
      .Select(profiler => profiler.Profile(CacheConfigurationBuilder.Default))
      .ToList();

    return new ProfileRegistry(profiles);
  }

  public ProfileRegistry Build(Type type)
  {
    var profilers = type.Assembly
      .GetTypes()
      .Where(
        type => type
          .IsAssignableTo(
            typeof(IProfiler<>)
              .MakeGenericType(type)))
      .Select(Activator.CreateInstance)
      .OfType<IProfiler>()
      .ToList();

    var profiles = profilers
      .Select(profiler => profiler.Profile(CacheConfigurationBuilder.Default))
      .ToList();

    return new ProfileRegistry(profiles);
  }
}
