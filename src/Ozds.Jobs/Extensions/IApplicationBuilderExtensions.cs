using Microsoft.EntityFrameworkCore;
using Ozds.Jobs.Context;

namespace Ozds.Jobs.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsJobs(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var context = app.ApplicationServices
      .GetRequiredService<IDbContextFactory<JobsDbContext>>()
      .CreateDbContext();
    context.Database.Migrate();

    return app;
  }
}
