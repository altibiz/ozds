using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.System;

public class RepresentativeEventModelEntityConverter : ConcreteModelEntityConverter<
  RepresentativeEventModel, RepresentativeEventEntity>
{
  protected override RepresentativeEventEntity ToEntity(
    RepresentativeEventModel model)
  {
    return model.ToEntity();
  }

  protected override RepresentativeEventModel ToModel(
    RepresentativeEventEntity entity)
  {
    return entity.ToModel();
  }
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
      Content = entity.Content,
      RepresentativeId = entity.RepresentativeId,
      Categories = entity.Categories.Select(c => c.ToModel()).ToList()
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
      Content = model.Content,
      RepresentativeId = model.RepresentativeId,
      Categories = model.Categories.Select(c => c.ToEntity()).ToList()
    };
  }
}
