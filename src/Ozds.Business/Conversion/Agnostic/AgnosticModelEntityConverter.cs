using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Agnostic;

public class AgnosticModelEntityConverter
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticModelEntityConverter(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public Type DbSetType(Type type)
  {
    if (type == typeof(SchneideriEM3xxxMeasurementModel))
    {
      return typeof(SchneideriEM3xxxMeasurementEntity);
    }
    if (type == typeof(AbbB2xMeasurementModel))
    {
      return typeof(AbbB2xMeasurementEntity);
    }
    if (type == typeof(SchneideriEM3xxxAggregateModel))
    {
      return typeof(SchneideriEM3xxxAggregateEntity);
    }
    if (type == typeof(AbbB2xAggregateModel))
    {
      return typeof(AbbB2xAggregateEntity);
    }

    throw new NotImplementedException();
  }

  public object ToEntity(object model)
  {
    return _serviceProvider
             .GetServices<IModelEntityConverter>()
             .FirstOrDefault(converter =>
               converter.CanConvertToEntity(model.GetType()))
             ?.ToEntity(model)
           ?? throw new InvalidOperationException(
             $"No converter found for model {model.GetType()}.");
  }

  public TEntity ToEntity<TEntity>(object model)
    where TEntity : class
  {
    return ToEntity(model) as TEntity
           ?? throw new InvalidOperationException(
             $"No converter found for model {model.GetType()}.");
  }

  public object ToModel(object entity)
  {
    return _serviceProvider
             .GetServices<IModelEntityConverter>()
             .FirstOrDefault(converter =>
               converter.CanConvertToModel(entity.GetType()))
             ?.ToModel(entity)
           ?? throw new InvalidOperationException(
             $"No converter found for entity {entity.GetType()}.");
  }

  public TModel ToModel<TModel>(object entity)
    where TModel : class
  {
    return ToModel(entity) as TModel
           ?? throw new InvalidOperationException(
             $"No converter found for entity {entity.GetType()}.");
  }
}
