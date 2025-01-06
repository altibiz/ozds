using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Time;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Workers;

public class MonthlyNetworkUserInvoiceReactor(
  IBillingJobSubscriber subscriber,
  IServiceProvider serviceProvider
) : BackgroundService, IReactor
{
  private readonly Channel<BillingJobEventArgs> channel =
    Channel.CreateUnbounded<BillingJobEventArgs>();

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
      using var scope = serviceProvider.CreateScope();
      var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataDbContext>>();
      var issuer = scope.ServiceProvider.GetRequiredService<INetworkUserInvoiceIssuer>();
      await using var context = await contextFactory.CreateDbContextAsync(stoppingToken);
      await Handle(context, issuer, eventArgs);
    }
  }

  private void OnNetworkUserCreate(
    object? sender,
    BillingJobEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    DataDbContext context,
    INetworkUserInvoiceIssuer issuer,
    BillingJobEventArgs eventArgs)
  {
    var networkUser = (await context.NetworkUsers
        .Where(
          context.PrimaryKeyEquals<NetworkUserEntity>(eventArgs.NetworkUserId))
        .FirstOrDefaultAsync())
      ?.ToModel();
    if (networkUser is null)
    {
      return;
    }

    var now = DateTimeOffset.UtcNow;
    var startOfLastMonth = now.GetStartOfLastMonth();
    var startOfThisMonth = now.GetStartOfMonth();
    await issuer.IssueNetworkUserInvoiceAsync(
      networkUser.Id,
      startOfLastMonth,
      startOfThisMonth
    );
  }
}
