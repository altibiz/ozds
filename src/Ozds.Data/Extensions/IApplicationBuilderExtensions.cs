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
    using var context = app.ApplicationServices
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContext();
    context.Database.Migrate();

    return app;
  }
}
