using System.Threading.Channels;
using Ozds.Business.Mutations;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class NetworkUserInvoiceStateReactor(
  INetworkUserInvoiceStateSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory
) : BackgroundService, IReactor
{
  private readonly Channel<NetworkUserInvoiceStateEventArgs> channel =
    Channel.CreateUnbounded<NetworkUserInvoiceStateEventArgs>();

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
    await foreach (var state in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, state);
    }
  }

  public void OnRegistered(
    object? sender,
    NetworkUserInvoiceStateEventArgs state)
  {
    channel.Writer.TryWrite(state);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    NetworkUserInvoiceStateEventArgs eventArgs)
  {
    var mutations = serviceProvider
      .GetRequiredService<OzdsNetworkUserInvoiceMutations>();
    await mutations.UpdateBillId(
      eventArgs.State.NetworkUserInvoiceId, eventArgs.State.BillId!);
  }
}
