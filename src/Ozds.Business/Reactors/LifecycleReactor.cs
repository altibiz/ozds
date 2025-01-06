using System.Text.Json;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors;

public class LifecycleReactor(
  IServiceScopeFactory scopeFactory
) : IReactor
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await using var scope = scopeFactory.CreateAsyncScope();
    try
    {
      var mutations = scope.ServiceProvider
        .GetRequiredService<ReadonlyMutations>();
      var activator = scope.ServiceProvider
        .GetRequiredService<AgnosticModelActivator>();
      var content = new StartupEventContent();
      var @event = CreateEvent(content, activator);
      await mutations.Create(@event, cancellationToken);
    }
    catch (Exception ex)
    {
      var logger = scope.ServiceProvider
        .GetRequiredService<ILogger<LifecycleReactor>>();
      logger.LogError(ex, "Failed to create startup event");
    }
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    await using var scope = scopeFactory.CreateAsyncScope();
    try
    {
      var mutations = scope.ServiceProvider
        .GetRequiredService<ReadonlyMutations>();
      var activator = scope.ServiceProvider
        .GetRequiredService<AgnosticModelActivator>();
      var content = new ShutdownEventContent();
      var @event = CreateEvent(content, activator);
      await mutations.Create(@event, cancellationToken);
    }
    catch (Exception ex)
    {
      var logger = scope.ServiceProvider
        .GetRequiredService<ILogger<LifecycleReactor>>();
      logger.LogError(ex, "Failed to create shutdown event");
    }
  }

  private static SystemEventModel CreateEvent(
    LifecycleEventContent content,
    AgnosticModelActivator activator)
  {
    var @event = activator.Activate<SystemEventModel>();
    @event.Title = content.Message;
    @event.Timestamp = DateTimeOffset.UtcNow;
    @event.Content = JsonSerializer.SerializeToDocument(content);
    @event.Level = LevelModel.Information;
    @event.Categories = new List<CategoryModel>
    {
      CategoryModel.All,
      CategoryModel.Lifecycle
    };
    return @event;
  }

  private record LifecycleEventContent(string Message);

  private sealed record ShutdownEventContent()
    : LifecycleEventContent("Shutdown");

  private sealed record StartupEventContent()
    : LifecycleEventContent("Startup");
}
