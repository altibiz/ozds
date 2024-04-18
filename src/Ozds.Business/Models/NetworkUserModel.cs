using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models;

public class NetworkUserModel : AuditableModel
{
  [Required] public required string LocationId { get; set; }

  [Required]
  public required LegalPersonModel LegalPerson { get; set; } = default!;

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
      LegalPerson = LegalPersonModel.New()
    };
  }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (
      validationContext.MemberName is null or nameof(LegalPerson)
    )
    {
      foreach (var result in LegalPerson.Validate(validationContext))
      {
        yield return result;
      }
    }
  }
}
