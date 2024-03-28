using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class AbbB2xMeterModel : MeterModel
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
    return new AbbB2xMeterModel
    {
      Id = entity.Id,
      Title = entity.Title,
      CreatedOn = entity.CreatedOn,
      CreatedById = entity.CreatedById,
      LastUpdatedOn = entity.LastUpdatedOn,
      LastUpdatedById = entity.LastUpdatedById,
      IsDeleted = entity.IsDeleted,
      DeletedOn = entity.DeletedOn,
      DeletedById = entity.DeletedById,
      MeasurementLocationId = entity.MeasurementLocationId,
      CatalogueId = entity.CatalogueId,
      MessengerId = entity.MessengerId,
      MeasurementValidatorId = entity.MeasurementValidatorId,
      ConnectionPower_W = entity.ConnectionPower_W,
      Phases = entity.Phases.Select(p => p.ToModel()).ToList()
    };
  }
}
