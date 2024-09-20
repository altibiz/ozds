using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class
  AbbB2xMeasurementValidatorModel : MeasurementValidatorModel<
  AbbB2xMeasurementModel>
{
  [Required]
  public required decimal MinVoltage_V { get; set; }

  [Required]
  public required decimal MaxVoltage_V { get; set; }

  [Required]
  public required decimal MinCurrent_A { get; set; }

  [Required]
  public required decimal MaxCurrent_A { get; set; }

  [Required]
  public required decimal MinActivePower_W { get; set; }

  [Required]
  public required decimal MaxActivePower_W { get; set; }

  [Required]
  public required decimal MinReactivePower_VAR { get; set; }

  [Required]
  public required decimal MaxReactivePower_VAR { get; set; }

  public override IEnumerable<ValidationResult> ValidateMeasurement(
    AbbB2xMeasurementModel measurement,
    string? memberName
  )
  {
    if (
      memberName is null or nameof(AbbB2xMeasurementModel.Voltage_V) &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhaseSum()
      < MinVoltage_V * 3 &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhaseTrough()
      < MinVoltage_V
    )
    {
      yield return new ValidationResult(
        $"Voltage must be greater than or equal to {MinVoltage_V}.",
        new[] { nameof(AbbB2xMeasurementModel.Voltage_V) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.Voltage_V) &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhaseSum()
      > MaxVoltage_V * 3 &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhasePeak() > MaxVoltage_V
    )
    {
      yield return new ValidationResult(
        $"Voltage must be less than or equal to {MaxVoltage_V}.",
        new[] { nameof(AbbB2xMeasurementModel.Voltage_V) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.Current_A) &&
      measurement.Current_A.TariffUnary().DuplexAny().PhaseSum()
      < MinCurrent_A * 3 &&
      measurement.Current_A.TariffUnary().DuplexAny().PhaseTrough()
      < MinCurrent_A
    )
    {
      yield return new ValidationResult(
        $"Current must be greater than or equal to {MinCurrent_A}.",
        new[] { nameof(AbbB2xMeasurementModel.Current_A) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.Current_A) &&
      measurement.Current_A.TariffUnary().DuplexAny().PhaseSum()
      > MaxCurrent_A * 3 &&
      measurement.Current_A.TariffUnary().DuplexAny().PhasePeak() > MaxCurrent_A
    )
    {
      yield return new ValidationResult(
        $"Current must be less than or equal to {MaxCurrent_A}.",
        new[] { nameof(AbbB2xMeasurementModel.Current_A) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.ActivePower_W) &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhaseSum() <
      MinActivePower_W * 3 &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhaseTrough() <
      MinActivePower_W
    )
    {
      yield return new ValidationResult(
        $"ActivePower must be greater than or equal to {MinActivePower_W}.",
        new[] { nameof(AbbB2xMeasurementModel.ActivePower_W) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.ActivePower_W) &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhaseSum() >
      MaxActivePower_W * 3 &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhasePeak() >
      MaxActivePower_W
    )
    {
      yield return new ValidationResult(
        $"ActivePower must be less than or equal to {MaxActivePower_W}.",
        new[] { nameof(AbbB2xMeasurementModel.ActivePower_W) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.ReactivePower_VAR) &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhaseSum() <
      MinReactivePower_VAR * 3 &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhaseTrough() <
      MinReactivePower_VAR
    )
    {
      yield return new ValidationResult(
        $"ReactivePower must be greater than or equal to {MinReactivePower_VAR}.",
        new[] { nameof(AbbB2xMeasurementModel.ReactivePower_VAR) }
      );
    }

    if (
      memberName is null or nameof(AbbB2xMeasurementModel.ReactivePower_VAR) &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhaseSum() >
      MaxReactivePower_VAR * 3 &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhasePeak() >
      MaxReactivePower_VAR
    )
    {
      yield return new ValidationResult(
        $"ReactivePower must be less than or equal to {MaxReactivePower_VAR}.",
        new[] { nameof(AbbB2xMeasurementModel.ReactivePower_VAR) }
      );
    }
  }
}
