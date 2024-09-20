using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Time;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Workers;

public class MonthlyNetworkUserInvoiceReactor(
  IServiceScopeFactory serviceScopeFactory,
  IBillingJobSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<NetworkUserInvoiceEventArgs> channel =
    Channel.CreateUnbounded<NetworkUserInvoiceEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeNetworkUserInvoiceCreate(OnNetworkUserCreate);

    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeNetworkUserInvoiceCreate(OnNetworkUserCreate);

    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnNetworkUserCreate(
    object? sender,
    NetworkUserInvoiceEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    NetworkUserInvoiceEventArgs eventArgs)
  {
    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var networkUser = (await context.Messengers
        .Where(context.PrimaryKeyEquals<MessengerEntity>(eventArgs.NetworkUserId))
        .FirstOrDefaultAsync())
      ?.ToModel();
    if (networkUser is null)
    {
      return;
    }

    var issuer = serviceProvider
      .GetRequiredService<INetworkUserInvoiceIssuer>();

    var (startOfLastMonth, endOfLastMonth) = DateTimeOffset.UtcNow
      .GetStartOfLastMonth()
      .GetMonthRange();
    await issuer.IssueNetworkUserInvoiceAsync(
      networkUser.Id,
      startOfLastMonth,
      endOfLastMonth
    );
  }
}
