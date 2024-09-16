using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;
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
    // Options
    services.Configure<OzdsMessagingOptions>(
      builder.Configuration.GetSection("Ozds:Messaging"));

    // MassTransit
    services.AddMassTransit(builder);

    // Observers
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));

    return services;
  }

  private static void AddMassTransit(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var messagingOptions = builder.Configuration
        .GetSection("Ozds:Messaging")
        .Get<OzdsMessagingOptions>()
      ?? throw new InvalidOperationException(
        "Missing Ozds:Messaging configuration");

    services.AddDbContext<MessagingDbContext>(
      builder => builder
        .UseNpgsql(
          messagingOptions.PersistenceConnectionString, m =>
          {
            m.MigrationsAssembly(
              typeof(MessagingDbContext).Assembly.GetName().Name);
            m.MigrationsHistoryTable(
              $"__{nameof(MessagingDbContext)}");
          }));

    services.AddMassTransit(
      config =>
      {
        var assembly = typeof(MessagingDbContext).Assembly;

        config.AddEntityFrameworkOutbox<MessagingDbContext>(
          config =>
          {
            config.UsePostgres();
            config.UseBusOutbox();
          });
        config.AddConfigureEndpointsCallback(
          (context, name, config) =>
          {
            config.UseEntityFrameworkOutbox<MessagingDbContext>(context);
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
          var connectionStringDictionary = messagingOptions.ConnectionString
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
          var connectionString = messagingOptions.ConnectionString;

          config.UsingAzureServiceBus(
            (context, cfg) =>
            {
              cfg.Host(connectionString);
              cfg.ConfigureEndpoints(context);
            });
        }
      });
  }

  private sealed class OzdsSagaRepositoryRegistrationProvider
    : ISagaRepositoryRegistrationProvider
  {
    public void Configure<TSaga>(
      ISagaRegistrationConfigurator<TSaga> configurator
    )
      where TSaga : class, ISaga
    {
      configurator.EntityFrameworkRepository(
        config =>
        {
          config.ConcurrencyMode = ConcurrencyMode.Optimistic;
          config.ExistingDbContext<MessagingDbContext>();
          config.UsePostgres();
        });
    }
  }

  private static void AddSingletonAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddSingleton(conversionType);
      foreach (var interfaceType in conversionType.GetAllInterfaces())
      {
        services.AddSingleton(
          interfaceType, services =>
            services.GetRequiredService(conversionType));
      }
    }
  }

  private static Type[] GetAllInterfaces(this Type type)
  {
    return type.GetInterfaces()
      .Concat(type.GetInterfaces().SelectMany(GetAllInterfaces))
      .Concat(type.BaseType?.GetAllInterfaces() ?? Array.Empty<Type>())
      .Distinct()
      .ToArray();
  }
}
