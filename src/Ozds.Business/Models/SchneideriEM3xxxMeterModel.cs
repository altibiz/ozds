using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record SchneideriEM3xxxMeterModel(
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
) : MeterModel(
  Id: Id,
  Title: Title,
  CreatedOn: CreatedOn,
  CreatedById: CreatedById,
  LastUpdatedOn: LastUpdatedOn,
  LastUpdatedById: LastUpdatedById,
  IsDeleted: IsDeleted,
  DeletedOn: DeletedOn,
  DeletedById: DeletedById,
  MeasurementLocationId: MeasurementLocationId,
  CatalogueId: CatalogueId,
  MessengerId: MessengerId,
  MeasurementValidatorId: MeasurementValidatorId,
  ConnectionPower_W: ConnectionPower_W,
  Phases: Phases
)
{
  public override ICapabilities Capabilities => new SchneideriEM3xxxCapabilities();

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class SchneideriEM3xxxMeterModelExtensions
{
  public static SchneideriEM3xxxMeterEntity ToEntity(this SchneideriEM3xxxMeterModel model) =>
    new()
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      MeasurementLocationId = model.MeasurementLocationId,
      CatalogueId = model.CatalogueId,
      MessengerId = model.MessengerId,
      MeasurementValidatorId = model.MeasurementValidatorId,
      ConnectionPower_W = model.ConnectionPower_W,
      Phases = model.Phases.Select(phase => phase.ToEntity()).ToList()
    };

  public static SchneideriEM3xxxMeterModel ToModel(this SchneideriEM3xxxMeterEntity entity) =>
    new(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.MeasurementLocationId,
      entity.CatalogueId,
      entity.MessengerId,
      entity.MeasurementValidatorId,
      entity.ConnectionPower_W,
      entity.Phases.Select(phase => phase.ToModel()).ToList()
    );
}
