using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RepresentativeEventModelEntityConverter : ModelEntityConverter<RepresentativeEventModel, RepresentativeEventEntity>
{
  protected override RepresentativeEventEntity ToEntity(RepresentativeEventModel model) =>
    model.ToEntity();

  protected override RepresentativeEventModel ToModel(RepresentativeEventEntity entity) =>
    entity.ToModel();
}

public static class RepresentativeEventModelEntityConverterExtensions
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
