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
    using var context = app.ApplicationServices
      .GetRequiredService<IDbContextFactory<MessagingDbContext>>()
      .CreateDbContext();
    context.Database.Migrate();

    return app;
  }
}
