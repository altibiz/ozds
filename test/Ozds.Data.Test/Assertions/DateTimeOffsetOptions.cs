using FluentAssertions.Equivalency;

namespace Ozds.Data.Test.Assertions;

public class DateTimeOffsetOptions
{
  public SelfReferenceEquivalencyAssertionOptions<TSelf> Configure<
    TSelf
  >(
    SelfReferenceEquivalencyAssertionOptions<TSelf> options
  )
    where TSelf : SelfReferenceEquivalencyAssertionOptions<TSelf>
  =>
    options
      .Using<DateTimeOffset>(context => context.Subject
        .Should()
        .BeCloseTo(context.Expectation, TimeSpan.FromMilliseconds(1)))
      .WhenTypeIs<DateTimeOffset>();
}
