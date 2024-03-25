using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record MeterModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string MeasurementLocationId,
  string CatalogueId,
  string MessengerId,
  string MeasurementValidatorId,
  float ConnectionPower_W,
  List<PhaseModel> Phases
) : AuditableModel(
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById
), IMeter
{
  public abstract ICapabilities Capabilities { get; }
}
