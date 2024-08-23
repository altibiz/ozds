using OrchardCore.Environment.Shell;
using OrchardCore.Modules;

namespace Ozds.Server.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsServer(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var hostedServices = services.Where(
      service => service.ServiceType.IsAssignableTo(typeof(IHostedService))
    );

    foreach (var hostedService in hostedServices)
    {
      services.Remove(hostedService);
      var modularTenantEvents = new ServiceDescriptor(
        typeof(IModularTenantEvents),
        typeof(HostedServiceModularTenantEvents<>)
          .MakeGenericType(
            hostedService.ImplementationType ?? hostedService.ServiceType),
        hostedService.Lifetime
      );
      services.Add(modularTenantEvents);
    }

    return services;
  }

  public static IApplicationBuilder UseOzdsServer(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
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
