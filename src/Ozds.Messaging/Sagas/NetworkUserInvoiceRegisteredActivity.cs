using MassTransit;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Observers.Abstractions;

namespace Ozds.Messaging.Sagas;

public class NetworkUserInvoiceRegisteredActivity(
  INetworkUserInvoiceStatePublisher publisher
)
  : IStateMachineActivity<
    NetworkUserInvoiceState,
    IRegisterNetworkUserInvoice
  >
{
  public async Task Execute(
    BehaviorContext<
      NetworkUserInvoiceState,
      IRegisterNetworkUserInvoice> context,
    IBehavior<
      NetworkUserInvoiceState,
      IRegisterNetworkUserInvoice> next)
  {
    publisher.PublishRegistered(context.Saga);

    await next.Execute(context).ConfigureAwait(false);
  }

  public Task Faulted<TException>(
    BehaviorExceptionContext<
      NetworkUserInvoiceState,
      IRegisterNetworkUserInvoice,
      TException> context,
    IBehavior<
      NetworkUserInvoiceState,
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
