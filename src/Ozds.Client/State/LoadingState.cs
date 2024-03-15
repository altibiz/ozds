namespace Ozds.Client.State;

public record LoadingState<T>(
  bool IsLoading = true,
  string? Error = default,
  T? Value = default
)
{
  public LoadingState<T> WithError(string? error) => this with { IsLoading = false, Error = error };
  public LoadingState<T> WithValue(T? value) => this with { IsLoading = false, Value = value };
  public LoadingState<T> NotFound() => this with { IsLoading = false };
}
