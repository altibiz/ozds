namespace Ozds.Data.Test.Fixtures;

public record TestResult<T>(
  T Expected,
  T Actual
);
