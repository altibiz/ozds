using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Complex;

public class RepresentativeModelActivator : ModelActivator<RepresentativeModel>
{
  public override RepresentativeModel ActivateConcrete()
  {
    return New();
  }
  public static RepresentativeModel New()
  {
    return new RepresentativeModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = null,
      LastUpdatedOn = null,
      LastUpdatedById = null,
      IsDeleted = false,
      DeletedOn = null,
      DeletedById = null,
      Role = RoleModel.NetworkUserRepresentative,
      PhysicalPerson = PhysicalPersonModelActivator.New()
    };
  }

  public static RepresentativeModel New(UserModel user)
  {
    return new RepresentativeModel
    {
      Id = user.Id,
      Title = user.UserName,
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = null,
      LastUpdatedOn = null,
      LastUpdatedById = null,
      IsDeleted = false,
      DeletedOn = null,
      DeletedById = null,
      Role = RoleModel.NetworkUserRepresentative,
      PhysicalPerson = PhysicalPersonModelActivator.New()
    };
  }
}
