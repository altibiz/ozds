using Ozds.Caching.Policies;

namespace Ozds.Caching.Configuration;

public class IndirectReverseDependencyEvictionPolicyBuilder(
  IndirectReverseDependencyEvictionPolicyKeyResolver keyResolver,
  Type type
)
{
  public IndirectReverseDependencyEvictionPolicy Build()
  {
    return new IndirectReverseDependencyEvictionPolicy(keyResolver, type);
  }
}
