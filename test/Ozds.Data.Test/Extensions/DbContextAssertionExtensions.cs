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
    var ignoreKeysOptions = new IgnoreKeysOptions(dbContext);
    var ignoreForeignKeysOptions = new IgnoreForeignKeysOptions(dbContext);
    var ignoreIgnoredPropertiesOptions = new IgnoreIgnoredPropertiesOptions(
      dbContext);

    return assertions.BeEquivalentTo(
      expectation,
      options =>
      {
        ignoreNavigationsOptions.Configure(options);
        ignoreKeysOptions.Configure(options);
        ignoreForeignKeysOptions.Configure(options);
        ignoreIgnoredPropertiesOptions.Configure(options);
        return options;
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
    var ignoreKeysOptions = new IgnoreKeysOptions(dbContext);
    var ignoreForeignKeysOptions = new IgnoreForeignKeysOptions(dbContext);
    var ignoreIgnoredPropertiesOptions = new IgnoreIgnoredPropertiesOptions(
      dbContext);

    return assertions.NotBeEquivalentTo(
      expectation,
      options =>
      {
        ignoreNavigationsOptions.Configure(options);
        ignoreKeysOptions.Configure(options);
        ignoreForeignKeysOptions.Configure(options);
        ignoreIgnoredPropertiesOptions.Configure(options);
        return options;
      },
      because,
      becauseArgs);
  }
}
