using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record AbbB2xMeasurementValidatorModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string MeterId,
  float MinVoltage_V,
  float MaxVoltage_V,
  float MinCurrent_A,
  float MaxCurrent_A,
  float MinActivePower_W,
  float MaxActivePower_W,
  float MinReactivePower_VAR,
  float MaxReactivePower_VAR
) : MeasurementValidatorModel<AbbB2xMeasurementModel>(
  Id: Id,
  Title: Title,
  CreatedOn: CreatedOn,
  CreatedById: CreatedById,
  LastUpdatedOn: LastUpdatedOn,
  LastUpdatedById: LastUpdatedById,
  IsDeleted: IsDeleted,
  DeletedOn: DeletedOn,
  DeletedById: DeletedById,
  MeterId: MeterId
)
{
  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }

  public override string? Validate(AbbB2xMeasurementModel measurement, string property)
  {
    throw new NotImplementedException();
  }
}

public static class AbbB2xMeasurementValidatorModelExtensions
{
  public static AbbB2xMeasurementValidatorModel ToModel(this AbbB2xMeasurementValidatorEntity entity) =>
    new(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.MeterId,
      entity.MinVoltage_V,
      entity.MaxVoltage_V,
      entity.MinCurrent_A,
      entity.MaxCurrent_A,
      entity.MinActivePower_W,
      entity.MaxActivePower_W,
      entity.MinReactivePower_VAR,
      entity.MaxReactivePower_VAR
    );

  public static AbbB2xMeasurementValidatorEntity ToEntity(this AbbB2xMeasurementValidatorModel model) =>
    new()
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
      MaxReactivePower_VAR = model.MaxReactivePower_VAR
    };
}
