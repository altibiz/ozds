namespace Ozds.Client.State;

public enum LoadingStage
{
  Loading,
  Error,
  Unfound,
  Found,
  Created,
}

public record LoadingState(
  LoadingStage Stage,
  Type Type,
  string? Error = default,
  object? ObjectValue = default
);

public record LoadingState<T>(
  LoadingStage Stage = LoadingStage.Loading,
  string? Error = default,
  T? Value = default
) : LoadingState(Stage, typeof(T), Error, Value)
{
  public LoadingState<TMapped> Map<TMapped>(Func<T, TMapped> map)
  {
    var mapped = Value is null ? default : map(Value);
    return new LoadingState<TMapped>(Stage, Error, mapped);
  }

  public LoadingState<T> WithError(string? error)
  {
    return this with
    {
      Stage = LoadingStage.Error,
      Value = default,
      Error = error,
    };
  }

  public LoadingState<T> WithValue(T? value)
  {
    return value is null
      ? this with
      {
        Stage = LoadingStage.Unfound,
        Value = default,
        Error = default,
      }
      : this with
      {
        Stage = LoadingStage.Found,
        Value = value,
        Error = default,
      };
  }

  public LoadingState<T> WithCreated(T value)
  {
    return this with
    {
      Stage = LoadingStage.Created,
      Value = value,
      Error = default,
    };
  }

  public LoadingState<T> WithReset()
  {
    return this with
    {
      Stage = LoadingStage.Loading,
      Value = default,
      Error = default,
    };
  }

  public LoadingState<T> NotFound()
  {
    return this with
    {
      Stage = LoadingStage.Unfound,
      Value = default,
      Error = default,
    };
  }
}
