using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation;

public class
  LocationMeasurementLocationModelActivator : ModelActivator<
  LocationMeasurementLocationModel>
{
  public override LocationMeasurementLocationModel ActivateConcrete()
  {
    return New();
  }

  public static LocationMeasurementLocationModel New()
  {
    return new LocationMeasurementLocationModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      LocationId = default!,
      MeterId = default!
    };
  }
}
