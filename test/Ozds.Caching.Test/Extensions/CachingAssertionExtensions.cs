using FluentAssertions.Primitives;

namespace Ozds.Caching.Test.Extensions;

public static class CachingAssertionExtensions
{
  public static AndConstraint<TAssertions> BeRuntimeEquivalentTo<
    TSubject,
    TAssertions,
    TExpectation
  >(
    this ObjectAssertions<TSubject, TAssertions> assertions,
    TExpectation expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : ObjectAssertions<TSubject, TAssertions>
  {
    return assertions.BeEquivalentTo(
      expectation,
      options =>
      {
        options.RespectingRuntimeTypes();
        return options;
      },
      because,
      becauseArgs);
  }
}
