using Ozds.Business.Math;
using Ozds.Fake.Records.Base;

namespace Ozds.Fake.Records;

public record class AbbB2xMeasurementRecord : MeasurementRecord
{
#pragma warning disable CA1707
  public required decimal VoltageL1AnyT0_V { get; set; }
  public required decimal VoltageL2AnyT0_V { get; set; }
  public required decimal VoltageL3AnyT0_V { get; set; }
  public required decimal CurrentL1AnyT0_A { get; set; }
  public required decimal CurrentL2AnyT0_A { get; set; }
  public required decimal CurrentL3AnyT0_A { get; set; }
  public required decimal ActivePowerL1NetT0_W { get; set; }
  public required decimal ActivePowerL2NetT0_W { get; set; }
  public required decimal ActivePowerL3NetT0_W { get; set; }
  public required decimal ReactivePowerL1NetT0_VAR { get; set; }
  public required decimal ReactivePowerL2NetT0_VAR { get; set; }
  public required decimal ReactivePowerL3NetT0_VAR { get; set; }
  public required decimal ActiveEnergyL1ImportT0_Wh { get; set; }
  public required decimal ActiveEnergyL2ImportT0_Wh { get; set; }
  public required decimal ActiveEnergyL3ImportT0_Wh { get; set; }
  public required decimal ActiveEnergyL1ExportT0_Wh { get; set; }
  public required decimal ActiveEnergyL2ExportT0_Wh { get; set; }
  public required decimal ActiveEnergyL3ExportT0_Wh { get; set; }
  public required decimal ReactiveEnergyL1ImportT0_VARh { get; set; }
  public required decimal ReactiveEnergyL2ImportT0_VARh { get; set; }
  public required decimal ReactiveEnergyL3ImportT0_VARh { get; set; }
  public required decimal ReactiveEnergyL1ExportT0_VARh { get; set; }
  public required decimal ReactiveEnergyL2ExportT0_VARh { get; set; }
  public required decimal ReactiveEnergyL3ExportT0_VARh { get; set; }
  public required decimal ActiveEnergyTotalImportT0_Wh { get; set; }
  public required decimal ActiveEnergyTotalExportT0_Wh { get; set; }
  public required decimal ReactiveEnergyTotalImportT0_VARh { get; set; }
  public required decimal ReactiveEnergyTotalExportT0_VARh { get; set; }
  public required decimal ActiveEnergyTotalImportT1_Wh { get; set; }
  public required decimal ActiveEnergyTotalImportT2_Wh { get; set; }

  public override TariffMeasure<decimal> ActiveEnergy_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
        || ActiveEnergyL2ImportT0_Wh is not 0
        || ActiveEnergyL3ImportT0_Wh is not 0
        || ActiveEnergyL1ExportT0_Wh is not 0
        || ActiveEnergyL2ExportT0_Wh is not 0
        || ActiveEnergyL3ExportT0_Wh is not 0
          ? new CompositeTariffMeasure<decimal>(
          [
            new UnaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new CompositePhasicMeasure<decimal>(
                [
                  new TriPhasicMeasure<decimal>(
                    ActiveEnergyL1ImportT0_Wh,
                    ActiveEnergyL2ImportT0_Wh,
                    ActiveEnergyL3ImportT0_Wh
                  ),
                  new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT0_Wh)
                ]),
                new CompositePhasicMeasure<decimal>(
                [
                  new TriPhasicMeasure<decimal>(
                    ActiveEnergyL1ExportT0_Wh,
                    ActiveEnergyL2ExportT0_Wh,
                    ActiveEnergyL3ExportT0_Wh
                  ),
                  new SinglePhasicMeasure<decimal>(ActiveEnergyTotalExportT0_Wh)
                ])
              )
            ),
            new BinaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1_Wh),
                PhasicMeasure<decimal>.Null
              ),
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2_Wh),
                PhasicMeasure<decimal>.Null
              )
            )
          ])
          : new CompositeTariffMeasure<decimal>(
          [
            new UnaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT0_Wh),
                new SinglePhasicMeasure<decimal>(ActiveEnergyTotalExportT0_Wh)
              )
            ),
            new BinaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1_Wh),
                PhasicMeasure<decimal>.Null
              ),
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2_Wh),
                PhasicMeasure<decimal>.Null
              )
            )
          ]);
    }
  }

  public override TariffMeasure<decimal> ReactiveEnergy_VARh
  {
    get
    {
      return ReactiveEnergyL1ImportT0_VARh is not 0
        || ReactiveEnergyL2ImportT0_VARh is not 0
        || ReactiveEnergyL3ImportT0_VARh is not 0
        || ReactiveEnergyL1ExportT0_VARh is not 0
        || ReactiveEnergyL2ExportT0_VARh is not 0
        || ReactiveEnergyL3ExportT0_VARh is not 0
          ? new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  ReactiveEnergyL1ImportT0_VARh,
                  ReactiveEnergyL2ImportT0_VARh,
                  ReactiveEnergyL3ImportT0_VARh
                ),
                new SinglePhasicMeasure<decimal>(
                  ReactiveEnergyTotalImportT0_VARh)
              ]),
              new CompositePhasicMeasure<decimal>(
              [
                new TriPhasicMeasure<decimal>(
                  ReactiveEnergyL1ExportT0_VARh,
                  ReactiveEnergyL2ExportT0_VARh,
                  ReactiveEnergyL3ExportT0_VARh
                ),
                new SinglePhasicMeasure<decimal>(
                  ReactiveEnergyTotalExportT0_VARh)
              ])
            )
          )
          : new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(
                ReactiveEnergyTotalImportT0_VARh),
              new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalExportT0_VARh)
            )
          );
    }
  }

  public override TariffMeasure<decimal> ApparentEnergy_VAh
  {
    get { return TariffMeasure<decimal>.Null; }
  }
#pragma warning restore CA1707
}
