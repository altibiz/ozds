using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Assertions;

namespace Ozds.Data.Test.Extensions;

public static class DbContextAssertionsExtensions
{
  public static AndConstraint<TAssertions> BeContextuallyEquivalentTo<
    TSubject,
    TAssertions,
    TExpectation
  >(
    this ObjectAssertions<TSubject, TAssertions> assertions,
    DbContext dbContext,
    TExpectation expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : ObjectAssertions<TSubject, TAssertions>
  {
    var ignoreNavigationsOptions = new IgnoreNavigationsOptions(dbContext);
    var ignoreIgnoredPropertiesOptions = new IgnoreIgnoredPropertiesOptions(
      dbContext);
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.BeEquivalentTo(
      expectation,
      options =>
      {
        ignoreNavigationsOptions.Configure(options);
        ignoreIgnoredPropertiesOptions.Configure(options);
        dateTimeOffsetOptions.Configure(options);
        return options
          .AllowingInfiniteRecursion()
          .IncludingNestedObjects()
          .RespectingRuntimeTypes();
      },
      because,
      becauseArgs);
  }

  public static AndConstraint<TAssertions> NotBeContextuallyEquivalentTo<
    TSubject,
    TAssertions,
    TExpectation
  >(
    this ObjectAssertions<TSubject, TAssertions> assertions,
    DbContext dbContext,
    TExpectation expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : ObjectAssertions<TSubject, TAssertions>
  {
    var ignoreNavigationsOptions = new IgnoreNavigationsOptions(dbContext);
    var ignoreIgnoredPropertiesOptions = new IgnoreIgnoredPropertiesOptions(
      dbContext);
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.NotBeEquivalentTo(
      expectation,
      options =>
      {
        ignoreNavigationsOptions.Configure(options);
        ignoreIgnoredPropertiesOptions.Configure(options);
        dateTimeOffsetOptions.Configure(options);
        return options
          .AllowingInfiniteRecursion()
          .IncludingNestedObjects()
          .RespectingRuntimeTypes();
      },
      because,
      becauseArgs);
  }

  public static AndConstraint<TAssertions> BeContextuallyEquivalentTo<
    TSubject,
    TAssertions,
    TCollection
  >(
    this GenericCollectionAssertions<TCollection, TSubject, TAssertions> assertions,
    DbContext dbContext,
    TCollection expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : GenericCollectionAssertions<TCollection, TSubject, TAssertions>
    where TCollection : IEnumerable<TSubject>
  {
    var ignoreNavigationsOptions = new IgnoreNavigationsOptions(dbContext);
    var ignoreIgnoredPropertiesOptions = new IgnoreIgnoredPropertiesOptions(
      dbContext);
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.BeEquivalentTo(
      expectation,
      options =>
      {
        ignoreNavigationsOptions.Configure(options);
        ignoreIgnoredPropertiesOptions.Configure(options);
        dateTimeOffsetOptions.Configure(options);
        return options
          .AllowingInfiniteRecursion()
          .IncludingNestedObjects()
          .RespectingRuntimeTypes();
      },
      because,
      becauseArgs);
  }

  public static AndConstraint<TAssertions> NotBeContextuallyEquivalentTo<
    TSubject,
    TAssertions,
    TCollection
  >(
    this GenericCollectionAssertions<TCollection, TSubject, TAssertions> assertions,
    DbContext dbContext,
    TCollection expectation,
    string because = "",
    params object[] becauseArgs
  )
    where TAssertions : GenericCollectionAssertions<TCollection, TSubject, TAssertions>
    where TCollection : IEnumerable<TSubject>
  {
    var ignoreNavigationsOptions = new IgnoreNavigationsOptions(dbContext);
    var ignoreIgnoredPropertiesOptions = new IgnoreIgnoredPropertiesOptions(
      dbContext);
    var dateTimeOffsetOptions = new DateTimeOffsetOptions();

    return assertions.NotBeEquivalentTo(
      expectation,
      options =>
      {
        ignoreNavigationsOptions.Configure(options);
        ignoreIgnoredPropertiesOptions.Configure(options);
        dateTimeOffsetOptions.Configure(options);
        return options
          .AllowingInfiniteRecursion()
          .IncludingNestedObjects()
          .RespectingRuntimeTypes();
      },
      because,
      becauseArgs);
  }
}
