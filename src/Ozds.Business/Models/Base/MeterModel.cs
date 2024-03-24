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
  Id: Id,
  Title: Title,
  CreatedOn: CreatedOn,
  CreatedById: CreatedById,
  LastUpdatedOn: LastUpdatedOn,
  LastUpdatedById: LastUpdatedById,
  IsDeleted: IsDeleted,
  DeletedOn: DeletedOn,
  DeletedById: DeletedById
), IMeter
{
  public abstract ICapabilities Capabilities { get; }
}
