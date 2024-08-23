using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Options;

namespace Ozds.Data.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsData(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var dataOptions = builder.Configuration.GetValue<OzdsDataOptions>("Ozds:Data")
      ?? throw new InvalidOperationException(
        "Ozds:Data not found in configuration"
      );

    services.AddDbContextFactory<OzdsDataDbContext>(
      (services, options) =>
      {
        if (builder.Environment.IsDevelopment())
        {
#pragma warning disable S125
          // TODO: switch to enable query/mutation logging
          // options.EnableSensitiveDataLogging();
          // options.EnableDetailedErrors();
          // options.UseLoggerFactory(
          //   LoggerFactory.Create(builder => builder.AddConsole())
          // );
#pragma warning restore S125
        }

        options
          .UseTimescale(
            dataOptions.ConnectionString,
            options =>
            {
              options.MigrationsAssembly(
                typeof(OzdsDataDbContext).Assembly.GetName().Name);
              options.MigrationsHistoryTable(
                $"__{nameof(OzdsDataDbContext)}");
            })
          .AddServedSaveChangesInterceptorsFromAssembly(
            Assembly.GetExecutingAssembly(),
            services
          );
      });

    return services;
  }

  public static IApplicationBuilder UseOzdsData(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<OzdsDataDbContext>();
    context.Database.Migrate();

    return app;
  }
}
