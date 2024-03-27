using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record AbbB2xMeterModel(
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
    get { return new AbbB2xCapabilities(); }
  }
}

public static class AbbB2xMeterModelExtensions
{
  public static AbbB2xMeterEntity ToEntity(this AbbB2xMeterModel model)
  {
    return new AbbB2xMeterEntity
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
      Phases = model.Phases.Select(p => p.ToEntity()).ToList()
    };
  }

  public static AbbB2xMeterModel ToModel(this AbbB2xMeterEntity entity)
  {
    return new AbbB2xMeterModel(
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
      entity.Phases.Select(p => p.ToModel()).ToList()
    );
  }
}
