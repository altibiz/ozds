namespace Ozds.Business.Models.Base;

public abstract class MeasurementLocationModel : AuditableModel
{
  public required string MeterId { get; init; }
}
