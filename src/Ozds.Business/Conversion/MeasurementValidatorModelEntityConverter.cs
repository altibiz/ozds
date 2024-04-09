using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class MeasurementValidatorModelEntityConverter : ModelEntityConverter<
  MeasurementValidatorModel, MeasurementValidatorEntity>
{
  protected override MeasurementValidatorEntity ToEntity(
    MeasurementValidatorModel model)
  {
    return model.ToEntity();
  }

  protected override MeasurementValidatorModel ToModel(
    MeasurementValidatorEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeasurementValidatorModelEntityConverterExtensions
{
  public static MeasurementValidatorEntity ToEntity(this MeasurementValidatorModel model)
  {
    if (model is AbbB2xMeasurementValidatorModel abbB2XMeasurementValidator)
    {
      return abbB2XMeasurementValidator.ToEntity();
    }

    if (model is SchneideriEM3xxxMeasurementValidatorModel schneideriEM3XxxMeasurementValidator)
    {
      return schneideriEM3XxxMeasurementValidator.ToEntity();
    }

    throw new NotSupportedException(
      $"MeasurementValidatorModel type {model.GetType().Name} is not supported."
    );
  }

  public static MeasurementValidatorModel ToModel(this MeasurementValidatorEntity entity)
  {
    if (entity is AbbB2xMeasurementValidatorEntity abbB2XMeasurementValidator)
    {
      return abbB2XMeasurementValidator.ToModel();
    }

    if (entity is SchneideriEM3xxxMeasurementValidatorEntity schneideriEM3XxxMeasurementValidator)
    {
      return schneideriEM3XxxMeasurementValidator.ToModel();
    }

    throw new NotSupportedException(
      $"MeasurementValidatorEntity type {entity.GetType().Name} is not supported."
    );
  }
}
