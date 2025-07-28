using Altibiz.DependencyInjection.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Ozds.Fake.Arguments;
using Ozds.Fake.Client;
using Ozds.Fake.Cloning;
using Ozds.Fake.Cloning.Abstractions;
using Ozds.Fake.Conversion;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Correction;
using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Faking;
using Ozds.Fake.Faking.Abstractions;
using Ozds.Fake.Generation;
using Ozds.Fake.Generation.Abstractions;
using Ozds.Fake.Loading;
using Ozds.Fake.Loading.Abstractions;
using Ozds.Fake.Options;
using Ozds.Fake.Packing;
using Ozds.Fake.Packing.Abstractions;
using Ozds.Fake.Services;
using Ozds.Fake.Workers.Abstractions;
using Ozds.Messaging.Context;
using Ozds.Messaging.Options;

// TODO: remove dependency on Ozds.Messaging here

namespace Ozds.Fake.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsFake(
    this IHostApplicationBuilder builder,
    IOzdsFakeArguments arguments
  )
  {
    builder.Services
      .AddSingleton(arguments.GetType(), arguments);

    builder
      .AddOptions()
      .AddConversion()
      .AddCorrection()
      .AddLoaders()
      .AddGenerators()
      .AddCloners()
      .AddPacking()
      .AddFaking();

    if (arguments is OzdsFakePushArguments push)
    {
      builder.AddClient(push.Timeout_s);
      builder.AddWorkers();
      builder.Services.AddHostedService<PushService>();
    }

    if (arguments is OzdsFakeSeedArguments seed)
    {
      builder.AddClient(seed.Timeout_s);
      builder.AddWorkers();
      builder.Services.AddHostedService<SeedService>();
    }

    if (arguments is OzdsFakeInsertArguments insert)
    {
      builder.AddClient(insert.Timeout_s);
      builder.AddWorkers();
      builder.Services.AddHostedService<InsertService>();
    }

    if (arguments is OzdsFakeAltibizArguments)
    {
      builder.AddMessaging();
    }

    if (arguments is OzdsFakeBypassArguments bypass)
    {
      builder.AddClient(bypass.Timeout_s);
    }

    return builder;
  }

  private static IHostApplicationBuilder AddFaking(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo<IModelFaker>();
    builder.Services.AddSingleton<ModelFaker>();
    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsFakeOptions>();
    var relativeServerSettings = builder.Configuration
      .GetSection("Ozds:Fake:ServerSettings")
      .Get<string>();
    if (relativeServerSettings is not null)
    {
      var serverSettings = Path.GetFullPath(
        relativeServerSettings,
        builder.Environment.ContentRootPath);
      var directory = Path.GetDirectoryName(serverSettings)
        ?? throw new InvalidOperationException(
          "ServerSettings must be a file path");
      var file = Path.GetFileName(serverSettings);
      var source = new JsonConfigurationSource
      {
        FileProvider = new PhysicalFileProvider(directory),
        Path = file
      };
      builder.Configuration.Sources.Insert(0, source);
    }

    return builder;
  }

  private static IHostApplicationBuilder AddGenerators(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(
      typeof(IMeasurementRecordGenerator));
    builder.Services.AddSingleton(typeof(MeasurementRecordGenerator));

    return builder;
  }

  private static IHostApplicationBuilder AddCloners(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IMeasurementCloner));
    builder.Services.AddSingleton(typeof(MeasurementCloner));
    return builder;
  }

  private static IHostApplicationBuilder AddLoaders(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(ILoader));
    builder.Services.AddSingleton(typeof(ResourceCache));
    return builder;
  }

  private static IHostApplicationBuilder AddConversion(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(
      typeof(IMeasurementRecordModelConverter));
    builder.Services.AddSingleton(
      typeof(MeasurementRecordConverter));
    return builder;
  }

  private static IHostApplicationBuilder AddCorrection(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(
      typeof(IRecordCorrector));
    builder.Services.AddSingleton(typeof(RecordCorrector));
    return builder;
  }

  private static IHostApplicationBuilder AddPacking(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(
      typeof(IMessengerPushRequestPacker));
    builder.Services.AddSingleton(typeof(MessengerPushRequestPacker));
    return builder;
  }

  private static IHostApplicationBuilder AddWorkers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IWorker));
    return builder;
  }

  private static IHostApplicationBuilder AddClient(
    this IHostApplicationBuilder builder,
    int timeout_s
  )
  {
    builder.Services.AddHttpClient(
      PushClient.Name,
      (services, options) =>
      {
        var clientOptions = services
          .GetRequiredService<IOptions<OzdsFakeOptions>>().Value.Client;

        options.Timeout = TimeSpan.FromSeconds(timeout_s);
        options.BaseAddress = new Uri(clientOptions.BaseUrl);
      });
    builder.Services.AddScoped(typeof(PushClient));
    builder.Services.AddScoped(typeof(InsertClient));
    return builder;
  }

  private static IHostApplicationBuilder AddMessaging(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddMassTransit(
      x =>
      {
        var fakeAssembly = typeof(HostExtensions).Assembly;
        var messagingAssembly = typeof(MessagingDbContext).Assembly;

        x.SetKebabCaseEndpointNameFormatter();

        x.AddConsumers(fakeAssembly);
        x.AddSagaStateMachines(fakeAssembly);
        x.AddActivities(fakeAssembly);

        x.AddSagas(messagingAssembly);
        x.SetInMemorySagaRepositoryProvider();

        var connectionString = ConfigureOzdsFakeOptions
          .ParseConnectionString(builder.Configuration);
        if (connectionString is OzdsMessagingParsedRabbitMqConnectionString
          rabbitMqConnectionString)
        {
          x.UsingRabbitMq(
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
        else
        {
          throw new InvalidOperationException(
            "Only RabbitMQ is supported");
        }
      });

    return builder;
  }
}
