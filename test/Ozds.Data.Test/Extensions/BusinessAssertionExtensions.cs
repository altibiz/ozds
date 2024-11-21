using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using Ozds.Data.Test.Assertions;

namespace Ozds.Data.Test.Extensions;

public static class BusinessAssertionExtensions
{
  public static AndConstraint<TAssertions> BeBusinesswiseEquivalentTo<
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
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.BeEquivalentTo(
      expectation,
      options =>
      {
        dateTimeOffsetOptions.Configure(options);
        return options;
      },
      because,
      becauseArgs);
  }

  public static AndConstraint<TAssertions> NotBeBusinesswiseEquivalentTo<
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
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.NotBeEquivalentTo(
      expectation,
      options =>
      {
        dateTimeOffsetOptions.Configure(options);
        return options;
      },
      because,
      becauseArgs);
  }

  public static AndConstraint<TAssertions> BeBusinesswiseEquivalentTo<
    TSubject,
    TAssertions,
    TCollection
  >(
    this GenericCollectionAssertions<TCollection, TSubject, TAssertions> assertions,
    TCollection expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : GenericCollectionAssertions<TCollection, TSubject, TAssertions>
    where TCollection : IEnumerable<TSubject>
  {
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.BeEquivalentTo(
      expectation,
      options =>
      {
        dateTimeOffsetOptions.Configure(options);
        return options;
      },
      because,
      becauseArgs);
  }

  public static AndConstraint<TAssertions> NotBeBusinesswiseEquivalentTo<
    TSubject,
    TAssertions,
    TCollection
  >(
    this GenericCollectionAssertions<TCollection, TSubject, TAssertions> assertions,
    TCollection expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : GenericCollectionAssertions<TCollection, TSubject, TAssertions>
    where TCollection : IEnumerable<TSubject>
  {
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.NotBeEquivalentTo(
      expectation,
      options =>
      {
        dateTimeOffsetOptions.Configure(options);
        return options;
      },
      because,
      becauseArgs);
  }
}
