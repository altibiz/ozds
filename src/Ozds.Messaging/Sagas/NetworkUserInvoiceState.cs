using MassTransit;

namespace Ozds.Messaging.Sagas;

public class NetworkUserInvoiceState : SagaStateMachineInstance
{
  public string CurrentState { get; set; } = default!;

  public uint RowVersion { get; set; }

  public string NetworkUserInvoiceId { get; set; } = default!;

  public string? BillId { get; set; }

  public Guid CorrelationId { get; set; } = Guid.Empty;
}
