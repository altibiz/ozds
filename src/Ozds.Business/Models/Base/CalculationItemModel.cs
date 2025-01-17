using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class CalculationItemModel : ICalculationItem
{
  public abstract SpanningMeasure<decimal> Amount { get; }

  public abstract ExpenditureMeasure<decimal> Price { get; }

  public abstract decimal Total { get; }

  public abstract string Kind { get; }

  [Required]
  public required decimal Price_EUR { get; set; }

  [Required]
  public required decimal Total_EUR { get; set; }

  public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    yield break;
  }
}
