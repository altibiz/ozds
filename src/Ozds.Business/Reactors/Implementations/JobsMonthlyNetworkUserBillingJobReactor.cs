using Ozds.Business.Models;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors.Implementations;

public class JobsMonthlyNetworkUserBillingJobReactor(
  IServiceProvider serviceProvider
) : Reactor<
  JobsBillingJobEventArgs,
  IJobsBillingJobSubscriber,
  JobsMonthlyNetworkUserBillingJobHandler>(serviceProvider)
{
}

public class JobsMonthlyNetworkUserBillingJobHandler(
  AuditableQueries auditableQueries,
  NetworkUserInvoiceIssuer issuer,
  ClockQueries clockQueries,
  TimeQueries timeQueries
) : Handler<JobsBillingJobEventArgs>
{
  public override async Task Handle(
    JobsBillingJobEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var networkUser = await auditableQueries
      .ReadSingle<NetworkUserModel>(
        eventArgs.NetworkUserId,
        cancellationToken);
    if (networkUser is null)
    {
      return;
    }

    var now = clockQueries.Timestamp();
    var startOfLastMonth = timeQueries.GetStartOfLastMonth(now);
    var startOfThisMonth = timeQueries.GetStartOfMonth(now);
    await issuer.IssueNetworkUserInvoiceAsync(
      networkUser.Id,
      startOfLastMonth,
      startOfThisMonth,
      cancellationToken
    );
  }
}
