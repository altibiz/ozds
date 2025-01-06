using MassTransit;
using Ozds.Messaging.Entities;

namespace Ozds.Fake.Sagas;

public class NetworkUserInvoiceRegistrationActivity(
  ILogger<NetworkUserInvoiceRegistrationActivity> logger
)
  : IStateMachineActivity<NetworkUserInvoiceStateEntity>
{
  private readonly ILogger _logger = logger;

  public async Task Execute(
    BehaviorContext<NetworkUserInvoiceStateEntity> context,
    IBehavior<NetworkUserInvoiceStateEntity> next
  )
  {
    await RegisterNetworkUserInvoice(context.Saga);

    await next.Execute(context);
  }

  public async Task Execute<T>(
    BehaviorContext<NetworkUserInvoiceStateEntity, T> context,
    IBehavior<NetworkUserInvoiceStateEntity, T> next
  )
    where T : class
  {
    await RegisterNetworkUserInvoice(context.Saga);

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
    BehaviorExceptionContext<NetworkUserInvoiceStateEntity, T, TException>
      context,
    IBehavior<NetworkUserInvoiceStateEntity, T> next
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

  private async Task RegisterNetworkUserInvoice(
    NetworkUserInvoiceStateEntity saga)
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
      saga.AbortReason = "Fake aborted";
      _logger.LogInformation(
        "Network user invoice {NetworkUserInvoiceId} aborted",
        saga.NetworkUserInvoiceId
      );
    }
  }
}
