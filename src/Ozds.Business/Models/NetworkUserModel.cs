using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record NetworkUserModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string LocationId
) : AuditableModel(
  Id: Id,
  Title: Title,
  CreatedOn: CreatedOn,
  CreatedById: CreatedById,
  LastUpdatedOn: LastUpdatedOn,
  LastUpdatedById: LastUpdatedById,
  IsDeleted: IsDeleted,
  DeletedOn: DeletedOn,
  DeletedById: DeletedById
)
{
  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class NetworkUserModelExtensions
{
  public static NetworkUserModel ToModel(this NetworkUserEntity entity) =>
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
      entity.LocationId
    );

  public static NetworkUserEntity ToEntity(this NetworkUserModel model) =>
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
      LocationId = model.LocationId
    };
}
