using Microsoft.EntityFrameworkCore;
using Ozds.Data;
using Ozds.Messaging;

namespace Ozds.Business.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder MigrateOzdsData(
    this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    scope.ServiceProvider.GetRequiredService<OzdsDataDbContext>().Database
      .Migrate();
    return app;
  }

  public static IApplicationBuilder MigrateOzdsMessaging(
    this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    scope.ServiceProvider.GetRequiredService<OzdsMessagingDbContext>().Database
      .Migrate();
    return app;
  }
}
