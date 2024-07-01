using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class
  ActivePowerTotalImportT1PeakCalculationItemModel : CalculationItemModel
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
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Peak_W),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }
}

public class UsageActivePowerTotalImportT1PeakCalculationItemModel
  : ActivePowerTotalImportT1PeakCalculationItemModel
{
  public override string Kind
  {
    get { return "SVT"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }
}
