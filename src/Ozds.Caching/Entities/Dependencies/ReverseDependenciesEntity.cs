using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Dependencies;

public class ReverseDependenciesEntity : IDependencyEntity
{
  public List<string> ReverseDependencies { get; set; } = new();
}
