using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Assertions;

public class IgnoreNavigationsOptions(
  DbContext dbContext
)
{
  public SelfReferenceEquivalencyAssertionOptions<TSelf> Configure<
    TSelf
  >(
    SelfReferenceEquivalencyAssertionOptions<TSelf> options
  )
    where TSelf : SelfReferenceEquivalencyAssertionOptions<TSelf>
  =>
    options.Excluding(x => Contains(x.Name, x.DeclaringType));

  private bool Contains(string name, Type? declaringType)
  {
    return navigations.Value.Contains((name, declaringType));
  }

  private readonly Lazy<HashSet<(string, Type?)>> navigations =
    new(() => dbContext
      .GetNavigations()
      .Select(x => (x.Name, x.DeclaringType))
      .ToHashSet());
}