using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class MeasurementModelEntityConverter : ModelEntityConverter<
  IMeasurement, MeasurementEntity>
{
  protected override MeasurementEntity ToEntity(
    IMeasurement model)
  {
    return model.ToEntity();
  }

  protected override IMeasurement ToModel(
    MeasurementEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeasurementModelEntityConverterExtensions
{
  public static MeasurementEntity ToEntity(
    this IMeasurement model)
  {
    if (model is AbbB2xMeasurementModel
      abbB2xMeasurementModel)
    {
      return abbB2xMeasurementModel.ToEntity();
    }

    if (model is SchneideriEM3xxxMeasurementModel
      schneideriEM3xxxMeasurementModel)
    {
      return schneideriEM3xxxMeasurementModel.ToEntity();
    }

    throw new NotSupportedException(
      $"MeasurementModel type {model.GetType().Name} is not supported."
    );
  }

  public static MeasurementModel ToModel(
    this MeasurementEntity entity)
  {
    if (entity is AbbB2xMeasurementEntity
      abbB2xMeasurementEntity)
    {
      return abbB2xMeasurementEntity.ToModel();
    }

    if (entity is SchneideriEM3xxxMeasurementEntity
      schneideriEM3xxMeasurementEntity)
    {
      return schneideriEM3xxMeasurementEntity.ToModel();
    }

    throw new NotSupportedException(
      $"MeasurementEntity type {entity.GetType().Name} is not supported."
    );
  }
}
