using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public class
  ReactiveEnergyTotalRampedT0CalculationItemModel : CalculationItemModel
{
  [Required] public required decimal ImportMin_VARh { get; set; }

  [Required] public required decimal ImportMax_VARh { get; set; }

  [Required] public required decimal ImportAmount_VARh { get; set; }

  [Required] public required decimal ExportMin_VARh { get; set; }

  [Required] public required decimal ExportMax_VARh { get; set; }

  [Required] public required decimal ExportAmount_VARh { get; set; }

  [Required] public required decimal Amount_VARh { get; set; }

  [Required] public required decimal Price_EUR { get; set; }

  [Required] public required decimal Total_EUR { get; set; }

  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ImportMin_VARh),
            new SinglePhasicMeasure<decimal>(ExportMin_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ImportMax_VARh),
            new SinglePhasicMeasure<decimal>(ExportMax_VARh)
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> Price
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(Price_EUR)
        )
      );
    }
  }
}
