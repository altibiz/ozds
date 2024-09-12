using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Complex;

public class NetworkUserModelActivator : ModelActivator<NetworkUserModel>
{
  public override NetworkUserModel ActivateConcrete()
  {
    return New();
  }

  public static NetworkUserModel New()
  {
    return new NetworkUserModel
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
      LocationId = "",
      LegalPerson = LegalPersonModelActivator.New(),
      AltiBizSubProjectCode = ""
    };
  }
}
