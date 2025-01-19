using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models;

public class BlueLowNetworkUserCalculationModel
  : NetworkUserCalculationModel<BlueLowNetworkUserCatalogueModel>
{
  [Required]
  public required UsageActiveEnergyTotalImportT0CalculationItemModel
    UsageActiveEnergyTotalImportT0
  { get; set; } = default!;

  [Required]
  public required UsageReactiveEnergyTotalRampedT0CalculationItemModel
    UsageReactiveEnergyTotalRampedT0
  { get; set; } = default!;

  public override string KindTitle
  {
    get { return "Blue Low Voltage"; }
  }

  protected override IEnumerable<ICalculationItem> AdditionalUsageItems
  {
    get
    {
      return new ICalculationItem[]
      {
        UsageActiveEnergyTotalImportT0,
        UsageReactiveEnergyTotalRampedT0
      };
    }
  }

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT0
                .Min_kWh),
            new NullPhasicMeasure<decimal>()
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageActiveEnergyTotalImportT0
                .Max_kWh),
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
      return
        new DualExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(
                UsageActiveEnergyTotalImportT0
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
              UsageReactiveEnergyTotalRampedT0.ReactiveImportMin_kVARh),
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ReactiveImportMax_kVARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ReactiveImportMax_kVARh),
            new SinglePhasicMeasure<decimal>(
              UsageReactiveEnergyTotalRampedT0.ReactiveExportMax_kVARh)
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
