using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record AbbB2xMeterModel(
  string Id,
  string Title,
  DateTimeOffset CreationDate,
  string? CreatedById,
  DateTimeOffset? LastUpdateDate,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletionDate,
  string? DeletedById,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId,
  string? MessengerId,
  float ConnectionPower_W,
  List<PhaseModel> Phases
) : MeterModel(
  Id,
  Title,
  CreationDate,
  CreatedById,
  LastUpdateDate,
  LastUpdatedById,
  IsDeleted,
  DeletionDate,
  DeletedById,
  NetworkUserMeasurementLocationId,
  LocationMeasurementLocationId,
  MessengerId,
  ConnectionPower_W,
  Phases
)
{
  public override ICapabilities Capabilities => new AbbB2xCapabilities();

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class AbbB2xMeterModelExtensions
{
  public static AbbB2xMeterEntity ToEntity(this AbbB2xMeterModel model)
  {
    return new AbbB2xMeterEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreationDate,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdateDate,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletionDate,
      DeletedById = model.DeletedById,
      NetworkUserMeasurementLocationId = model.NetworkUserMeasurementLocationId,
      LocationMeasurementLocationId = model.LocationMeasurementLocationId,
      MessengerId = model.MessengerId,
      ConnectionPower_W = model.ConnectionPower_W,
      Phases = model.Phases.Select(phase => phase.ToEntity()).ToList()
    };
  }

  public static AbbB2xMeterModel ToModel(this AbbB2xMeterEntity entity)
  {
    return new AbbB2xMeterModel(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.NetworkUserMeasurementLocationId,
      entity.LocationMeasurementLocationId,
      entity.MessengerId,
      entity.ConnectionPower_W,
      entity.Phases.Select(phase => phase.ToModel()).ToList()
    );
  }
}
