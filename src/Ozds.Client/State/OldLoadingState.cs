namespace Ozds.Client.State;

// public record LoadingState<T>(
//   bool IsLoading = true,
//   string? Error = default,
//   T? Value = default
// )
// {
//   public LoadingState<T> WithError(string? error)
//   {
//     return this with { IsLoading = false, Error = error };
//   }

//   public LoadingState<T> WithValue(T? value)
//   {
//     return this with { IsLoading = false, Value = value };
//   }

//   public LoadingState<T> NotFound()
//   {
//     return this with { IsLoading = false };
//   }
// }
