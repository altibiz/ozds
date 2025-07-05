using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Complex;

public class PeriodModel : IModel
{
  [Required]
  public required DurationModel Duration { get; set; }

  [Required]
  public required uint Multiplier { get; set; }

  public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    yield break;
  }
}
