namespace Ozds.Client.State;

public enum LoadingState
{
  Loading,
  Error,
  Unfound,
  Found,
  Created
}

public record LoadingState<T>(
  LoadingState State = LoadingState.Loading,
  string? Error = default,
  T? Value = default
)
{
  public LoadingState<T> WithError(string? error)
  {
    return this with
    {
      State = LoadingState.Error,
      Value = default,
      Error = error
    };
  }

  public LoadingState<T> WithValue(T? value)
  {
    return value is null
      ? this with
      {
        State = LoadingState.Unfound,
        Value = default,
        Error = default
      }
      : this with
      {
        State = LoadingState.Found,
        Value = value,
        Error = default
      };
  }

  public LoadingState<T> WithCreated(T value)
  {
    return this with
    {
      State = LoadingState.Created,
      Value = value,
      Error = default
    };
  }

  public LoadingState<T> WithReset()
  {
    return this with
    {
      State = LoadingState.Loading,
      Value = default,
      Error = default
    };
  }
}
