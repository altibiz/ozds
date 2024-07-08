using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class
  AbbB2xMeasurementModel : MeasurementModel<AbbB2xMeasurementValidatorModel>
{
  [Required]
  public required float VoltageL1AnyT0_V { get; set; }

  [Required]
  public required float VoltageL2AnyT0_V { get; set; }

  [Required]
  public required float VoltageL3AnyT0_V { get; set; }

  [Required]
  public required float CurrentL1AnyT0_A { get; set; }

  [Required]
  public required float CurrentL2AnyT0_A { get; set; }

  [Required]
  public required float CurrentL3AnyT0_A { get; set; }

  [Required]
  public required float ActivePowerL1NetT0_W { get; set; }

  [Required]
  public required float ActivePowerL2NetT0_W { get; set; }

  [Required]
  public required float ActivePowerL3NetT0_W { get; set; }

  [Required]
  public required float ReactivePowerL1NetT0_VAR { get; set; }

  [Required]
  public required float ReactivePowerL2NetT0_VAR { get; set; }

  [Required]
  public required float ReactivePowerL3NetT0_VAR { get; set; }

  [Required]
  public required float ActiveEnergyL1ImportT0_Wh { get; set; }

  [Required]
  public required float ActiveEnergyL2ImportT0_Wh { get; set; }

  [Required]
  public required float ActiveEnergyL3ImportT0_Wh { get; set; }

  [Required]
  public required float ActiveEnergyL1ExportT0_Wh { get; set; }

  [Required]
  public required float ActiveEnergyL2ExportT0_Wh { get; set; }

  [Required]
  public required float ActiveEnergyL3ExportT0_Wh { get; set; }

  [Required]
  public required float ReactiveEnergyL1ImportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyL2ImportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyL3ImportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyL1ExportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyL2ExportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyL3ExportT0_VARh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT0_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalExportT0_Wh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalImportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalExportT0_VARh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT1_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT2_Wh { get; set; }

  public override TariffMeasure<float> Current_A
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new AnyDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            CurrentL1AnyT0_A, CurrentL2AnyT0_A,
            CurrentL3AnyT0_A)
        )
      );
    }
  }

  public override TariffMeasure<float> Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new AnyDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            VoltageL1AnyT0_V, VoltageL2AnyT0_V,
            VoltageL3AnyT0_V)
        )
      );
    }
  }

  public override TariffMeasure<float> ActivePower_W
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new NetDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            ActivePowerL1NetT0_W,
            ActivePowerL2NetT0_W,
            ActivePowerL3NetT0_W
          )
        )
      );
    }
  }

  public override TariffMeasure<float> ReactivePower_VAR
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new NetDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            ReactivePowerL1NetT0_VAR,
            ReactivePowerL2NetT0_VAR,
            ReactivePowerL3NetT0_VAR
          )
        )
      );
    }
  }

  public override TariffMeasure<float> ApparentPower_VA
  {
    get { return TariffMeasure<float>.Null; }
  }

  public override TariffMeasure<float> ActiveEnergy_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
        || ActiveEnergyL2ImportT0_Wh is not 0
        || ActiveEnergyL3ImportT0_Wh is not 0
        || ActiveEnergyL1ExportT0_Wh is not 0
        || ActiveEnergyL2ExportT0_Wh is not 0
        || ActiveEnergyL3ExportT0_Wh is not 0
          ? new CompositeTariffMeasure<float>(
          [
            new UnaryTariffMeasure<float>(
              new ImportExportDuplexMeasure<float>(
                new CompositePhasicMeasure<float>(
                [
                  new TriPhasicMeasure<float>(
                    ActiveEnergyL1ImportT0_Wh,
                    ActiveEnergyL2ImportT0_Wh,
                    ActiveEnergyL3ImportT0_Wh
                  ),
                  new SinglePhasicMeasureSum<float>(
                    ActiveEnergyTotalImportT0_Wh)
                ]),
                new CompositePhasicMeasure<float>(
                [
                  new TriPhasicMeasure<float>(
                    ActiveEnergyL1ExportT0_Wh,
                    ActiveEnergyL2ExportT0_Wh,
                    ActiveEnergyL3ExportT0_Wh
                  ),
                  new SinglePhasicMeasureSum<float>(
                    ActiveEnergyTotalExportT0_Wh)
                ])
              )
            ),
            new BinaryTariffMeasure<float>(
              new ImportExportDuplexMeasure<float>(
                new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT1_Wh),
                PhasicMeasure<float>.Null
              ),
              new ImportExportDuplexMeasure<float>(
                new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT2_Wh),
                PhasicMeasure<float>.Null
              )
            )
          ])
          : new CompositeTariffMeasure<float>(
          [
            new UnaryTariffMeasure<float>(
              new ImportExportDuplexMeasure<float>(
                new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT0_Wh),
                new SinglePhasicMeasureSum<float>(ActiveEnergyTotalExportT0_Wh)
              )
            ),
            new BinaryTariffMeasure<float>(
              new ImportExportDuplexMeasure<float>(
                new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT1_Wh),
                PhasicMeasure<float>.Null
              ),
              new ImportExportDuplexMeasure<float>(
                new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT2_Wh),
                PhasicMeasure<float>.Null
              )
            )
          ]);
    }
  }

  public override TariffMeasure<float> ReactiveEnergy_VARh
  {
    get
    {
      return ReactiveEnergyL1ImportT0_VARh is not 0
        || ReactiveEnergyL2ImportT0_VARh is not 0
        || ReactiveEnergyL3ImportT0_VARh is not 0
        || ReactiveEnergyL1ExportT0_VARh is not 0
        || ReactiveEnergyL2ExportT0_VARh is not 0
        || ReactiveEnergyL3ExportT0_VARh is not 0
          ? new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new CompositePhasicMeasure<float>(
              [
                new TriPhasicMeasure<float>(
                  ReactiveEnergyL1ImportT0_VARh,
                  ReactiveEnergyL2ImportT0_VARh,
                  ReactiveEnergyL3ImportT0_VARh
                ),
                new SinglePhasicMeasureSum<float>(
                  ReactiveEnergyTotalImportT0_VARh)
              ]),
              new CompositePhasicMeasure<float>(
              [
                new TriPhasicMeasure<float>(
                  ReactiveEnergyL1ExportT0_VARh,
                  ReactiveEnergyL2ExportT0_VARh,
                  ReactiveEnergyL3ExportT0_VARh
                ),
                new SinglePhasicMeasureSum<float>(
                  ReactiveEnergyTotalExportT0_VARh)
              ])
            )
          )
          : new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(
                ReactiveEnergyTotalImportT0_VARh),
              new SinglePhasicMeasureSum<float>(
                ReactiveEnergyTotalExportT0_VARh)
            )
          );
    }
  }

  public override TariffMeasure<float> ApparentEnergy_VAh
  {
    get { return TariffMeasure<float>.Null; }
  }
}
