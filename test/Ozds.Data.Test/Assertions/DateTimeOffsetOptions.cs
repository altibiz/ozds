using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Test.Assertions;

public class DateTimeOffsetOptions(
#pragma warning disable CS9113 // Parameter is unread.
  DbContext dbContext
#pragma warning restore CS9113 // Parameter is unread.
)
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
