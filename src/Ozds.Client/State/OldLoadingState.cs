namespace Ozds.Client.State;

public record OldLoadingState<T>(
  bool IsLoading = true,
  string? Error = default,
  T? Value = default
)
{
  public OldLoadingState<T> WithError(string? error)
  {
    return this with { IsLoading = false, Error = error };
  }

  public OldLoadingState<T> WithValue(T? value)
  {
    return this with { IsLoading = false, Value = value };
  }

  public OldLoadingState<T> NotFound()
  {
    return this with { IsLoading = false };
  }
}
