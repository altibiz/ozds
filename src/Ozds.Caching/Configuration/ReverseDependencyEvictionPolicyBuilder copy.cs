using Ozds.Caching.Policies;

namespace Ozds.Caching.Configuration;

public class ReverseDependencyEvictionPolicyBuilder
{
  public ReverseDependencyEvictionPolicy Build()
  {
    return new ReverseDependencyEvictionPolicy();
  }
}
