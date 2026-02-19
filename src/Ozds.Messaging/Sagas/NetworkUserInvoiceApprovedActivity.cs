using MassTransit;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Sagas;

public class NetworkUserInvoiceApprovedActivity(
  INetworkUserInvoiceStatePublisher publisher
)
  : IStateMachineActivity<
    NetworkUserInvoiceStateEntity,
    IApproveNetworkUserInvoice
  >
{
  public async Task Execute(
    BehaviorContext<
      NetworkUserInvoiceStateEntity,
      IApproveNetworkUserInvoice
    > context,
    IBehavior<NetworkUserInvoiceStateEntity, IApproveNetworkUserInvoice> next
  )
  {
    publisher.Publish(
      new NetworkUserInvoiceStateEventArgs { State = context.Saga }
    );

    await next.Execute(context);
  }

  public Task Faulted<TException>(
    BehaviorExceptionContext<
      NetworkUserInvoiceStateEntity,
      IApproveNetworkUserInvoice,
      TException
    > context,
    IBehavior<NetworkUserInvoiceStateEntity, IApproveNetworkUserInvoice> next
  )
    where TException : Exception
  {
    return next.Faulted(context);
  }

  public void Probe(ProbeContext context)
  {
    context.CreateScope("network-user-issue-approval");
  }

  public void Accept(StateMachineVisitor visitor)
  {
    visitor.Visit(this);
  }
}
