using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models;

public class AbbB2xAggregateModel : AggregateModel
{
  [Required]
  public required InstantaneousAggregateMeasureModel VoltageL1AnyT0_V { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel VoltageL2AnyT0_V { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel VoltageL3AnyT0_V { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel CurrentL1AnyT0_A { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel CurrentL2AnyT0_A { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel CurrentL3AnyT0_A { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel ActivePowerL1NetT0_W { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel ActivePowerL2NetT0_W { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel ActivePowerL3NetT0_W { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel ReactivePowerL1NetT0_VAR { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel ReactivePowerL2NetT0_VAR { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel ReactivePowerL3NetT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyL1ImportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerL1ImportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyL2ImportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerL2ImportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyL3ImportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerL3ImportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyL1ExportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerL1ExportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyL2ExportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerL2ExportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyL3ExportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerL3ExportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyL1ImportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerL1ImportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyL2ImportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerL2ImportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyL3ImportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerL3ImportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyL1ExportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerL1ExportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyL2ExportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerL2ExportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyL3ExportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerL3ExportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyTotalImportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerTotalImportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyTotalExportT0_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerTotalExportT0_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyTotalImportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerTotalImportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ReactiveEnergyTotalExportT0_VARh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedReactivePowerTotalExportT0_VAR { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyTotalImportT1_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerTotalImportT1_W { get; set; } = default!;

  [Required]
  public required CumulativeAggregateMeasureModel ActiveEnergyTotalImportT2_Wh { get; set; } = default!;

  [Required]
  public required InstantaneousAggregateMeasureModel DerivedActivePowerTotalImportT2_W { get; set; } = default!;

  public override TariffMeasure<decimal> Current_A
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(
            CurrentL1AnyT0_A.Avg,
            CurrentL2AnyT0_A.Avg,
            CurrentL3AnyT0_A.Avg
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(
            VoltageL1AnyT0_V.Avg,
            VoltageL2AnyT0_V.Avg,
            VoltageL3AnyT0_V.Avg
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ActivePower_W
  {
    get
    {
      return new CompositeTariffMeasure<decimal>(
      [
        base.ActivePower_W,
        new UnaryTariffMeasure<decimal>(
          new NetDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              ActivePowerL1NetT0_W.Avg,
              ActivePowerL2NetT0_W.Avg,
              ActivePowerL3NetT0_W.Avg
            )
          )
        )
      ]);
    }
  }

  public override TariffMeasure<decimal> ReactivePower_VAR
  {
    get
    {
      return new CompositeTariffMeasure<decimal>(
      [
        base.ReactivePower_VAR,
        new UnaryTariffMeasure<decimal>(
          new NetDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              ReactivePowerL1NetT0_VAR.Avg,
              ReactivePowerL2NetT0_VAR.Avg,
              ReactivePowerL3NetT0_VAR.Avg
            )
          )
        )
      ]);
    }
  }

  public override SpanningMeasure<decimal> ActiveEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new CompositeTariffMeasure<decimal>(
        [
          new BinaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalImportT1_Wh.Min),
              new NullPhasicMeasure<decimal>()
            ),
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalImportT2_Wh.Min),
              new NullPhasicMeasure<decimal>()
            )
          ),

          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalImportT0_Wh.Min),
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalExportT0_Wh.Min)
            )
          )
        ]),
        new CompositeTariffMeasure<decimal>(
        [
          new BinaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalImportT1_Wh.Max),
              new NullPhasicMeasure<decimal>()
            ),
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalImportT2_Wh.Max),
              new NullPhasicMeasure<decimal>()
            )
          ),

          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalImportT0_Wh.Max),
              new SinglePhasicMeasureSum<decimal>(
                ActiveEnergyTotalExportT0_Wh.Max)
            )
          )
        ])
      );
    }
  }

  public override SpanningMeasure<decimal> ReactiveEnergySpan_VARh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalImportT0_VARh.Min),
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalExportT0_VARh.Min)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalImportT0_VARh.Max),
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalExportT0_VARh.Max)
          )
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure<decimal>.Null; }
  }
}
