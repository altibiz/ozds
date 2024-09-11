using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;

namespace Ozds.Data.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsData(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataDbContext>();
    context.Database.Migrate();

    return app;
  }
}
