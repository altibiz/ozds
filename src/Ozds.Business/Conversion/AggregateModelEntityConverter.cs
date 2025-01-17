using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class AggregateModelEntityConverter : ConcreteModelEntityConverter<
  IAggregate, AggregateEntity>
{
  protected override AggregateEntity ToEntity(
    IAggregate model)
  {
    return model.ToEntity();
  }

  protected override IAggregate ToModel(
    AggregateEntity entity)
  {
    return entity.ToModel();
  }
}

public static class AggregateModelEntityConverterExtensions
{
  public static AggregateEntity ToEntity(
    this IAggregate model)
  {
    if (model is AbbB2xAggregateModel
      abbB2xAggregateModel)
    {
      return abbB2xAggregateModel.ToEntity();
    }

    if (model is SchneideriEM3xxxAggregateModel
      schneideriEM3xxxAggregateModel)
    {
      return schneideriEM3xxxAggregateModel.ToEntity();
    }

    throw new NotSupportedException(
      $"AggregateModel type {model.GetType().Name} is not supported."
    );
  }

  public static AggregateModel ToModel(
    this AggregateEntity entity)
  {
    if (entity is AbbB2xAggregateEntity
      abbB2xAggregateEntity)
    {
      return abbB2xAggregateEntity.ToModel();
    }

    if (entity is SchneideriEM3xxxAggregateEntity
      schneideriEM3xxAggregateEntity)
    {
      return schneideriEM3xxAggregateEntity.ToModel();
    }

    throw new NotSupportedException(
      $"AggregateEntity type {entity.GetType().Name} is not supported."
    );
  }
}
