using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class CalculationModel : FinancialModel, ICalculation
{
  [Required]
  public required DateTimeOffset RequestedFromDate { get; set; }

  [Required]
  public required DateTimeOffset RequestedToDate { get; set; }

  [Required]
  public required DateTimeOffset MeteredFromDate { get; set; }

  [Required]
  public required DateTimeOffset MeteredToDate { get; set; }

  public override decimal TaxRate_Percent
  {
    get { return 0.0M; }
  }

  public override decimal Tax_EUR
  {
    get { return 0.0M; }
  }

  public override decimal TotalWithTax_EUR
  {
    get { return Total_EUR; }
  }
}
