using System.Threading.Channels;
using Ozds.Business.Mutations;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;
using Ozds.Messaging.Sagas;

namespace Ozds.Business.Services;

public class NetworkUserInvoiceStateSubscriber(
  INetworkUserInvoiceStateSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
  private readonly Channel<NetworkUserInvoiceState> registered =
    Channel.CreateUnbounded<NetworkUserInvoiceState>();

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.Subscribe(OnRegistered);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.Unsubscribe(OnRegistered);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      var state = await registered.Reader.ReadAsync(stoppingToken);
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      var mutations = scope.ServiceProvider.GetRequiredService<OzdsNetworkUserInvoiceMutations>();
      await mutations.UpdateBillId(state.NetworkUserInvoiceId, state.BillId!);
    }
  }

  public void OnRegistered(object? sender, NetworkUserInvoiceStateEventArgs state)
  {
    registered.Writer.TryWrite(state.State);
  }
}
