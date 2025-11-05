using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Dependencies;

public class DependenciesEntity : IDependencyEntity
{
  public List<string> Dependencies { get; set; } = new();
}
