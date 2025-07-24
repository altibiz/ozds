using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class
  ReactiveEnergyTotalRampedT0CalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal ReactiveImportMin_kVARh { get; set; }

  [Required]
  public required decimal ReactiveImportMax_kVARh { get; set; }

  [Required]
  public required decimal ReactiveImportAmount_kVARh { get; set; }

  [Required]
  public required decimal ReactiveExportMin_kVARh { get; set; }

  [Required]
  public required decimal ReactiveExportMax_kVARh { get; set; }

  [Required]
  public required decimal ReactiveExportAmount_kVARh { get; set; }

  [Required]
  public required decimal ActiveImportMin_kWh { get; set; }

  [Required]
  public required decimal ActiveImportMax_kWh { get; set; }

  [Required]
  public required decimal ActiveImportAmount_kWh { get; set; }

  [Required]
  public required decimal Amount_kVARh { get; set; }

  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(ReactiveImportMin_kVARh),
            new SinglePhasicSumMeasure<decimal>(ReactiveExportMin_kVARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(ReactiveImportMax_kVARh),
            new SinglePhasicSumMeasure<decimal>(ReactiveExportMax_kVARh)
          )
        )
      );
    }
  }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public class UsageReactiveEnergyTotalRampedT0CalculationItemModel
  : ReactiveEnergyTotalRampedT0CalculationItemModel
{
  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(Price_EUR)
          )
        )
      );
    }
  }
}
