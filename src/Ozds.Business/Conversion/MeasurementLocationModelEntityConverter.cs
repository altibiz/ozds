using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class MeasurementLocationModelEntityConverter : ModelEntityConverter<
  IMeasurementLocation, MeasurementLocationEntity>
{
  protected override MeasurementLocationEntity ToEntity(
    IMeasurementLocation model)
  {
    return model.ToEntity();
  }

  protected override IMeasurementLocation ToModel(
    MeasurementLocationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeasurementLocationModelEntityConverterExtensions
{
  public static MeasurementLocationEntity ToEntity(
    this IMeasurementLocation model)
  {
    if (model is NetworkUserMeasurementLocationModel
      networkUserMeasurementLocation)
    {
      return networkUserMeasurementLocation.ToEntity();
    }

    if (model is LocationMeasurementLocationModel locationMeasurementLocation)
    {
      return locationMeasurementLocation.ToEntity();
    }

    throw new NotSupportedException(
      $"MeasurementLocationModel type {model.GetType().Name} is not supported."
    );
  }

  public static IMeasurementLocation ToModel(
    this MeasurementLocationEntity entity)
  {
    if (entity is NetworkUserMeasurementLocationEntity
      networkUserMeasurementLocation)
    {
      return networkUserMeasurementLocation.ToModel();
    }

    if (entity is LocationMeasurementLocationEntity locationMeasurementLocation)
    {
      return locationMeasurementLocation.ToModel();
    }

    throw new NotSupportedException(
      $"MeasurementLocationEntity type {
        entity.GetType().Name
      } is not supported."
    );
  }
}
