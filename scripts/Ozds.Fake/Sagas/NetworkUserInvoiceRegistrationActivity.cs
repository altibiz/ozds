using MassTransit;
using Ozds.Messaging.Sagas;

namespace Ozds.Fake.Sagas;

public class NetworkUserInvoiceRegistrationActivity(
  ILogger<NetworkUserInvoiceRegistrationActivity> logger
)
  : IStateMachineActivity<NetworkUserInvoiceState>
{
  private readonly ILogger _logger = logger;

  public async Task Execute(
    BehaviorContext<NetworkUserInvoiceState> context,
    IBehavior<NetworkUserInvoiceState> next
  )
  {
    await RegisterNetworkUserInvoice(context.Saga);

    await next.Execute(context).ConfigureAwait(false);
  }

  public async Task Execute<T>(
    BehaviorContext<NetworkUserInvoiceState, T> context,
    IBehavior<NetworkUserInvoiceState, T> next
  )
    where T : class
  {
    await RegisterNetworkUserInvoice(context.Saga);

    await next.Execute(context).ConfigureAwait(false);
  }

  public Task Faulted<TException>(
    BehaviorExceptionContext<NetworkUserInvoiceState, TException> context,
    IBehavior<NetworkUserInvoiceState> next
  )
    where TException : Exception
  {
    return next.Execute(context);
  }

  public Task Faulted<T, TException>(
    BehaviorExceptionContext<NetworkUserInvoiceState, T, TException> context,
    IBehavior<NetworkUserInvoiceState, T> next
  )
    where T : class
    where TException : Exception
  {
    return next.Execute(context);
  }

  public void Probe(ProbeContext context)
  {
    context.CreateScope("network-user-issue-registration");
  }

  public void Accept(StateMachineVisitor visitor)
  {
    visitor.Visit(this);
  }

  private async Task RegisterNetworkUserInvoice(NetworkUserInvoiceState saga)
  {
    await Task.Delay(1000); // NOTE: Simulate fetching
    _logger.LogInformation(
      "Network user invoice acknowledged: {NetworkUserInvoiceId}",
      saga.NetworkUserInvoiceId
    );

    if (Random.Shared.Next(0, 10) != 0)
    {
      await Task.Delay(1000); // NOTE: Simulate writing
      saga.BillId = Guid.NewGuid().ToString();
      _logger.LogInformation(
        "Network user invoice {NetworkUserInvoiceId} registration ID generated: {RegistrationId}",
        saga.NetworkUserInvoiceId,
        saga.BillId
      );
    }
    else
    {
      _logger.LogInformation(
        "Network user invoice {NetworkUserInvoiceId} cancelled",
        saga.NetworkUserInvoiceId
      );
    }
  }
}
