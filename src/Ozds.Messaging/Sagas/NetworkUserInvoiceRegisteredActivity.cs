using MassTransit;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Sagas;

public class NetworkUserInvoiceRegisteredActivity(
  INetworkUserInvoiceStatePublisher publisher
)
  : IStateMachineActivity<
    NetworkUserInvoiceStateEntity,
    IRegisterNetworkUserInvoice
  >
{
  public async Task Execute(
    BehaviorContext<
      NetworkUserInvoiceStateEntity,
      IRegisterNetworkUserInvoice> context,
    IBehavior<
      NetworkUserInvoiceStateEntity,
      IRegisterNetworkUserInvoice> next)
  {
    publisher.Publish(new NetworkUserInvoiceStateEventArgs
    {
      State = context.Saga
    });

    await next.Execute(context);
  }

  public Task Faulted<TException>(
    BehaviorExceptionContext<
      NetworkUserInvoiceStateEntity,
      IRegisterNetworkUserInvoice,
      TException> context,
    IBehavior<
      NetworkUserInvoiceStateEntity,
      IRegisterNetworkUserInvoice> next
  )
    where TException : Exception
  {
    return next.Faulted(context);
  }

  public void Probe(ProbeContext context)
  {
    context.CreateScope("network-user-issue-registration");
  }

  public void Accept(StateMachineVisitor visitor)
  {
    visitor.Visit(this);
  }
}
