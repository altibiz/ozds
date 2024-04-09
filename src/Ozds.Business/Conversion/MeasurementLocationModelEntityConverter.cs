using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class MeasurementLocationModelEntityConverter : ModelEntityConverter<
  MeasurementLocationModel, MeasurementLocationEntity>
{
  protected override MeasurementLocationEntity ToEntity(
    MeasurementLocationModel model)
  {
    return model.ToEntity();
  }

  protected override MeasurementLocationModel ToModel(
    MeasurementLocationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeasurementLocationModelEntityConverterExtensions
{
  public static MeasurementLocationEntity ToEntity(
    this MeasurementLocationModel model)
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

  public static MeasurementLocationModel ToModel(
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
      $"MeasurementLocationEntity type {entity.GetType().Name} is not supported."
    );
  }
}
