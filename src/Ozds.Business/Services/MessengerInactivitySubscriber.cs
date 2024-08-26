using System.Threading.Channels;
using Ozds.Business.Notifications.Abstractions;
using Ozds.Jobs.Observers.Abstractions;

namespace Ozds.Business.Services;

public class MessengerInactivitySubscriber(
  IMessengerJobSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
  private readonly Channel<string> inactive =
    Channel.CreateUnbounded<string>();

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.OnInactivity += OnInactivity;
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.OnInactivity -= OnInactivity;
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      var id = await inactive.Reader.ReadAsync(stoppingToken);
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      var sender = scope.ServiceProvider.GetRequiredService<INotificationSender>();
    }
  }

  private void OnInactivity(object? sender, string id)
  {
    inactive.Writer.TryWrite(id);
  }
}
