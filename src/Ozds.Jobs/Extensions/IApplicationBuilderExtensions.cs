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
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<JobsDbContext>();
    context.Database.Migrate();

    return app;
  }
}
