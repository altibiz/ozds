using Microsoft.EntityFrameworkCore;
using Ozds.Data;

namespace Ozds.Business.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder MigrateOzdsData(
    this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    scope.ServiceProvider.GetRequiredService<OzdsDbContext>().Database.Migrate();
    return app;
  }
}
