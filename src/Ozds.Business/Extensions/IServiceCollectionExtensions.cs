using System.Reflection;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using Ozds.Business.Activation.Abstractions;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Aggregation;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Finance;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Iot;
using Ozds.Business.Localization;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Naming.Abstractions;
using Ozds.Business.Naming.Agnostic;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Extensions;
using Ozds.Messaging;

// TODO: switch to enable query/mutation logging

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusinessClient(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    // Activation
    services.AddTransientAssignableTo(typeof(IModelActivator));
    services.AddSingleton(typeof(AgnosticModelActivator));

    services.AddData(builder);
    services.AddMessaging(builder);

    services.AddScopedAssignableTo(typeof(IOzdsQueries));
    services.AddScopedAssignableTo(typeof(IOzdsMutations));

    services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    services.AddSingleton(typeof(AgnosticAggregateUpserter));

    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(AgnosticModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IMeasurementAggregateConverter));
    services.AddSingleton(typeof(AgnosticMeasurementAggregateConverter));
    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));
    services.AddSingleton(typeof(AgnosticPushRequestMeasurementConverter));
    services.AddTransientAssignableTo(typeof(IMeterNamingConvention));
    services.AddSingleton(typeof(AgnosticMeterNamingConvention));

    services.AddTransientAssignableTo(
      typeof(INetworkUserCalculationCalculator));
    services.AddSingleton(typeof(AgnosticNetworkUserCalculationCalculator));
    services.AddTransientAssignableTo(typeof(ICalculationItemCalculator));
    services.AddSingleton(typeof(AgnosticCalculationItemCalculator));
    services.AddTransient(typeof(NetworkUserInvoiceCalculator));
    services.AddScoped(typeof(NetworkUserInvoiceIssuer));

    services.AddScoped<OzdsIotHandler>();
    services.AddScoped<BatchAggregatedMeasurementUpserter>();

    services.AddSingleton<IOzdsLocalizer, OzdsLocalizer>();

    return services;
  }

  private static void AddData(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddDbContextFactory<OzdsDataDbContext>(
      (services, options) =>
      {
        var connectionString =
          builder.Configuration["Ozds:Data:ConnectionString"]
          ?? throw new InvalidOperationException(
            "Ozds:Data:ConnectionString not found in configuration"
          );

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
            connectionString,
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
  }

  private static void AddMessaging(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var persistenceConnectionString = builder.Configuration[
        "Ozds:Messaging:PersistenceConnectionString"]
      ?? throw new InvalidOperationException(
        "Ozds:Messaging:PersistenceConnectionString not found in configuration"
      );

    services.AddDbContext<OzdsMessagingDbContext>(
      builder => builder
        .UseNpgsql(
          persistenceConnectionString, m =>
          {
            m.MigrationsAssembly(
              typeof(OzdsMessagingDbContext).Assembly.GetName().Name);
            m.MigrationsHistoryTable(
              $"__{nameof(OzdsMessagingDbContext)}");
          }));

    services.AddMassTransit(
      x =>
      {
        var messagingAssembly = typeof(OzdsMessagingDbContext).Assembly;
        var businessAssembly = typeof(IServiceCollectionExtensions).Assembly;

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
        x.SetKebabCaseEndpointNameFormatter();

        x.AddConsumers(businessAssembly);
        x.AddSagaStateMachines(businessAssembly);
        x.AddActivities(businessAssembly);

        x.AddSagas(messagingAssembly);
        x.SetSagaRepositoryProvider(
          new OzdsSagaRepositoryRegistrationProvider());

        if (builder.Environment.IsDevelopment())
        {
          var connectionStringDictionary = (
              builder.Configuration["Ozds:Messaging:ConnectionString"]
              ?? throw new InvalidOperationException(
                "Messaging connection string not configured"
              )
            )
            .Split(';')
            .ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
          var host = connectionStringDictionary["Host"];
          var virtualHost = connectionStringDictionary["VirtualHost"];
          var username = connectionStringDictionary["Username"];
          var password = connectionStringDictionary["Password"];

          x.UsingRabbitMq(
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
          var connectionString =
            builder.Configuration["Ozds:Messaging:ConnectionString"]
            ?? throw new InvalidOperationException(
              "Azure Service Bus connection string not configured"
            );

          x.UsingAzureServiceBus(
            (context, cfg) =>
            {
              cfg.Host(connectionString);
              cfg.ConfigureEndpoints(context);
            });
        }
      });

    services.RemoveHostedService<MassTransitHostedService>();
    services
      .RemoveHostedService<BusOutboxDeliveryService<OzdsMessagingDbContext>>();
    services.RemoveHostedService<InboxCleanupService<OzdsMessagingDbContext>>();

    services.AddSingleton<
      IModularTenantEvents,
      HostedServiceModularTenantEvents<
        MassTransitHostedService>>();

#pragma warning disable S125
    // FIXME: nothing happens
    // services.AddSingleton<
    //   IModularTenantEvents,
    //   HostedServiceModularTenantEvents<
    //     BusOutboxDeliveryService<OzdsMessagingDbContext>>>();
    // services.AddSingleton<
    //   IModularTenantEvents,
    //   HostedServiceModularTenantEvents<
    //     InboxCleanupService<OzdsMessagingDbContext>>>();
#pragma warning restore S125
  }

  private static void AddScopedAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          !type.IsGenericType &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddScoped(assignableTo, conversionType);
      services.AddScoped(conversionType);
    }
  }

  private static void AddTransientAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          !type.IsGenericType &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddTransient(assignableTo, conversionType);
      services.AddTransient(conversionType);
    }
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

  private sealed class HostedServiceModularTenantEvents<THostedService>(
    IServiceProvider serviceProvider,
    ILogger<HostedServiceModularTenantEvents<THostedService>> logger,
    ShellSettings shellSettings
  ) : ModularTenantEvents, IDisposable, IAsyncDisposable
    where THostedService : IHostedService
  {
    private bool _disposed;
    private THostedService? _hostedService;
    private bool _started;

    public async ValueTask DisposeAsync()
    {
      if (_hostedService is not IAsyncDisposable asyncDisposable)
      {
        return;
      }

      if (_disposed || _hostedService is null)
      {
        _disposed = true;
        return;
      }

      if (_started)
      {
        await _hostedService.StopAsync(CancellationToken.None);
        _started = false;
      }

      await asyncDisposable.DisposeAsync();
      _disposed = true;
    }

    public void Dispose()
    {
      if (_hostedService is not IDisposable disposable)
      {
        return;
      }

      if (_disposed || _hostedService is null)
      {
        _disposed = true;
        return;
      }

      if (_started)
      {
        _hostedService
          .StopAsync(CancellationToken.None)
          .GetAwaiter()
          .GetResult();
        _started = false;
      }

      disposable.Dispose();
      _disposed = true;
    }

    public override async Task ActivatedAsync()
    {
      ObjectDisposedException.ThrowIf(
        _disposed,
        _hostedService!
      );

      if (_started)
      {
        return;
      }

      logger.LogInformation(
        "Starting hosted service '{HostedService}' for tenant '{TenantName}'.",
        typeof(THostedService).Name,
        shellSettings.Name
      );

      _hostedService = ActivatorUtilities
        .CreateInstance<THostedService>(serviceProvider);

      await _hostedService
        .StartAsync(CancellationToken.None)
        .ConfigureAwait(false);

      _started = true;
    }

    public override async Task TerminatingAsync()
    {
      ObjectDisposedException.ThrowIf(
        _disposed,
        _hostedService!
      );

      if (_hostedService is null || !_started)
      {
        return;
      }

      logger.LogInformation(
        "Stopping hosted service '{HostedService}' for tenant '{TenantName}'.",
        typeof(THostedService).Name,
        shellSettings.Name
      );

      await _hostedService
        .StopAsync(CancellationToken.None)
        .ConfigureAwait(false);

      _started = false;
    }
  }
}
