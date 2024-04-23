using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models;

public class WhiteLowNetworkUserCalculationModel
  : NetworkUserCalculationModel<WhiteLowNetworkUserCatalogueModel>
{
  [Required]
  public required ActiveEnergyTotalImportT1CalculationItemModel
    UsageActiveEnergyTotalImportT1
  { get; set; } = default!;

  [Required]
  public required ActiveEnergyTotalImportT2CalculationItemModel
    UsageActiveEnergyTotalImportT2
  { get; set; } = default!;

  [Required]
  public required ReactiveEnergyTotalRampedT0CalculationItemModel
    UsageReactiveEnergyTotalRampedT0
  { get; set; } = default!;

  public override string Kind
  {
    get { return "White Low Voltage"; }
  }

  protected override IEnumerable<ICalculationItem> AdditionalUsageItems
  {
    get
    {
      return new ICalculationItem[]
      {
        UsageActiveEnergyTotalImportT1,
        UsageActiveEnergyTotalImportT2,
        UsageReactiveEnergyTotalRampedT0
      };
    }
  }

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(UsageActiveEnergyTotalImportT1
              .Min_Wh),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(UsageActiveEnergyTotalImportT2
              .Min_Wh),
            new NullPhasicMeasure<decimal>()
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(UsageActiveEnergyTotalImportT2
              .Max_Wh),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(UsageActiveEnergyTotalImportT2
              .Max_Wh),
            new NullPhasicMeasure<decimal>()
          )
        )
      );
    }
  }

  public override ExpenditureMeasure<decimal> ActiveEnergyPrice_EUR
  {
    get
    {
      return new DualExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(UsageActiveEnergyTotalImportT1
              .Price_EUR),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(UsageActiveEnergyTotalImportT2
              .Price_EUR),
            new NullPhasicMeasure<decimal>()
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(SupplyActiveEnergyTotalImportT1
              .Price_EUR),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(SupplyActiveEnergyTotalImportT2
              .Price_EUR),
            new NullPhasicMeasure<decimal>()
          )
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ReactiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ImportMin_VARh),
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ExportMin_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ImportMax_VARh),
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ExportMax_VARh)
          )
        )
      );
    }
  }

  public override ExpenditureMeasure<decimal> ReactiveEnergyPrice_EUR
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.Price_EUR)
          )
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ActivePowerAmount_W
  {
    get { return SpanningMeasure<decimal>.Null; }
  }

  public override ExpenditureMeasure<decimal> ActivePowerPrice_EUR
  {
    get { return ExpenditureMeasure<decimal>.Null; }
  }
}
