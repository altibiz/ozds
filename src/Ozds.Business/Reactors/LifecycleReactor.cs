using System.Text.Json;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors;

// TODO: startup after migrations

public class LifecycleReactor(
  IServiceScopeFactory scopeFactory
) : IReactor
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public async Task StartAsync(CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
#pragma warning disable S125 // Sections of code should not be commented out
    // await using var context =
    //   await factory.CreateDbContextAsync(cancellationToken);
    // var content = new StartupEventContent();
    // var @event = CreateEvent(content);
    // var eventEntity = @event.ToEntity();
    // context.Add(eventEntity);
    // await context.SaveChangesAsync(cancellationToken);
#pragma warning restore S125 // Sections of code should not be commented out
  }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    await using var scope = scopeFactory.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<ReadonlyMutations>();
    var activator = scope.ServiceProvider
      .GetRequiredService<AgnosticModelActivator>();
    var content = new ShutdownEventContent();
    var @event = CreateEvent(content, activator);
    await mutations.Create(@event, cancellationToken);
  }

  private SystemEventModel CreateEvent(
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

#pragma warning disable S125 // Sections of code should not be commented out
  // private sealed record StartupEventContent()
  //   : LifecycleEventContent("Startup");
#pragma warning restore S125 // Sections of code should not be commented out
}
