using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class MessengerModelActivator : ModelActivator<MessengerModel>
{
  public override MessengerModel ActivateConcrete()
  {
    return New();
  }

  public static MessengerModel New()
  {
    return new MessengerModel
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
      LocationId = default!
    };
  }
}
