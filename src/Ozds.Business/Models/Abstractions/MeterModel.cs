using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public abstract record MeterModel(
  string Id,
  bool IsDeleted,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId
) : AuditableModel(Id, IsDeleted), IMeter
{
  public abstract ICapabilities Capabilities { get; }
}
