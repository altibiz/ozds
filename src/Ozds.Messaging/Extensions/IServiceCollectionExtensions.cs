using MassTransit;
using MassTransit.Configuration;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Observers;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Options;

namespace Ozds.Messaging.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsMessaging(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var options = builder.Configuration.GetValue<OzdsMessagingOptions>(
      "Ozds:Messaging") ?? throw new InvalidOperationException(
        "Ozds:Messaging not found in configuration");

    services.AddSingleton<INetworkUserInvoiceStatePublisher, NetworkUserInvoiceStateObserver>();
    services.AddSingleton<INetworkUserInvoiceStateSubscriber, NetworkUserInvoiceStateObserver>();

    services.AddDbContext<OzdsMessagingDbContext>(
      builder => builder
        .UseNpgsql(
          options.PersistenceConnectionString, m =>
          {
            m.MigrationsAssembly(
              typeof(OzdsMessagingDbContext).Assembly.GetName().Name);
            m.MigrationsHistoryTable(
              $"__{nameof(OzdsMessagingDbContext)}");
          }));

    services.AddMassTransit(
      config =>
      {
        var assembly = typeof(OzdsMessagingDbContext).Assembly;

#pragma warning disable S125
        // FIXME: nothing happens
        // x.AddEntityFrameworkOutbox<OzdsMessagingDbContext>(o =>
        // {
        //   o.UsePostgres();
        //   o.UseBusOutbox();
        // });
        // x.AddConfigureEndpointsCallback((context, name, cfg) =>
        // {
        //   cfg.UseEntityFrameworkOutbox<OzdsMessagingDbContext>(context);
        // });
#pragma warning restore S125
        config.SetKebabCaseEndpointNameFormatter();

        config.AddConsumers(assembly);
        config.AddSagaStateMachines(assembly);
        config.AddActivities(assembly);

        config.AddSagas(assembly);
        config.SetSagaRepositoryProvider(
          new OzdsSagaRepositoryRegistrationProvider());

        if (builder.Environment.IsDevelopment())
        {
          var connectionStringDictionary = options.ConnectionString
            .Split(';')
            .ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
          var host = connectionStringDictionary["Host"];
          var virtualHost = connectionStringDictionary["VirtualHost"];
          var username = connectionStringDictionary["Username"];
          var password = connectionStringDictionary["Password"];

          config.UsingRabbitMq(
            (context, cfg) =>
            {
              cfg.Host(
                host, virtualHost, cfg =>
                {
                  cfg.Username(username);
                  cfg.Password(password);
                });
              cfg.ConfigureEndpoints(context);
            });
        }
        else
        {
          var connectionString = options.ConnectionString;

          config.UsingAzureServiceBus(
            (context, cfg) =>
            {
              cfg.Host(connectionString);
              cfg.ConfigureEndpoints(context);
            });
        }
      });

    // FIXME: nothing happens when not removed
    services
      .RemoveHostedService<BusOutboxDeliveryService<OzdsMessagingDbContext>>();
    services.RemoveHostedService<InboxCleanupService<OzdsMessagingDbContext>>();

    return services;
  }

  public static IApplicationBuilder UseOzdsMessaging(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<OzdsMessagingDbContext>();
    context.Database.Migrate();

    return app;
  }

  private sealed class OzdsSagaRepositoryRegistrationProvider
    : ISagaRepositoryRegistrationProvider
  {
    public void Configure<TSaga>(
      ISagaRegistrationConfigurator<TSaga> configurator
    )
      where TSaga : class, ISaga
    {
      configurator
        .EntityFrameworkRepository(
          r =>
          {
            r.ConcurrencyMode = ConcurrencyMode.Optimistic;
            r.ExistingDbContext<OzdsMessagingDbContext>();
            r.UsePostgres();
          });
    }
  }
}
