using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models;

public class RedLowNetworkUserCalculationModel
  : NetworkUserCalculationModel<RedLowNetworkUserCatalogueModel>
{
  [Required]
  public required UsageActiveEnergyTotalImportT1CalculationItemModel
    UsageActiveEnergyTotalImportT1
  { get; set; } = default!;

  [Required]
  public required UsageActiveEnergyTotalImportT2CalculationItemModel
    UsageActiveEnergyTotalImportT2
  { get; set; } = default!;

  [Required]
  public required UsageActivePowerTotalImportT1PeakCalculationItemModel
    UsageActivePowerTotalImportT1Peak
  { get; set; } = default!;

  [Required]
  public required UsageReactiveEnergyTotalRampedT0CalculationItemModel
    UsageReactiveEnergyTotalRampedT0
  { get; set; } = default!;

  public override string Kind
  {
    get { return "Red Low Voltage"; }
  }

  protected override IEnumerable<ICalculationItem> AdditionalUsageItems
  {
    get
    {
      return new ICalculationItem[]
      {
        UsageActiveEnergyTotalImportT1,
        UsageActiveEnergyTotalImportT2,
        UsageActivePowerTotalImportT1Peak,
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
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT1
                .Min_Wh),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT2
                .Min_Wh),
            new NullPhasicMeasure<decimal>()
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT2
                .Max_Wh),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT2
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
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT1
                .Price_EUR),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT2
                .Price_EUR),
            new NullPhasicMeasure<decimal>()
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              SupplyActiveEnergyTotalImportT1
                .Price_EUR),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              SupplyActiveEnergyTotalImportT2
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
              UsageReactiveEnergyTotalRampedT0.ReactiveImportMin_VARh),
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ReactiveExportMin_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ReactiveImportMax_VARh),
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ReactiveExportMax_VARh)
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
              UsageReactiveEnergyTotalRampedT0.Price_EUR
            )
          )
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ActivePowerAmount_W
  {
    get
    {
      return new PeakSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActivePowerTotalImportT1Peak
                .Amount_W),
            new NullPhasicMeasure<decimal>()
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }

  public override ExpenditureMeasure<decimal> ActivePowerPrice_EUR
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActivePowerTotalImportT1Peak
                .Price_EUR),
            new NullPhasicMeasure<decimal>()
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }
}
