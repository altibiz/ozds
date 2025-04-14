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

namespace Ozds.Messaging.Extensions;

// FIXME: nothing happens when adding outbox/inbox

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsMessaging(
    this IServiceCollection services,
    // NOTE: hack to allow Ozds.Fake to handle MassTransit
    bool withBus = true
  )
  {
    services.AddOptions();
    services.AddObservers();
    services.AddMutations();
    services.AddQueries();
    services.AddDatabase();
    if (withBus)
    {
      services.AddSender();
      services.AddBus();
    }

    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureOzdsJobsOptions>();
    return services;
  }

  private static IServiceCollection AddObservers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));
    return services;
  }

  private static IServiceCollection AddSender(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IMessageSender));
    return services;
  }

  private static IServiceCollection AddMutations(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IMutations));
    return services;
  }

  private static IServiceCollection AddQueries(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IQueries));
    return services;
  }

  private static void AddDatabase(
    this IServiceCollection services
  )
  {
    services.AddPooledDbContextFactory<MessagingDbContext>(
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
    this IServiceCollection services
  )
  {
    services.AddMassTransit(
      config =>
      {
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

#if DEBUG // TODO: runtime config
        config.UsingRabbitMq(
          (context, cfg) =>
          {
            var messagingOptions = context
              .GetRequiredService<IOptions<OzdsMessagingOptions>>().Value;

            var connectionStringDictionary = messagingOptions.ConnectionString
              .Split(';')
              .ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
            var host = connectionStringDictionary["Host"];
            var virtualHost = connectionStringDictionary["VirtualHost"];
            var username = connectionStringDictionary["Username"];
            var password = connectionStringDictionary["Password"];

            cfg.Host(
              host, virtualHost, cfg =>
              {
                cfg.Username(username);
                cfg.Password(password);
              });
            cfg.ConfigureEndpoints(context);
          });
#else
        config.UsingAzureServiceBus(
          (context, cfg) =>
          {
            var messagingOptions = context
              .GetRequiredService<IOptions<OzdsMessagingOptions>>().Value;

            var connectionString = messagingOptions.ConnectionString;

            cfg.Host(connectionString);
            cfg.ConfigureEndpoints(context);
          });
#endif
      });

    services
      .RemoveHostedService<BusOutboxDeliveryService<MessagingDbContext>>();
    services.RemoveHostedService<InboxCleanupService<MessagingDbContext>>();
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
