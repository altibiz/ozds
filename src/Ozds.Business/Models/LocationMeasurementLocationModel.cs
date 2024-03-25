using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record LocationMeasurementLocationModel(
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
  string LocationId
) : MeasurementLocationModel(
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
}

public static class LocationMeasurementLocationModelExtensions
{
  public static LocationMeasurementLocationModel ToModel(this LocationMeasurementLocationEntity entity) =>
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
      entity.LocationId
    );

  public static LocationMeasurementLocationEntity ToEntity(this LocationMeasurementLocationModel model) =>
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
      LocationId = model.LocationId
    };
}
