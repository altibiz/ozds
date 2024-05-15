using MassTransit;
using Ozds.Business.Mutations;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Sagas;

namespace Ozds.Business.Sagas;

public class NetworkUserInvoiceRegisteredActivity(
  OzdsNetworkUserInvoiceMutations mutations
)
  : IStateMachineActivity<
    NetworkUserInvoiceState,
    IRegisterNetworkUserInvoice
  >
{
  private readonly OzdsNetworkUserInvoiceMutations _mutations = mutations;

  public async Task Execute(
    BehaviorContext<
      NetworkUserInvoiceState,
      IRegisterNetworkUserInvoice> context,
    IBehavior<
      NetworkUserInvoiceState,
      IRegisterNetworkUserInvoice> next)
  {
    await _mutations.UpdateBillId(
      context.Saga.NetworkUserInvoiceId,
      context.Message.BillId
    );

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
