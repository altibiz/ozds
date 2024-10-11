using MassTransit;

namespace Ozds.Messaging.Entities;

public class NetworkUserInvoiceStateEntity : SagaStateMachineInstance
{
  public string CurrentState { get; set; } = default!;

  public uint RowVersion { get; set; }

  public string NetworkUserInvoiceId { get; set; } = default!;

  public string? BillId { get; set; }

  public string? AbortReason { get; set; }

  public Guid CorrelationId { get; set; } = Guid.Empty;
}
