using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class
  SchneideriEM3xxxMeasurementValidatorModel : MeasurementValidatorModel<
  SchneideriEM3xxxMeasurementModel>
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

  [Required]
  public required decimal MinApparentPower_VA { get; set; }

  [Required]
  public required decimal MaxApparentPower_VA { get; set; }

  public static SchneideriEM3xxxMeasurementValidatorModel New()
  {
    return new SchneideriEM3xxxMeasurementValidatorModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      MinVoltage_V = default,
      MaxVoltage_V = default,
      MinCurrent_A = default,
      MaxCurrent_A = default,
      MinActivePower_W = default,
      MaxActivePower_W = default,
      MinReactivePower_VAR = default,
      MaxReactivePower_VAR = default,
      MinApparentPower_VA = default,
      MaxApparentPower_VA = default
    };
  }

  public override IEnumerable<ValidationResult> ValidateMeasurement(
    SchneideriEM3xxxMeasurementModel measurement,
    string? memberName
  )
  {
    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Voltage_V) &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhaseSum()
      < MinVoltage_V * 3 &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhaseTrough()
      < MinVoltage_V
    )
    {
      yield return new ValidationResult(
        $"Voltage must be greater than or equal to {MinVoltage_V}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Voltage_V) }
      );
    }

    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Voltage_V) &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhaseSum()
      > MaxVoltage_V * 3 &&
      measurement.Voltage_V.TariffUnary().DuplexAny().PhasePeak() > MaxVoltage_V
    )
    {
      yield return new ValidationResult(
        $"Voltage must be less than or equal to {MaxVoltage_V}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Voltage_V) }
      );
    }

    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Current_A) &&
      measurement.Current_A.TariffUnary().DuplexAny().PhaseSum()
      < MinCurrent_A * 3 &&
      measurement.Current_A.TariffUnary().DuplexAny().PhaseTrough()
      < MinCurrent_A
    )
    {
      yield return new ValidationResult(
        $"Current must be greater than or equal to {MinCurrent_A}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Current_A) }
      );
    }

    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Current_A) &&
      measurement.Current_A.TariffUnary().DuplexAny().PhaseSum()
      > MaxCurrent_A * 3 &&
      measurement.Current_A.TariffUnary().DuplexAny().PhasePeak() > MaxCurrent_A
    )
    {
      yield return new ValidationResult(
        $"Current must be less than or equal to {MaxCurrent_A}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Current_A) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ActivePower_W) &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhaseSum() <
      MinActivePower_W * 3 &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhaseTrough() <
      MinActivePower_W
    )
    {
      yield return new ValidationResult(
        $"ActivePower must be greater than or equal to {MinActivePower_W}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ActivePower_W) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ActivePower_W) &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhaseSum() >
      MaxActivePower_W * 3 &&
      measurement.ActivePower_W.TariffUnary().DuplexAny().PhasePeak() >
      MaxActivePower_W
    )
    {
      yield return new ValidationResult(
        $"ActivePower must be less than or equal to {MaxActivePower_W}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ActivePower_W) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ReactivePower_VAR) &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhaseSum() <
      MinReactivePower_VAR * 3 &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhaseTrough() <
      MinReactivePower_VAR
    )
    {
      yield return new ValidationResult(
        $"ReactivePower must be greater than or equal to {
          MinReactivePower_VAR
        }.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ReactivePower_VAR) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ReactivePower_VAR) &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhaseSum() >
      MaxReactivePower_VAR * 3 &&
      measurement.ReactivePower_VAR.TariffUnary().DuplexAny().PhasePeak() >
      MaxReactivePower_VAR
    )
    {
      yield return new ValidationResult(
        $"ReactivePower must be less than or equal to {MaxReactivePower_VAR}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ReactivePower_VAR) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ApparentPower_VA) &&
      measurement.ApparentPower_VA.TariffUnary().DuplexAny().PhaseSum() <
      MinApparentPower_VA * 3 &&
      measurement.ApparentPower_VA.TariffUnary().DuplexAny().PhaseTrough() <
      MinApparentPower_VA
    )
    {
      yield return new ValidationResult(
        $"ApparentPower must be greater than or equal to {
          MinApparentPower_VA
        }.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ApparentPower_VA) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ApparentPower_VA) &&
      measurement.ApparentPower_VA.TariffUnary().DuplexAny().PhaseSum() >
      MaxApparentPower_VA * 3 &&
      measurement.ApparentPower_VA.TariffUnary().DuplexAny().PhasePeak() >
      MaxApparentPower_VA
    )
    {
      yield return new ValidationResult(
        $"ApparentPower must be less than or equal to {MaxApparentPower_VA}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ApparentPower_VA) }
      );
    }
  }
}
