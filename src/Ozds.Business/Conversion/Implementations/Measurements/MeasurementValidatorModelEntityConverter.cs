using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class MeasurementValidatorModelEntityConverter : ConcreteModelEntityConverter<
  IMeasurementValidator, MeasurementValidatorEntity>
{
  protected override MeasurementValidatorEntity ToEntity(
    IMeasurementValidator model)
  {
    return model.ToEntity();
  }

  protected override IMeasurementValidator ToModel(
    MeasurementValidatorEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeasurementValidatorModelEntityConverterExtensions
{
  public static MeasurementValidatorEntity ToEntity(
    this IMeasurementValidator model)
  {
    if (model is AbbB2xMeasurementValidatorModel abbB2XMeasurementValidator)
    {
      return abbB2XMeasurementValidator.ToEntity();
    }

    if (model is SchneideriEM3xxxMeasurementValidatorModel
      schneideriEM3XxxMeasurementValidator)
    {
      return schneideriEM3XxxMeasurementValidator.ToEntity();
    }

    throw new NotSupportedException(
      $"MeasurementValidatorModel type {model.GetType().Name} is not supported."
    );
  }

  public static IMeasurementValidator ToModel(
    this MeasurementValidatorEntity entity)
  {
    if (entity is AbbB2xMeasurementValidatorEntity abbB2XMeasurementValidator)
    {
      return abbB2XMeasurementValidator.ToModel();
    }

    if (entity is SchneideriEM3xxxMeasurementValidatorEntity
      schneideriEM3XxxMeasurementValidator)
    {
      return schneideriEM3XxxMeasurementValidator.ToModel();
    }

    throw new NotSupportedException(
      $"MeasurementValidatorEntity type {entity.GetType().Name} is not supported."
    );
  }
}
