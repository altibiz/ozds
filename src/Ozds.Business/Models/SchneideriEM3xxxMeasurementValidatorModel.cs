using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class
  SchneideriEM3xxxMeasurementValidatorModel : MeasurementValidatorModel<
  SchneideriEM3xxxMeasurementModel>
{
  [Required] public required float MinVoltage_V { get; init; }

  [Required] public required float MaxVoltage_V { get; init; }

  [Required] public required float MinCurrent_A { get; init; }

  [Required] public required float MaxCurrent_A { get; init; }

  [Required] public required float MinActivePower_W { get; init; }

  [Required] public required float MaxActivePower_W { get; init; }

  [Required] public required float MinReactivePower_VAR { get; init; }

  [Required] public required float MaxReactivePower_VAR { get; init; }

  [Required] public required float MinApparentPower_VA { get; init; }

  [Required] public required float MaxApparentPower_VA { get; init; }

  public override IEnumerable<ValidationResult> ValidateMeasurement(
    SchneideriEM3xxxMeasurementModel measurement,
    string? memberName
  )
  {
    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Voltage_V) &&
      measurement.Voltage_V.TariffUnary.DuplexAny.PhaseSum < MinVoltage_V * 3 &&
      measurement.Voltage_V.TariffUnary.DuplexAny.PhaseTrough < MinVoltage_V
    )
    {
      yield return new ValidationResult(
        $"Voltage must be greater than or equal to {MinVoltage_V}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Voltage_V) }
      );
    }

    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Voltage_V) &&
      measurement.Voltage_V.TariffUnary.DuplexAny.PhaseSum > MaxVoltage_V * 3 &&
      measurement.Voltage_V.TariffUnary.DuplexAny.PhasePeak > MaxVoltage_V
    )
    {
      yield return new ValidationResult(
        $"Voltage must be less than or equal to {MaxVoltage_V}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Voltage_V) }
      );
    }

    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Current_A) &&
      measurement.Current_A.TariffUnary.DuplexAny.PhaseSum < MinCurrent_A * 3 &&
      measurement.Current_A.TariffUnary.DuplexAny.PhaseTrough < MinCurrent_A
    )
    {
      yield return new ValidationResult(
        $"Current must be greater than or equal to {MinCurrent_A}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.Current_A) }
      );
    }

    if (
      memberName is null or nameof(SchneideriEM3xxxAggregateModel.Current_A) &&
      measurement.Current_A.TariffUnary.DuplexAny.PhaseSum > MaxCurrent_A * 3 &&
      measurement.Current_A.TariffUnary.DuplexAny.PhasePeak > MaxCurrent_A
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
      measurement.ActivePower_W.TariffUnary.DuplexAny.PhaseSum <
      MinActivePower_W * 3 &&
      measurement.ActivePower_W.TariffUnary.DuplexAny.PhaseTrough <
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
      measurement.ActivePower_W.TariffUnary.DuplexAny.PhaseSum >
      MaxActivePower_W * 3 &&
      measurement.ActivePower_W.TariffUnary.DuplexAny.PhasePeak >
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
      measurement.ReactivePower_VAR.TariffUnary.DuplexAny.PhaseSum <
      MinReactivePower_VAR * 3 &&
      measurement.ReactivePower_VAR.TariffUnary.DuplexAny.PhaseTrough <
      MinReactivePower_VAR
    )
    {
      yield return new ValidationResult(
        $"ReactivePower must be greater than or equal to {MinReactivePower_VAR}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ReactivePower_VAR) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ReactivePower_VAR) &&
      measurement.ReactivePower_VAR.TariffUnary.DuplexAny.PhaseSum >
      MaxReactivePower_VAR * 3 &&
      measurement.ReactivePower_VAR.TariffUnary.DuplexAny.PhasePeak >
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
      measurement.ApparentPower_VA.TariffUnary.DuplexAny.PhaseSum <
      MinApparentPower_VA * 3 &&
      measurement.ApparentPower_VA.TariffUnary.DuplexAny.PhaseTrough <
      MinApparentPower_VA
    )
    {
      yield return new ValidationResult(
        $"ApparentPower must be greater than or equal to {MinApparentPower_VA}.",
        new[] { nameof(SchneideriEM3xxxAggregateModel.ApparentPower_VA) }
      );
    }

    if (
      memberName is null
        or nameof(SchneideriEM3xxxAggregateModel.ApparentPower_VA) &&
      measurement.ApparentPower_VA.TariffUnary.DuplexAny.PhaseSum >
      MaxApparentPower_VA * 3 &&
      measurement.ApparentPower_VA.TariffUnary.DuplexAny.PhasePeak >
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

public static class SchneideriEM3xxxMeasurementValidatorModelExtensions
{
  public static SchneideriEM3xxxMeasurementValidatorModel ToModel(
    this SchneideriEM3xxxMeasurementValidatorEntity entity)
  {
    return new SchneideriEM3xxxMeasurementValidatorModel
    {
      Id = entity.Id,
      Title = entity.Title,
      CreatedOn = entity.CreatedOn,
      CreatedById = entity.CreatedById,
      LastUpdatedOn = entity.LastUpdatedOn,
      LastUpdatedById = entity.LastUpdatedById,
      IsDeleted = entity.IsDeleted,
      DeletedOn = entity.DeletedOn,
      DeletedById = entity.DeletedById,
      MeterId = entity.MeterId,
      MinVoltage_V = entity.MinVoltage_V,
      MaxVoltage_V = entity.MaxVoltage_V,
      MinCurrent_A = entity.MinCurrent_A,
      MaxCurrent_A = entity.MaxCurrent_A,
      MinActivePower_W = entity.MinActivePower_W,
      MaxActivePower_W = entity.MaxActivePower_W,
      MinReactivePower_VAR = entity.MinReactivePower_VAR,
      MaxReactivePower_VAR = entity.MaxReactivePower_VAR,
      MinApparentPower_VA = entity.MinApparentPower_VA,
      MaxApparentPower_VA = entity.MaxApparentPower_VA
    };
  }

  public static SchneideriEM3xxxMeasurementValidatorEntity ToEntity(
    this SchneideriEM3xxxMeasurementValidatorModel model)
  {
    return new SchneideriEM3xxxMeasurementValidatorEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      MeterId = model.MeterId,
      MinVoltage_V = model.MinVoltage_V,
      MaxVoltage_V = model.MaxVoltage_V,
      MinCurrent_A = model.MinCurrent_A,
      MaxCurrent_A = model.MaxCurrent_A,
      MinActivePower_W = model.MinActivePower_W,
      MaxActivePower_W = model.MaxActivePower_W,
      MinReactivePower_VAR = model.MinReactivePower_VAR,
      MaxReactivePower_VAR = model.MaxReactivePower_VAR,
      MinApparentPower_VA = model.MinApparentPower_VA,
      MaxApparentPower_VA = model.MaxApparentPower_VA
    };
  }
}
