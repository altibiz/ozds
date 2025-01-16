using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class CalculationModel : IdentifiableModel, ICalculation
{
  [Required]
  public required DateTimeOffset FromDate { get; set; } = default!;

  [Required]
  public required DateTimeOffset ToDate { get; set; } = default!;

  [Required]
  public required DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  [Required]
  public required string? IssuedById { get; set; } = default!;

  [Required]
  public required string MeterId { get; set; } = default!;

  [Required]
  public required IMeter ArchivedMeter { get; set; } = default!;

  [Required]
  public required decimal Total_EUR { get; set; }

  public decimal TaxRate_Percent => 0.0M;

  public decimal Tax_EUR => 0.0M;

  public decimal TotalWithTax_EUR => Total_EUR;

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if ((validationContext.MemberName is null ||
        validationContext.MemberName == nameof(IssuedOn)) &&
      IssuedOn > DateTimeOffset.UtcNow)
    {
      yield return new ValidationResult(
        "IssuedOn must be in the past",
        new[] { nameof(IssuedOn) });
    }
  }
}
