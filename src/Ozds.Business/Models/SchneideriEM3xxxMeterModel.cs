using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record SchneideriEM3xxxMeterModel(
  string Id,
  string Title,
  DateTimeOffset CreationDate,
  string? CreatedById,
  DateTimeOffset? LastUpdateDate,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletionDate,
  string? DeletedById,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId,
  string? MessengerId,
  float ConnectionPower_W,
  List<PhaseModel> Phases
) : MeterModel(
  Id,
  Title,
  CreationDate,
  CreatedById,
  LastUpdateDate,
  LastUpdatedById,
  IsDeleted,
  DeletionDate,
  DeletedById,
  NetworkUserMeasurementLocationId,
  LocationMeasurementLocationId,
  MessengerId,
  ConnectionPower_W,
  Phases
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
  public static SchneideriEM3xxxMeterEntity ToEntity(this SchneideriEM3xxxMeterModel model)
  {
    return new SchneideriEM3xxxMeterEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreationDate,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdateDate,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletionDate,
      DeletedById = model.DeletedById,
      NetworkUserMeasurementLocationId = model.NetworkUserMeasurementLocationId,
      LocationMeasurementLocationId = model.LocationMeasurementLocationId,
      MessengerId = model.MessengerId,
      ConnectionPower_W = model.ConnectionPower_W,
      Phases = model.Phases.Select(phase => phase.ToEntity()).ToList()
    };
  }

  public static SchneideriEM3xxxMeterModel ToModel(this SchneideriEM3xxxMeterEntity entity)
  {
    return new SchneideriEM3xxxMeterModel(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.NetworkUserMeasurementLocationId,
      entity.LocationMeasurementLocationId,
      entity.MessengerId,
      entity.ConnectionPower_W,
      entity.Phases.Select(phase => phase.ToModel()).ToList()
    );
  }
}
