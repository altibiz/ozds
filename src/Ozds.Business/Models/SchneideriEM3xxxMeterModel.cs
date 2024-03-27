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
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById,
  MeasurementLocationId,
  CatalogueId,
  MessengerId,
  MeasurementValidatorId,
  ConnectionPower_W,
  Phases
)
{
  public override ICapabilities Capabilities
  {
    get { return new SchneideriEM3xxxCapabilities(); }
  }
}

public static class SchneideriEM3xxxMeterModelExtensions
{
  public static SchneideriEM3xxxMeterEntity ToEntity(
    this SchneideriEM3xxxMeterModel model)
  {
    return new SchneideriEM3xxxMeterEntity
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
  }

  public static SchneideriEM3xxxMeterModel ToModel(
    this SchneideriEM3xxxMeterEntity entity)
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
      entity.MeasurementLocationId,
      entity.CatalogueId,
      entity.MessengerId,
      entity.MeasurementValidatorId,
      entity.ConnectionPower_W,
      entity.Phases.Select(phase => phase.ToModel()).ToList()
    );
  }
}
