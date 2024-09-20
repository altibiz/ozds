using Ozds.Business.Observers.Abstractions;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Server.Middleware;

public class ExceptionMiddleware(
  RequestDelegate next,
  IErrorPublisher publisher
)
{
  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await next(httpContext);
    }
    catch (Exception ex)
    {
      publisher.PublishError(new ErrorEventArgs
      {
        Message = ex.Message,
        Exception = ex
      });

      throw;
    }
  }
}
