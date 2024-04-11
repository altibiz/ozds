using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class ActiveEnergyTotalImportCalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal Min_Wh { get; set; }
  [Required]
  public required decimal Max_Wh { get; set; }
  [Required]
  public required decimal Amount_Wh { get; set; }
  [Required]
  public required decimal Price_EUR { get; set; }
  [Required]
  public required decimal Total_EUR { get; set; }
}

public class ActiveEnergyTotalImportT0CalculationItemModel
  : ActiveEnergyTotalImportCalculationItemModel
{
  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Min_Wh),
            PhasicMeasure<decimal>.Null
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Max_Wh),
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

public class ActiveEnergyTotalImportT1CalculationItemModel
  : ActiveEnergyTotalImportCalculationItemModel
{
  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Min_Wh),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Max_Wh),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }

  public override TariffMeasure<decimal> Price
  {
    get
    {
      return new BinaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(Price_EUR),
          PhasicMeasure<decimal>.Null
        ),
        DuplexMeasure<decimal>.Null
      );
    }
  }
}

public class ActiveEnergyTotalImportT2CalculationItemModel
  : ActiveEnergyTotalImportCalculationItemModel
{
  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Min_Wh),
            PhasicMeasure<decimal>.Null
          )
        ),
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Max_Wh),
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
      return new BinaryTariffMeasure<decimal>(
        DuplexMeasure<decimal>.Null,
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(Price_EUR),
          PhasicMeasure<decimal>.Null
        )
      );
    }
  }
}
