using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class RepresentativeEventModel : EventModel
{
  [Required] public required string RepresentativeId { get; set; }
}

public static class RepresentativeEventModelExtensions
{
  public static RepresentativeEventModel ToModel(
    this RepresentativeEventEntity entity)
  {
    return new RepresentativeEventModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Description = entity.Description,
      RepresentativeId = entity.RepresentativeId
    };
  }

  public static RepresentativeEventEntity ToEntity(
    this RepresentativeEventModel model)
  {
    return new RepresentativeEventEntity
    {
      Id = model.Id,
      Title = model.Title,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      RepresentativeId = model.RepresentativeId
    };
  }
}
