using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class MessengerModel : AuditableModel
{
  [Required] public required string LocationId { get; set; }

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

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    foreach (var validationResult in base.Validate(validationContext))
    {
      yield return validationResult;
    }

    if (
      validationContext.MemberName is null or nameof(Id) &&
      Id is null)
    {
      yield return new ValidationResult(
        "ID must be set",
        new[] { nameof(Id) });
    }
  }
}
