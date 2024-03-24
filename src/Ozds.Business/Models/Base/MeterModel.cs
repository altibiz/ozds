using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record MeterModel(
  string Id,
  string Title,
  DateTimeOffset CreationDate,
  string? CreatedById,
  DateTimeOffset? LastUpdateDate,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletionDate,
  string? DeletedBy,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId,
  string? MessengerId,
  float ConnectionPower_W,
  List<PhaseModel> Phases
) : AuditableModel(
  Id,
  Title,
  CreationDate,
  CreatedById,
  LastUpdateDate,
  LastUpdatedById,
  IsDeleted,
  DeletionDate,
  DeletedBy
), IMeter
{
  public abstract ICapabilities Capabilities { get; }
}
