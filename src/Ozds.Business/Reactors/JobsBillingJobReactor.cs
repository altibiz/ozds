using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries.Agnostic;
using Ozds.Business.Reactors.Base;
using Ozds.Business.Time;

namespace Ozds.Business.Reactors;

public class JobsBillingJobReactor(
  IServiceProvider serviceProvider
) : Reactor<
  JobsBillingJobEventArgs,
  IJobsBillingJobSubscriber,
  JobsBillingJobHandler>(serviceProvider)
{
}

public class JobsBillingJobHandler(
  AuditableQueries auditableQueries,
  INetworkUserInvoiceIssuer issuer
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

    var now = DateTimeOffset.UtcNow;
    var startOfLastMonth = now.GetStartOfLastMonth();
    var startOfThisMonth = now.GetStartOfMonth();
    await issuer.IssueNetworkUserInvoiceAsync(
      networkUser.Id,
      startOfLastMonth,
      startOfThisMonth,
      cancellationToken
    );
  }
}
