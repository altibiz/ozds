using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementLocationModel :
  AuditableModel,
  IMeasurementLocation
{
  public required string MeterId { get; set; }
}
