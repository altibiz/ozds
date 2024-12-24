using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public class MeterModel : AuditableModel, IMeter
{
  [Required]
  public required decimal ConnectionPower_W { get; set; }

  [Required]
  public required HashSet<PhaseModel> Phases { get; set; } = [];

  [Required]
  public required string? MessengerId { get; set; }

  public virtual ICapabilities Capabilities
  {
    get { return new NullCapabilities(); }
  }

  [Required]
  public required virtual string Kind { get; set; } = default!;

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

    if (
      validationContext.MemberName is null or nameof(ConnectionPower_W) &&
      ConnectionPower_W <= 0)
    {
      yield return new ValidationResult(
        "Connection power must be greater than 0",
        new[] { nameof(ConnectionPower_W) });
    }

    if (
      validationContext.MemberName is null or nameof(Phases) &&
      Phases.Count == 0)
    {
      yield return new ValidationResult(
        "At least one phase must be set",
        new[] { nameof(Phases) });
    }

    if (
      validationContext.MemberName is null or nameof(Phases) &&
      Phases.Count > 3
    )
    {
      yield return new ValidationResult(
        "Maximum of three phases can be set",
        new[] { nameof(Phases) });
    }

    if (
      validationContext.MemberName is null or nameof(Phases) &&
      Phases.Count != Phases.Distinct().Count()
    )
    {
      yield return new ValidationResult(
        "Phases must be unique",
        new[] { nameof(Phases) });
    }
  }
}
