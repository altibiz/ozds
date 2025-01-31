using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Assertions;

public class IgnoreIgnoredPropertiesOptions(
  DbContext dbContext
)
{
  private readonly Lazy<HashSet<(string, Type?)>> ignored =
    new(
      () => dbContext
        .GetIgnoredProperties()
        .Select(x => (x.Name, x.DeclaringType))
        .ToHashSet());

  public SelfReferenceEquivalencyAssertionOptions<TSelf> Configure<
    TSelf
  >(
    SelfReferenceEquivalencyAssertionOptions<TSelf> options
  )
    where TSelf : SelfReferenceEquivalencyAssertionOptions<TSelf>
  {
    return options.Excluding(x => Contains(x.Name, x.DeclaringType));
  }

  private bool Contains(string name, Type? declaringType)
  {
    return ignored.Value.Contains((name, declaringType));
  }
}
