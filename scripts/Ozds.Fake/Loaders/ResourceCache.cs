using System.Collections.Concurrent;
using Ozds.Fake.Loaders.Abstractions;

namespace Ozds.Fake.Loaders;

public class ResourceCache(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<(string, Type), Lazy<Task<object>>>
    _cache = new();

  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public TOut Get<TLoader, TOut>(string name)
    where TLoader : ILoader<TOut>
    where TOut : class
  {
    return GetAsync<TLoader, TOut>(name).Result;
  }

  public async Task<TOut> GetAsync<TLoader, TOut>(
    string name,
    CancellationToken cancellationToken = default
  )
    where TLoader : ILoader<TOut>
    where TOut : class
  {
    return (await _cache.GetOrAdd(
      (name, typeof(TLoader)),
      _ => new Lazy<Task<object>>(
        async () =>
        {
          await using var stream = Load(name);
          var loader = _serviceProvider.GetRequiredService<TLoader>();
          return loader.Load(stream);
        })
    ).Value as TOut)!;
  }

  private static Stream Load(string name)
  {
    var assembly = typeof(ResourceCache).Assembly;
    var fullName = $"{assembly.GetName().Name}.Assets.{name}";

    var stream = assembly.GetManifestResourceStream(fullName) ??
      throw new InvalidOperationException(
        $"Resource {fullName} does not exist. "
        + $"Here are the available resources for the given assembly '{
          assembly.GetName().Name
        }':\n"
        + string.Join("\n", assembly.GetManifestResourceNames())
      );
    return stream;
  }
}
