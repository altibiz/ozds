using Altibiz.DependencyInjection.Extensions;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Ozds.Messaging.Context;
using Ozds.Messaging.Mutations.Abstractions;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Options;
using Ozds.Messaging.Queries.Abstractions;
using Ozds.Messaging.Sender.Abstractions;
using Ozds.Messaging.Services;

namespace Ozds.Messaging.Extensions;

// FIXME: nothing happens when adding outbox/inbox

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsMessaging(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddObservers();
    builder.AddMutations();
    builder.AddQueries();
    builder.AddDatabase();
    builder.AddServices();
    if (ConfigureOzdsMessagingOptions.WithBus(builder.Configuration))
    {
      builder.AddSender();
      builder.AddBus();
    }

    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsMessagingOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddObservers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IPublisher));
    builder.Services.AddSingletonAssignableTo(typeof(ISubscriber));
    return builder;
  }

  private static IHostApplicationBuilder AddSender(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IMessageSender));
    return builder;
  }

  private static IHostApplicationBuilder AddMutations(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IMutations));
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IQueries));
    return builder;
  }

  private static IHostApplicationBuilder AddServices(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddHostedService<MigrationService>();
    return builder;
  }

  private static void AddDatabase(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddPooledDbContextFactory<MessagingDbContext>(
      (services, builder) =>
      {
        var messagingOptions = services
          .GetRequiredService<IOptions<OzdsMessagingOptions>>().Value;
        var environment = services
          .GetRequiredService<IHostEnvironment>();

        builder
          .UseNpgsql(
            messagingOptions.PersistenceConnectionString, m =>
            {
              m.MigrationsAssembly(
                typeof(MessagingDbContext).Assembly.GetName().Name);
              m.MigrationsHistoryTable(
                $"__Ozds{nameof(MessagingDbContext)}");
            })
          .UseSnakeCaseNamingConvention();

        if (environment.IsDevelopment())
        {
          builder.ConfigureWarnings(
            warnings => warnings
              .Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        }
      });
  }

  private static void AddBus(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddMassTransit(
      config =>
      {
        var connectionString = ConfigureOzdsMessagingOptions
          .ParseConnectionString(builder.Configuration);

        var assembly = typeof(MessagingDbContext).Assembly;

#pragma warning disable S125
        // x.AddEntityFrameworkOutbox<MessagingDbContext>(o =>
        // {
        //   o.UsePostgres();
        //   o.UseBusOutbox();
        // });
        // x.AddConfigureEndpointsCallback((context, name, cfg) =>
        // {
        //   cfg.UseEntityFrameworkOutbox<MessagingDbContext>(context);
        // });
#pragma warning restore S125
        config.SetKebabCaseEndpointNameFormatter();

        config.AddConsumers(assembly);
        config.AddSagaStateMachines(assembly);
        config.AddActivities(assembly);

        config.AddSagas(assembly);
        config.SetSagaRepositoryProvider(
          new OzdsSagaRepositoryRegistrationProvider());

        if (connectionString is OzdsMessagingParsedRabbitMqConnectionString
          rabbitMqConnectionString)
        {
          config.UsingRabbitMq(
            (context, cfg) =>
            {
              cfg.Host(
                rabbitMqConnectionString.Host,
                (ushort)rabbitMqConnectionString.Port,
                rabbitMqConnectionString.VirtualHost,
                cfg =>
                {
                  cfg.Username(rabbitMqConnectionString.User);
                  cfg.Password(rabbitMqConnectionString.Password);
                });
              cfg.ConfigureEndpoints(context);
            });
        }
        else if (connectionString
          is OzdsMessagingParsedAzureServiceBusConnectionString
          azureServiceBusConnectionString)
        {
          config.UsingAzureServiceBus(
            (context, cfg) =>
            {
              cfg.Host(azureServiceBusConnectionString.ConnectionString);
              cfg.ConfigureEndpoints(context);
            });
        }
        else
        {
          throw new InvalidOperationException(
            "Unknown connection string type");
        }
      });

    builder.Services
      .RemoveHostedService<BusOutboxDeliveryService<MessagingDbContext>>();
    builder.Services
      .RemoveHostedService<InboxCleanupService<MessagingDbContext>>();
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
          // NOTE: yea its a function to a function and
          // idk why the API is like that but it works
          config.DatabaseFactory(
            services => () =>
            {
              var factory = services
                .GetRequiredService<IDbContextFactory<MessagingDbContext>>();
              var dbContext = factory.CreateDbContext();
              return dbContext;
            });
          config.UsePostgres();
        });
    }
  }
}
