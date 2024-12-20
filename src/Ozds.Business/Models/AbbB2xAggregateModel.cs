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
          new InstantaneousPhaseMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              CurrentL1AnyT0_A.Avg,
              CurrentL2AnyT0_A.Avg,
              CurrentL3AnyT0_A.Avg
            ),
            new TriPhasicMeasure<decimal>(
              CurrentL1AnyT0_A.Min,
              CurrentL2AnyT0_A.Min,
              CurrentL3AnyT0_A.Min
            ),
            new[] { CurrentL1AnyT0_A, CurrentL2AnyT0_A, CurrentL3AnyT0_A }
              .Select(phase => phase.MinTimestamp)
              .Min(),
            new TriPhasicMeasure<decimal>(
              CurrentL1AnyT0_A.Max,
              CurrentL2AnyT0_A.Max,
              CurrentL3AnyT0_A.Max
            ),
            new[] { CurrentL1AnyT0_A, CurrentL2AnyT0_A, CurrentL3AnyT0_A }
              .Select(phase => phase.MaxTimestamp)
              .Max()
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
          new InstantaneousPhaseMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              VoltageL1AnyT0_V.Avg,
              VoltageL2AnyT0_V.Avg,
              VoltageL3AnyT0_V.Avg
            ),
            new TriPhasicMeasure<decimal>(
              VoltageL1AnyT0_V.Min,
              VoltageL2AnyT0_V.Min,
              VoltageL3AnyT0_V.Min
            ),
            new[] { VoltageL1AnyT0_V, VoltageL2AnyT0_V, VoltageL3AnyT0_V }
              .Select(phase => phase.MinTimestamp)
              .Min(),
            new TriPhasicMeasure<decimal>(
              VoltageL1AnyT0_V.Max,
              VoltageL2AnyT0_V.Max,
              VoltageL3AnyT0_V.Max
            ),
            new[] { VoltageL1AnyT0_V, VoltageL2AnyT0_V, VoltageL3AnyT0_V }
              .Select(phase => phase.MaxTimestamp)
              .Max()
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ActivePower_W
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new NetDuplexMeasure<decimal>(
          new InstantaneousPhaseMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              ActivePowerL1NetT0_W.Avg,
              ActivePowerL2NetT0_W.Avg,
              ActivePowerL3NetT0_W.Avg
            ),
            new TriPhasicMeasure<decimal>(
              ActivePowerL1NetT0_W.Min,
              ActivePowerL2NetT0_W.Min,
              ActivePowerL3NetT0_W.Min
            ),
            new[] { ActivePowerL1NetT0_W, ActivePowerL2NetT0_W, ActivePowerL3NetT0_W }
              .Select(phase => phase.MinTimestamp)
              .Min(),
            new TriPhasicMeasure<decimal>(
              ActivePowerL1NetT0_W.Max,
              ActivePowerL2NetT0_W.Max,
              ActivePowerL3NetT0_W.Max
            ),
            new[] { ActivePowerL1NetT0_W, ActivePowerL2NetT0_W, ActivePowerL3NetT0_W }
              .Select(phase => phase.MaxTimestamp)
              .Max()
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ReactivePower_VAR
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new NetDuplexMeasure<decimal>(
          new InstantaneousPhaseMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              ReactivePowerL1NetT0_VAR.Avg,
              ReactivePowerL2NetT0_VAR.Avg,
              ReactivePowerL3NetT0_VAR.Avg
            ),
            new TriPhasicMeasure<decimal>(
              ReactivePowerL1NetT0_VAR.Min,
              ReactivePowerL2NetT0_VAR.Min,
              ReactivePowerL3NetT0_VAR.Min
            ),
            new[] { ReactivePowerL1NetT0_VAR, ReactivePowerL2NetT0_VAR, ReactivePowerL3NetT0_VAR }
              .Select(phase => phase.MinTimestamp)
              .Min(),
            new TriPhasicMeasure<decimal>(
              ReactivePowerL1NetT0_VAR.Max,
              ReactivePowerL2NetT0_VAR.Max,
              ReactivePowerL3NetT0_VAR.Max
            ),
            new[] { ReactivePowerL1NetT0_VAR, ReactivePowerL2NetT0_VAR, ReactivePowerL3NetT0_VAR }
              .Select(phase => phase.MaxTimestamp)
              .Max()
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ApparentPower_VA
  {
    get
    {
      return TariffMeasure<decimal>.Null;
    }
  }

  public override TariffMeasure<decimal> ActiveEnergy_Wh
  {
    get
    {
      return new CompositeTariffMeasure<decimal>(
      [
          new BinaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new CumulativePhasicMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT1_Wh.Min),
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT1_Wh.Max)
              ),
              PhasicMeasure<decimal>.Null
            ),
            new ImportExportDuplexMeasure<decimal>(
              new CumulativePhasicMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT2_Wh.Min),
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT2_Wh.Max)
              ),
              PhasicMeasure<decimal>.Null
            )
          ),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new CumulativePhasicMeasure<decimal>(
                new CompositePhasicMeasure<decimal>(
                [
                  new TriPhasicMeasure<decimal>(
                    ActiveEnergyL1ImportT0_Wh.Min,
                    ActiveEnergyL2ImportT0_Wh.Min,
                    ActiveEnergyL3ImportT0_Wh.Min
                  ),
                  new SinglePhasicMeasureSum<decimal>(
                    ActiveEnergyTotalImportT0_Wh.Min)
                ]),
                new CompositePhasicMeasure<decimal>(
                [
                  new TriPhasicMeasure<decimal>(
                    ActiveEnergyL1ImportT0_Wh.Max,
                    ActiveEnergyL2ImportT0_Wh.Max,
                    ActiveEnergyL3ImportT0_Wh.Max
                  ),
                  new SinglePhasicMeasureSum<decimal>(
                    ActiveEnergyTotalImportT0_Wh.Max)
                ])
              ),
              new CumulativePhasicMeasure<decimal>(
                new CompositePhasicMeasure<decimal>(
                [
                  new TriPhasicMeasure<decimal>(
                    ActiveEnergyL1ExportT0_Wh.Min,
                    ActiveEnergyL2ExportT0_Wh.Min,
                    ActiveEnergyL3ExportT0_Wh.Min
                  ),
                  new SinglePhasicMeasureSum<decimal>(
                    ActiveEnergyTotalExportT0_Wh.Min)
                ]),
                new CompositePhasicMeasure<decimal>(
                [
                  new TriPhasicMeasure<decimal>(
                    ActiveEnergyL1ExportT0_Wh.Max,
                    ActiveEnergyL2ExportT0_Wh.Max,
                    ActiveEnergyL3ExportT0_Wh.Max
                  ),
                  new SinglePhasicMeasureSum<decimal>(
                    ActiveEnergyTotalExportT0_Wh.Max)
                ])
              )
            )
          )
      ]);
    }
  }

  public override TariffMeasure<decimal> ReactiveEnergy_VARh
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new CumulativePhasicMeasure<decimal>(
            new CompositePhasicMeasure<decimal>(
            [
              new TriPhasicMeasure<decimal>(
                ReactiveEnergyL1ImportT0_VARh.Min,
                ReactiveEnergyL2ImportT0_VARh.Min,
                ReactiveEnergyL3ImportT0_VARh.Min
              ),
              new SinglePhasicMeasureSum<decimal>(
                ReactiveEnergyTotalImportT0_VARh.Min)
            ]),
            new CompositePhasicMeasure<decimal>(
            [
              new TriPhasicMeasure<decimal>(
                ReactiveEnergyL1ImportT0_VARh.Max,
                ReactiveEnergyL2ImportT0_VARh.Max,
                ReactiveEnergyL3ImportT0_VARh.Max
              ),
              new SinglePhasicMeasureSum<decimal>(
                ReactiveEnergyTotalImportT0_VARh.Max)
            ])
          ),
          new CumulativePhasicMeasure<decimal>(
            new CompositePhasicMeasure<decimal>(
            [
              new TriPhasicMeasure<decimal>(
                ReactiveEnergyL1ExportT0_VARh.Min,
                ReactiveEnergyL2ExportT0_VARh.Min,
                ReactiveEnergyL3ExportT0_VARh.Min
              ),
              new SinglePhasicMeasureSum<decimal>(
                ReactiveEnergyTotalExportT0_VARh.Min)
            ]),
            new CompositePhasicMeasure<decimal>(
            [
              new TriPhasicMeasure<decimal>(
                ReactiveEnergyL1ExportT0_VARh.Max,
                ReactiveEnergyL2ExportT0_VARh.Max,
                ReactiveEnergyL3ExportT0_VARh.Max
              ),
              new SinglePhasicMeasureSum<decimal>(
                ReactiveEnergyTotalExportT0_VARh.Max)
            ])
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ApparentEnergy_VAh
  {
    get { return TariffMeasure<decimal>.Null; }
  }

  public override TariffMeasure<decimal> DerivedActivePower_W =>
    new CompositeTariffMeasure<decimal>(
    [
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new InstantaneousPhaseMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                DerivedActivePowerTotalImportT1_W.Avg),
              new SinglePhasicMeasureSum<decimal>(
                DerivedActivePowerTotalImportT1_W.Min),
              DerivedActivePowerTotalImportT1_W.MinTimestamp,
              new SinglePhasicMeasureSum<decimal>(
                DerivedActivePowerTotalImportT1_W.Max),
              DerivedActivePowerTotalImportT1_W.MaxTimestamp
            ),
            PhasicMeasure<decimal>.Null
          ),
          new ImportExportDuplexMeasure<decimal>(
            new InstantaneousPhaseMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(
                DerivedActivePowerTotalImportT2_W.Avg),
              new SinglePhasicMeasureSum<decimal>(
                DerivedActivePowerTotalImportT2_W.Min),
              DerivedActivePowerTotalImportT2_W.MinTimestamp,
              new SinglePhasicMeasureSum<decimal>(
                DerivedActivePowerTotalImportT2_W.Max),
              DerivedActivePowerTotalImportT2_W.MaxTimestamp
            ),
            PhasicMeasure<decimal>.Null
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new InstantaneousPhaseMeasure<decimal>(
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  DerivedActivePowerL1ImportT0_W.Avg,
                  DerivedActivePowerL2ImportT0_W.Avg,
                  DerivedActivePowerL3ImportT0_W.Avg
                ),
                new SinglePhasicMeasureSum<decimal>(
                  DerivedActivePowerTotalImportT0_W.Avg)
              ]),
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  DerivedActivePowerL1ImportT0_W.Min,
                  DerivedActivePowerL2ImportT0_W.Min,
                  DerivedActivePowerL3ImportT0_W.Min
                ),
                new SinglePhasicMeasureSum<decimal>(
                  DerivedActivePowerTotalImportT0_W.Min)
              ]),
              DerivedActivePowerTotalImportT0_W.MinTimestamp,
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  DerivedActivePowerL1ImportT0_W.Max,
                  DerivedActivePowerL2ImportT0_W.Max,
                  DerivedActivePowerL3ImportT0_W.Max
                ),
                new SinglePhasicMeasureSum<decimal>(
                  DerivedActivePowerTotalImportT0_W.Max)
              ]),
              DerivedActivePowerTotalImportT0_W.MaxTimestamp
            ),
            new InstantaneousPhaseMeasure<decimal>(
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  DerivedActivePowerL1ExportT0_W.Avg,
                  DerivedActivePowerL2ExportT0_W.Avg,
                  DerivedActivePowerL3ExportT0_W.Avg
                ),
                new SinglePhasicMeasureSum<decimal>(
                  DerivedActivePowerTotalExportT0_W.Avg)
              ]),
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  DerivedActivePowerL1ExportT0_W.Min,
                  DerivedActivePowerL2ExportT0_W.Min,
                  DerivedActivePowerL3ExportT0_W.Min
                ),
                new SinglePhasicMeasureSum<decimal>(
                  DerivedActivePowerTotalExportT0_W.Min)
              ]),
              DerivedActivePowerTotalExportT0_W.MinTimestamp,
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  DerivedActivePowerL1ExportT0_W.Max,
                  DerivedActivePowerL2ExportT0_W.Max,
                  DerivedActivePowerL3ExportT0_W.Max
                ),
                new SinglePhasicMeasureSum<decimal>(
                  DerivedActivePowerTotalExportT0_W.Max)
              ]),
              DerivedActivePowerTotalExportT0_W.MaxTimestamp
            )
          )
        )
    ]);

  public override TariffMeasure<decimal> DerivedReactivePower_VAR =>
    new UnaryTariffMeasure<decimal>(
      new ImportExportDuplexMeasure<decimal>(
        new InstantaneousPhaseMeasure<decimal>(
          new CompositePhasicMeasure<decimal>(
          [
            new TriPhasicMeasure<decimal>(
              DerivedReactivePowerL1ImportT0_VAR.Avg,
              DerivedReactivePowerL2ImportT0_VAR.Avg,
              DerivedReactivePowerL3ImportT0_VAR.Avg
            ),
            new SinglePhasicMeasureSum<decimal>(
              DerivedReactivePowerTotalImportT0_VAR.Avg)
          ]),
          new CompositePhasicMeasure<decimal>(
          [
            new TriPhasicMeasure<decimal>(
              DerivedReactivePowerL1ImportT0_VAR.Min,
              DerivedReactivePowerL2ImportT0_VAR.Min,
              DerivedReactivePowerL3ImportT0_VAR.Min
            ),
            new SinglePhasicMeasureSum<decimal>(
              DerivedReactivePowerTotalImportT0_VAR.Min)
          ]),
          DerivedReactivePowerTotalImportT0_VAR.MinTimestamp,
          new CompositePhasicMeasure<decimal>(
          [
            new TriPhasicMeasure<decimal>(
              DerivedReactivePowerL1ImportT0_VAR.Max,
              DerivedReactivePowerL2ImportT0_VAR.Max,
              DerivedReactivePowerL3ImportT0_VAR.Max
            ),
            new SinglePhasicMeasureSum<decimal>(
              DerivedReactivePowerTotalImportT0_VAR.Max)
          ]),
          DerivedReactivePowerTotalImportT0_VAR.MaxTimestamp
        ),
        new InstantaneousPhaseMeasure<decimal>(
          new CompositePhasicMeasure<decimal>(
          [
            new TriPhasicMeasure<decimal>(
              DerivedReactivePowerL1ExportT0_VAR.Avg,
              DerivedReactivePowerL2ExportT0_VAR.Avg,
              DerivedReactivePowerL3ExportT0_VAR.Avg
            ),
            new SinglePhasicMeasureSum<decimal>(
              DerivedReactivePowerTotalExportT0_VAR.Avg)
          ]),
          new CompositePhasicMeasure<decimal>(
          [
            new TriPhasicMeasure<decimal>(
              DerivedReactivePowerL1ExportT0_VAR.Min,
              DerivedReactivePowerL2ExportT0_VAR.Min,
              DerivedReactivePowerL3ExportT0_VAR.Min
            ),
            new SinglePhasicMeasureSum<decimal>(
              DerivedReactivePowerTotalExportT0_VAR.Min)
          ]),
          DerivedReactivePowerTotalExportT0_VAR.MinTimestamp,
          new CompositePhasicMeasure<decimal>(
          [
            new TriPhasicMeasure<decimal>(
              DerivedReactivePowerL1ExportT0_VAR.Max,
              DerivedReactivePowerL2ExportT0_VAR.Max,
              DerivedReactivePowerL3ExportT0_VAR.Max
            ),
            new SinglePhasicMeasureSum<decimal>(
              DerivedReactivePowerTotalExportT0_VAR.Max)
          ]),
          DerivedReactivePowerTotalExportT0_VAR.MaxTimestamp
        )
      )
    );

  public override TariffMeasure<decimal> DerivedApparentPower_VA =>
    TariffMeasure<decimal>.Null;
}
