// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Components.Web;
// using Ozds.Business.Observers.Abstractions;
// using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

// namespace Ozds.Client.Components.Layout;

// public class MainLayoutErrorBoundary : ErrorBoundary
// {
//   [Inject]
//   public IErrorPublisher Publisher { get; set; } = default!;

//   protected override Task OnErrorAsync(Exception exception)
//   {
//     Publisher.PublishError(
//       new ErrorEventArgs
//       {
//         Message = exception.Message,
//         Exception = exception
//       });

//     return Task.CompletedTask;
//   }
// }
