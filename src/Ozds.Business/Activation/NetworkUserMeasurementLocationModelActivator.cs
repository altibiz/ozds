using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class NetworkUserMeasurementLocationModelActivator : ModelActivator<NetworkUserMeasurementLocationModel>
{
  public override NetworkUserMeasurementLocationModel ActivateConcrete()
  {
    return New();
  }

  public static NetworkUserMeasurementLocationModel New()
  {
    return new NetworkUserMeasurementLocationModel
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
      NetworkUserId = default!,
      NetworkUserCatalogueId = default!,
      MeterId = default!
    };
  }
}
