using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;

namespace Ozds.Messaging.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsMessaging(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MessagingDbContext>();
    context.Database.Migrate();

    return app;
  }
}
