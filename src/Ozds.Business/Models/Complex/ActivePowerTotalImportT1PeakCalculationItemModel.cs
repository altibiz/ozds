using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class
  ActivePowerTotalImportT1PeakCalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal Peak_kW { get; set; }

  [Required]
  public required decimal Amount_kW { get; set; }

  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new PeakSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Peak_kW),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public class UsageActivePowerTotalImportT1PeakCalculationItemModel
  : ActivePowerTotalImportT1PeakCalculationItemModel
{
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
