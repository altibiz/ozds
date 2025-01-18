using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class CalculationModel : FinancialModel, ICalculation
{
  [Required]
  public required string MeterId { get; set; } = default!;

  [Required]
  public required IMeter ArchivedMeter { get; set; } = default!;

  public override decimal TaxRate_Percent => 0.0M;

  public override decimal Tax_EUR => 0.0M;

  public override decimal TotalWithTax_EUR => Total_EUR;
}
