using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeterModel : AuditableModel, IMeter
{
  [Required]
  public required string MeasurementLocationId { get; set; }
  [Required]
  public required string CatalogueId { get; set; }
  [Required]
  public required string MessengerId { get; set; }
  [Required]
  public required string MeasurementValidatorId { get; set; }
  [Required]
  public required float ConnectionPower_W { get; set; }
  [Required]
  public required List<PhaseModel> Phases { get; set; } = new();

  public abstract ICapabilities Capabilities { get; }

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    foreach (var validationResult in base.Validate(validationContext))
    {
      yield return validationResult;
    }

    if (ConnectionPower_W <= 0)
    {
      yield return new ValidationResult(
        "Connection power must be greater than 0",
        new[] { nameof(ConnectionPower_W) });
    }

    if (Phases.Count == 0)
    {
      yield return new ValidationResult(
        "At least one phase must be set",
        new[] { nameof(Phases) });
    }

    if (Phases.Count > 3)
    {
      yield return new ValidationResult(
        "Maximum of three phases can be set",
        new[] { nameof(Phases) });
    }

    if (Phases.Count != Phases.Distinct().Count())
    {
      yield return new ValidationResult(
        "Phases must be unique",
        new[] { nameof(Phases) });
    }
  }
}
