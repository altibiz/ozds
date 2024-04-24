using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

// TODO: ArchivedNetworkUserMeasurementLocation

namespace Ozds.Business.Models.Base;

public abstract class CalculationModel : ICalculation
{
  [Required] public required DateTimeOffset FromDate { get; set; } = default!;
  [Required] public required DateTimeOffset ToDate { get; set; } = default!;

  [Required]
  public required DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  [Required] public required string? IssuedById { get; set; } = default!;

  [Required] public required string Id { get; init; } = default!;
  [Required] public required string Title { get; set; } = default!;

  [Required] public required string MeterId { get; set; } = default!;

  [Required] public required IMeter ArchivedMeter { get; set; } = default!;

  [Required] public required decimal Total_EUR { get; set; }

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (validationContext.MemberName is null ||
        validationContext.MemberName == nameof(IssuedOn))
    {
      if (IssuedOn > DateTimeOffset.UtcNow)
      {
        yield return new ValidationResult("IssuedOn must be in the past",
          new[] { nameof(IssuedOn) });
      }
    }
  }
}
