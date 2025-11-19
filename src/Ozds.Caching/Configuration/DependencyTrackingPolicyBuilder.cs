using Ozds.Caching.Policies;

namespace Ozds.Caching.Configuration;

public class DependencyTrackingPolicyBuilder
{
  public DependencyTrackingPolicy Build()
  {
    return new DependencyTrackingPolicy();
  }
}
