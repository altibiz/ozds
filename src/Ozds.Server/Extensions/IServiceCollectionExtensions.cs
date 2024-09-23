using OrchardCore.Environment.Shell;
using OrchardCore.Modules;

// TODO: finer control over what services are hosted

namespace Ozds.Server.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsServer(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var hostedServices = services
      .Where(
        service =>
          service.ServiceType == typeof(IHostedService) &&
          service.Lifetime == ServiceLifetime.Singleton)
      .Where(
        service =>
          !(service.ImplementationInstance?.GetType().Namespace?.StartsWith(nameof(Microsoft))
          ?? service.ImplementationType?.Namespace?.StartsWith(nameof(Microsoft))
          ?? service.ImplementationFactory?.Method?.Module.Name?.StartsWith(nameof(Microsoft))
          ?? false)
          && !(service.ImplementationInstance?.GetType().Namespace?.StartsWith(nameof(OrchardCore))
            ?? service.ImplementationType?.Namespace?.StartsWith(nameof(OrchardCore))
            ?? service.ImplementationFactory?.Method.Module.Name?.StartsWith(nameof(OrchardCore))
            ?? false)
      )
      .ToList();

    foreach (var hostedService in hostedServices)
    {
      services.Remove(hostedService);
      var modularTenantEvents =
        hostedService.ImplementationFactory is { } factory
          ? new ServiceDescriptor(
            typeof(IModularTenantEvents),
            services => ActivatorUtilities.CreateInstance(
              services,
              typeof(HostedServiceModularTenantEvents<>)
                .MakeGenericType(
                  hostedService.ImplementationType ??
                  hostedService.ServiceType),
              factory(services)),
            ServiceLifetime.Singleton
          )
          : new ServiceDescriptor(
            typeof(IModularTenantEvents),
            services => ActivatorUtilities.CreateInstance(
              services,
              typeof(HostedServiceModularTenantEvents<>)
                .MakeGenericType(
                  hostedService.ImplementationType ??
                  hostedService.ServiceType),
              ActivatorUtilities.CreateInstance(
                services,
                hostedService.ImplementationType ??
                hostedService.ServiceType)),
            ServiceLifetime.Singleton
          );
      services.Add(modularTenantEvents);
    }

    return services;
  }

  private sealed class HostedServiceModularTenantEvents<THostedService>(
    THostedService hostedService,
    ILogger<HostedServiceModularTenantEvents<THostedService>> logger,
    ShellSettings shellSettings
  ) : ModularTenantEvents, IDisposable, IAsyncDisposable
    where THostedService : IHostedService
  {
    private bool _disposed;
    private bool _started;

    public async ValueTask DisposeAsync()
    {
      if (hostedService is not IAsyncDisposable asyncDisposable)
      {
        return;
      }

      if (_disposed)
      {
        _disposed = true;
        return;
      }

      if (_started)
      {
        await hostedService.StopAsync(CancellationToken.None);
        _started = false;
      }

      await asyncDisposable.DisposeAsync();
      _disposed = true;
    }

    public void Dispose()
    {
      if (hostedService is not IDisposable disposable)
      {
        return;
      }

      if (_disposed)
      {
        _disposed = true;
        return;
      }

      if (_started)
      {
        hostedService
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
        hostedService
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

      await hostedService
        .StartAsync(CancellationToken.None)
        .ConfigureAwait(false);

      _started = true;
    }

    public override async Task TerminatingAsync()
    {
      ObjectDisposedException.ThrowIf(
        _disposed,
        hostedService
      );

      if (!_started)
      {
        return;
      }

      logger.LogInformation(
        "Stopping hosted service '{HostedService}' for tenant '{TenantName}'.",
        typeof(THostedService).Name,
        shellSettings.Name
      );

      await hostedService
        .StopAsync(CancellationToken.None)
        .ConfigureAwait(false);

      _started = false;
    }
  }
}
