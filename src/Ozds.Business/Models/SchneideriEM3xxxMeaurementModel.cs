using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeasurementModel : MeasurementModel<
  SchneideriEM3xxxMeasurementValidatorModel>
{
  [Required]
  public required decimal VoltageL1AnyT0_V { get; set; }

  [Required]
  public required decimal VoltageL2AnyT0_V { get; set; }

  [Required]
  public required decimal VoltageL3AnyT0_V { get; set; }

  [Required]
  public required decimal CurrentL1AnyT0_A { get; set; }

  [Required]
  public required decimal CurrentL2AnyT0_A { get; set; }

  [Required]
  public required decimal CurrentL3AnyT0_A { get; set; }

  [Required]
  public required decimal ActivePowerL1NetT0_W { get; set; }

  [Required]
  public required decimal ActivePowerL2NetT0_W { get; set; }

  [Required]
  public required decimal ActivePowerL3NetT0_W { get; set; }

  [Required]
  public required decimal ReactivePowerTotalNetT0_VAR { get; set; }

  [Required]
  public required decimal ApparentPowerTotalNetT0_VA { get; set; }

  [Required]
  public required decimal ActiveEnergyL1ImportT0_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyL2ImportT0_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyL3ImportT0_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalExportT0_Wh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalImportT0_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalExportT0_VARh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2_Wh { get; set; }

  public override TariffMeasure<decimal> Current_A
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(
            CurrentL1AnyT0_A,
            CurrentL2AnyT0_A,
            CurrentL3AnyT0_A
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
            VoltageL1AnyT0_V,
            VoltageL2AnyT0_V,
            VoltageL3AnyT0_V
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
          new TriPhasicMeasure<decimal>(
            ActivePowerL1NetT0_W,
            ActivePowerL2NetT0_W,
            ActivePowerL3NetT0_W
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
          new SinglePhasicMeasureSum<decimal>(
            ReactivePowerTotalNetT0_VAR
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ApparentPower_VA
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new NetDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(
            ApparentPowerTotalNetT0_VA
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ActiveEnergy_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
        && ActiveEnergyL2ImportT0_Wh is not 0
        && ActiveEnergyL3ImportT0_Wh is not 0
          ? new CompositeTariffMeasure<decimal>(
          [
            new UnaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new TriPhasicMeasure<decimal>(
                  ActiveEnergyL1ImportT0_Wh,
                  ActiveEnergyL2ImportT0_Wh,
                  ActiveEnergyL3ImportT0_Wh
                ),
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalExportT0_Wh
                )
              )
            ),
            new BinaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT1_Wh),
                PhasicMeasure<decimal>.Null
              ),
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT2_Wh),
                PhasicMeasure<decimal>.Null
              )
            )
          ])
          : new CompositeTariffMeasure<decimal>(
          [
            new UnaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT0_Wh),
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalExportT0_Wh)
              )
            ),
            new BinaryTariffMeasure<decimal>(
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT1_Wh),
                PhasicMeasure<decimal>.Null
              ),
              new ImportExportDuplexMeasure<decimal>(
                new SinglePhasicMeasureSum<decimal>(
                  ActiveEnergyTotalImportT2_Wh),
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
      return new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(ReactiveEnergyTotalImportT0_VARh),
          new SinglePhasicMeasureSum<decimal>(ReactiveEnergyTotalExportT0_VARh)
        )
      );
    }
  }

  public override TariffMeasure<decimal> ApparentEnergy_VAh
  {
    get { return TariffMeasure<decimal>.Null; }
  }
}
