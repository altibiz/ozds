using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class
  ReactiveEnergyTotalRampedT0CalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal ReactiveImportMin_VARh { get; set; }

  [Required]
  public required decimal ReactiveImportMax_VARh { get; set; }

  [Required]
  public required decimal ReactiveImportAmount_VARh { get; set; }

  [Required]
  public required decimal ReactiveExportMin_VARh { get; set; }

  [Required]
  public required decimal ReactiveExportMax_VARh { get; set; }

  [Required]
  public required decimal ReactiveExportAmount_VARh { get; set; }

  [Required]
  public required decimal ActiveImportMin_Wh { get; set; }

  [Required]
  public required decimal ActiveImportMax_Wh { get; set; }

  [Required]
  public required decimal ActiveImportAmount_Wh { get; set; }

  [Required]
  public required decimal Amount_VARh { get; set; }

  [Required]
  public required decimal Price_EUR { get; set; }

  [Required]
  public required decimal Total_EUR { get; set; }

  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ReactiveImportMin_VARh),
            new SinglePhasicMeasure<decimal>(ReactiveExportMin_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ReactiveImportMax_VARh),
            new SinglePhasicMeasure<decimal>(ReactiveExportMax_VARh)
          )
        )
      );
    }
  }

  public override decimal Total => Total_EUR;
}

public class UsageReactiveEnergyTotalRampedT0CalculationItemModel
  : ReactiveEnergyTotalRampedT0CalculationItemModel
{
  public override string Kind
  {
    get { return "JEN"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR)
          )
        )
      );
    }
  }
}
