using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class MessengerModel : AuditableModel
{
  public required string LocationId { get; set; }
}

public static class MessengerModelExtensions
{
  public static MessengerModel ToModel(this MessengerEntity entity)
  {
    return new MessengerModel
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
      LocationId = entity.LocationId
    };
  }

  public static MessengerEntity ToEntity(this MessengerModel model)
  {
    return new MessengerEntity
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
}
