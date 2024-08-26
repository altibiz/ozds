using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Observers;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Options;

namespace Ozds.Messaging.Extensions;

// FIXME: outbox and inbox
#pragma warning disable S125
// services.RemoveHostedService<BusOutboxDeliveryService<OzdsMessagingDbContext>>();
// services.RemoveHostedService<InboxCleanupService<OzdsMessagingDbContext>>();
#pragma warning restore S125

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsMessaging(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.Configure<OzdsMessagingOptions>(
      builder.Configuration.GetSection("Ozds:Messaging"));

    var options = builder.Configuration.GetValue<OzdsMessagingOptions>(
      "Ozds:Messaging") ?? throw new InvalidOperationException(
        "Ozds:Messaging not found in configuration");

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

        config.AddEntityFrameworkOutbox<OzdsMessagingDbContext>(config =>
        {
          config.UsePostgres();
          config.UseBusOutbox();
        });
        config.AddConfigureEndpointsCallback((context, name, config) =>
        {
          config.UseEntityFrameworkOutbox<OzdsMessagingDbContext>(context);
        });
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

    services.AddSingleton<INetworkUserInvoiceStatePublisher, NetworkUserInvoiceStateObserver>();
    services.AddSingleton<INetworkUserInvoiceStateSubscriber, NetworkUserInvoiceStateObserver>();

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
      configurator.EntityFrameworkRepository(config =>
      {
        config.ConcurrencyMode = ConcurrencyMode.Optimistic;
        config.ExistingDbContext<OzdsMessagingDbContext>();
        config.UsePostgres();
      });
    }
  }
}
