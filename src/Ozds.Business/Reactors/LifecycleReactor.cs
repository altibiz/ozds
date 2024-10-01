using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;

namespace Ozds.Business.Reactors;

// TODO: startup after migrations

public class LifecycleReactor(
  IDbContextFactory<DataDbContext> factory,
  AgnosticModelActivator activator
) : IReactor
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    // await using var context =
    //   await factory.CreateDbContextAsync(cancellationToken);
    // var content = new StartupEventContent();
    // var @event = CreateEvent(content);
    // var eventEntity = @event.ToEntity();
    // context.Add(eventEntity);
    // await context.SaveChangesAsync(cancellationToken);
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    await using var context =
      await factory.CreateDbContextAsync(cancellationToken);
    var content = new ShutdownEventContent();
    var @event = CreateEvent(content);
    var eventEntity = @event.ToEntity();
    context.Add(eventEntity);
    await context.SaveChangesAsync(cancellationToken);
  }

  private SystemEventModel CreateEvent(LifecycleEventContent content)
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

#pragma warning disable S2094 // Classes should not be empty
  private record LifecycleEventContent(string Message);

  private sealed record StartupEventContent()
    : LifecycleEventContent("Startup");

  private sealed record ShutdownEventContent()
    : LifecycleEventContent("Shutdown");
#pragma warning restore S2094 // Classes should not be empty
}
