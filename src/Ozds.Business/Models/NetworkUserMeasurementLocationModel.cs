using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record NetworkUserMeasurementLocationModel(
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
  string NetworkUserId
) : MeasurementLocationModel(
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById,
  MeterId
)
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class NetworkUserMeasurementLocationModelExtensions
{
  public static NetworkUserMeasurementLocationModel ToModel(
    this NetworkUserMeasurementLocationEntity entity)
  {
    return new NetworkUserMeasurementLocationModel(
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
      entity.NetworkUserId
    );
  }

  public static NetworkUserMeasurementLocationEntity ToEntity(
    this NetworkUserMeasurementLocationModel model)
  {
    return new NetworkUserMeasurementLocationEntity
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
      NetworkUserId = model.NetworkUserId
    };
  }
}
