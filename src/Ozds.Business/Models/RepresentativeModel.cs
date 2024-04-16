using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models;

public class RepresentativeModel : AuditableModel
{
  [Required] public required RoleModel Role { get; set; }

  [Required] public required PhysicalPersonModel PhysicalPerson { get; set; } = default!;

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
      PhysicalPerson = PhysicalPersonModel.New()
    };
  }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    foreach (var result in base.Validate(validationContext))
    {
      yield return result;
    }

    if (
      validationContext.MemberName is null or nameof(PhysicalPerson)
    )
    {
      foreach (var result in PhysicalPerson.Validate(validationContext))
      {
        yield return result;
      }
    }
  }
}
