using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models.Base;

public class MessengerModel : AuditableModel
{
  [Required]
  public required string LocationId { get; set; }

  [Required]
  public required PeriodModel MaxInactivityPeriod { get; set; }

  [Required]
  public required PeriodModel PushDelayPeriod { get; set; }

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
