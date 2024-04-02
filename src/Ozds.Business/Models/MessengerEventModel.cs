using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class MessengerEventModel : EventModel
{
  [Required] public required string MessengerId { get; set; }
}

public static class MessengerEventModelExtensions
{
  public static MessengerEventModel ToModel(this MessengerEventEntity entity)
  {
    return new MessengerEventModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Description = entity.Description,
      MessengerId = entity.MessengerId
    };
  }

  public static MessengerEventEntity ToEntity(this MessengerEventModel model)
  {
    return new MessengerEventEntity
    {
      Id = model.Id,
      Title = model.Title,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      MessengerId = model.MessengerId
    };
  }
}
