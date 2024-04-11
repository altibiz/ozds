using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public class ActivePowerTotalImportT1PeakCalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal Peak_W { get; set; }
  [Required]
  public required decimal Amount_W { get; set; }
  [Required]
  public required decimal Price_EUR { get; set; }
  [Required]
  public required decimal Total_EUR { get; set; }

  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new PeakSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Peak_W),
            PhasicMeasure<decimal>.Null
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
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(Price_EUR),
          PhasicMeasure<decimal>.Null
        )
      );
    }
  }
}
