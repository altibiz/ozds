using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeasurementModel : MeasurementModel<
  SchneideriEM3xxxMeasurementValidatorModel>
{
  [Required] public required float VoltageL1AnyT0_V { get; init; }

  [Required] public required float VoltageL2AnyT0_V { get; init; }

  [Required] public required float VoltageL3AnyT0_V { get; init; }

  [Required] public required float CurrentL1AnyT0_A { get; init; }

  [Required] public required float CurrentL2AnyT0_A { get; init; }

  [Required] public required float CurrentL3AnyT0_A { get; init; }

  [Required] public required float ActivePowerL1NetT0_W { get; init; }

  [Required] public required float ActivePowerL2NetT0_W { get; init; }

  [Required] public required float ActivePowerL3NetT0_W { get; init; }

  [Required] public required float ReactivePowerTotalNetT0_VAR { get; init; }

  [Required] public required float ApparentPowerTotalNetT0_VA { get; init; }

  [Required] public required float ActiveEnergyL1ImportT0_Wh { get; init; }

  [Required] public required float ActiveEnergyL2ImportT0_Wh { get; init; }

  [Required] public required float ActiveEnergyL3ImportT0_Wh { get; init; }

  [Required] public required float ActiveEnergyTotalImportT0_Wh { get; init; }

  [Required] public required float ActiveEnergyTotalExportT0_Wh { get; init; }

  [Required]
  public required float ReactiveEnergyTotalImportT0_VARh { get; init; }

  [Required]
  public required float ReactiveEnergyTotalExportT0_VARh { get; init; }

  [Required] public required float ActiveEnergyTotalImportT1_Wh { get; init; }

  [Required] public required float ActiveEnergyTotalImportT2_Wh { get; init; }

  public override TariffMeasure Current_A
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            CurrentL1AnyT0_A,
            CurrentL2AnyT0_A,
            CurrentL3AnyT0_A
          )
        )
      );
    }
  }

  public override TariffMeasure Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            VoltageL1AnyT0_V,
            VoltageL2AnyT0_V,
            VoltageL3AnyT0_V
          )
        )
      );
    }
  }

  public override TariffMeasure ActivePower_W
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new TriPhasicMeasure(
            ActivePowerL1NetT0_W,
            ActivePowerL2NetT0_W,
            ActivePowerL3NetT0_W
          )
        )
      );
    }
  }

  public override TariffMeasure ReactivePower_VAR
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new SinglePhasicMeasure(
            ReactivePowerTotalNetT0_VAR
          )
        )
      );
    }
  }

  public override TariffMeasure ApparentPower_VA
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new SinglePhasicMeasure(
            ApparentPowerTotalNetT0_VA
          )
        )
      );
    }
  }


  public override TariffMeasure ActiveEnergy_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
             && ActiveEnergyL2ImportT0_Wh is not 0
             && ActiveEnergyL3ImportT0_Wh is not 0
        ? new CompositeTariffMeasure(new List<TariffMeasure>
        {
          new UnaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new TriPhasicMeasure(
                ActiveEnergyL1ImportT0_Wh,
                ActiveEnergyL2ImportT0_Wh,
                ActiveEnergyL3ImportT0_Wh
              ),
              new SinglePhasicMeasure(
                ActiveEnergyTotalExportT0_Wh
              )
            )
          ),
          new BinaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT1_Wh),
              PhasicMeasure.Null
            ),
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT2_Wh),
              PhasicMeasure.Null
            )
          )
        })
        : new CompositeTariffMeasure(new List<TariffMeasure>
        {
          new UnaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT0_Wh),
              new SinglePhasicMeasure(ActiveEnergyTotalExportT0_Wh)
            )
          ),
          new BinaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT1_Wh),
              PhasicMeasure.Null
            ),
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT2_Wh),
              PhasicMeasure.Null
            )
          )
        });
    }
  }

  public override TariffMeasure ReactiveEnergy_VARh
  {
    get
    {
      return new UnaryTariffMeasure(
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ReactiveEnergyTotalImportT0_VARh),
          new SinglePhasicMeasure(ReactiveEnergyTotalExportT0_VARh)
        )
      );
    }
  }

  public override TariffMeasure ApparentEnergy_VAh
  {
    get { return TariffMeasure.Null; }
  }
}
