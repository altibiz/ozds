using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeasurementModel : MeasurementModel<
  SchneideriEM3xxxMeasurementValidatorModel>
{
  [Required] public required float VoltageL1AnyT0_V { get; set; }

  [Required] public required float VoltageL2AnyT0_V { get; set; }

  [Required] public required float VoltageL3AnyT0_V { get; set; }

  [Required] public required float CurrentL1AnyT0_A { get; set; }

  [Required] public required float CurrentL2AnyT0_A { get; set; }

  [Required] public required float CurrentL3AnyT0_A { get; set; }

  [Required] public required float ActivePowerL1NetT0_W { get; set; }

  [Required] public required float ActivePowerL2NetT0_W { get; set; }

  [Required] public required float ActivePowerL3NetT0_W { get; set; }

  [Required] public required float ReactivePowerTotalNetT0_VAR { get; set; }

  [Required] public required float ApparentPowerTotalNetT0_VA { get; set; }

  [Required] public required float ActiveEnergyL1ImportT0_Wh { get; set; }

  [Required] public required float ActiveEnergyL2ImportT0_Wh { get; set; }

  [Required] public required float ActiveEnergyL3ImportT0_Wh { get; set; }

  [Required] public required float ActiveEnergyTotalImportT0_Wh { get; set; }

  [Required] public required float ActiveEnergyTotalExportT0_Wh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalImportT0_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalExportT0_VARh { get; set; }

  [Required] public required float ActiveEnergyTotalImportT1_Wh { get; set; }

  [Required] public required float ActiveEnergyTotalImportT2_Wh { get; set; }

  public override TariffMeasure<float> Current_A
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new AnyDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            CurrentL1AnyT0_A,
            CurrentL2AnyT0_A,
            CurrentL3AnyT0_A
          )
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
            VoltageL1AnyT0_V,
            VoltageL2AnyT0_V,
            VoltageL3AnyT0_V
          )
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
          new SinglePhasicMeasure<float>(
            ReactivePowerTotalNetT0_VAR
          )
        )
      );
    }
  }

  public override TariffMeasure<float> ApparentPower_VA
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new NetDuplexMeasure<float>(
          new SinglePhasicMeasure<float>(
            ApparentPowerTotalNetT0_VA
          )
        )
      );
    }
  }


  public override TariffMeasure<float> ActiveEnergy_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
             && ActiveEnergyL2ImportT0_Wh is not 0
             && ActiveEnergyL3ImportT0_Wh is not 0
        ? new CompositeTariffMeasure<float>(new List<TariffMeasure<float>>
        {
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new TriPhasicMeasure<float>(
                ActiveEnergyL1ImportT0_Wh,
                ActiveEnergyL2ImportT0_Wh,
                ActiveEnergyL3ImportT0_Wh
              ),
              new SinglePhasicMeasure<float>(
                ActiveEnergyTotalExportT0_Wh
              )
            )
          ),
          new BinaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasure<float>(ActiveEnergyTotalImportT1_Wh),
              PhasicMeasure<float>.Null
            ),
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasure<float>(ActiveEnergyTotalImportT2_Wh),
              PhasicMeasure<float>.Null
            )
          )
        })
        : new CompositeTariffMeasure<float>(new List<TariffMeasure<float>>
        {
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasure<float>(ActiveEnergyTotalImportT0_Wh),
              new SinglePhasicMeasure<float>(ActiveEnergyTotalExportT0_Wh)
            )
          ),
          new BinaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasure<float>(ActiveEnergyTotalImportT1_Wh),
              PhasicMeasure<float>.Null
            ),
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasure<float>(ActiveEnergyTotalImportT2_Wh),
              PhasicMeasure<float>.Null
            )
          )
        });
    }
  }

  public override TariffMeasure<float> ReactiveEnergy_VARh
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new ImportExportDuplexMeasure<float>(
          new SinglePhasicMeasure<float>(ReactiveEnergyTotalImportT0_VARh),
          new SinglePhasicMeasure<float>(ReactiveEnergyTotalExportT0_VARh)
        )
      );
    }
  }

  public override TariffMeasure<float> ApparentEnergy_VAh
  {
    get { return TariffMeasure<float>.Null; }
  }
}
