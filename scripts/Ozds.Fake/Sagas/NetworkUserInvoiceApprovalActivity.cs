using MassTransit;
using Ozds.Messaging.Entities;

namespace Ozds.Fake.Sagas;

public class NetworkUserInvoiceApprovalActivity(
  ILogger<NetworkUserInvoiceApprovalActivity> logger
) : IStateMachineActivity<NetworkUserInvoiceStateEntity>
{
  private readonly ILogger _logger = logger;

  public async Task Execute(
    BehaviorContext<NetworkUserInvoiceStateEntity> context,
    IBehavior<NetworkUserInvoiceStateEntity> next
  )
  {
    await ApproveNetworkUserInvoice(context.Saga);

    await next.Execute(context);
  }

  public async Task Execute<T>(
    BehaviorContext<NetworkUserInvoiceStateEntity, T> context,
    IBehavior<NetworkUserInvoiceStateEntity, T> next
  )
    where T : class
  {
    await ApproveNetworkUserInvoice(context.Saga);

    await next.Execute(context);
  }

  public Task Faulted<TException>(
    BehaviorExceptionContext<NetworkUserInvoiceStateEntity, TException> context,
    IBehavior<NetworkUserInvoiceStateEntity> next
  )
    where TException : Exception
  {
    return next.Execute(context);
  }

  public Task Faulted<T, TException>(
    BehaviorExceptionContext<
      NetworkUserInvoiceStateEntity,
      T,
      TException
    > context,
    IBehavior<NetworkUserInvoiceStateEntity, T> next
  )
    where T : class
    where TException : Exception
  {
    return next.Execute(context);
  }

  public void Probe(ProbeContext context)
  {
    context.CreateScope("network-user-issue-approval");
  }

  public void Accept(StateMachineVisitor visitor)
  {
    visitor.Visit(this);
  }

  private async Task ApproveNetworkUserInvoice(
    NetworkUserInvoiceStateEntity saga
  )
  {
    if (saga.Approved)
    {
      _logger.LogInformation(
        "Network user invoice automatically approved: {NetworkUserInvoiceId}",
        saga.NetworkUserInvoiceId
      );
      return;
    }

    await Task.Delay(1000); // NOTE: Simulate fetching
    _logger.LogInformation(
      "Network user invoice registered: {NetworkUserInvoiceId}",
      saga.NetworkUserInvoiceId
    );

    if (Random.Shared.Next(0, 10) != 0)
    {
      await Task.Delay(1000); // NOTE: Simulate writing
      saga.Approved = true;
      _logger.LogInformation(
        "Network user invoice {NetworkUserInvoiceId} approved",
        saga.NetworkUserInvoiceId
      );
    }
    else
    {
      saga.Approved = false;
      _logger.LogInformation(
        "Network user invoice {NetworkUserInvoiceId} disapproved",
        saga.NetworkUserInvoiceId
      );
    }
  }
}
