using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Agnostic;

public class AgnosticModelEntityConverter(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public Type EntityType(Type type)
  {
    return _serviceProvider
        .GetServices<IModelEntityConverter>()
        .Where(
          converter =>
            converter.CanConvertToEntity(type))
        .DefaultIfEmpty(null)
        .Aggregate((acc, next) =>
          acc is null
            ? null
            : next!.EntityType().IsAssignableTo(acc.EntityType())
              ? next
              : acc)
        ?.EntityType()
      ?? throw new InvalidOperationException(
        $"No converter found for model {type}.");
  }

  public object ToEntity(object model)
  {
    return _serviceProvider
        .GetServices<IModelEntityConverter>()
        .Where(
          converter =>
            converter.CanConvertToEntity(model.GetType()))
        .DefaultIfEmpty(null)
        .Aggregate((acc, next) =>
          acc is null
            ? null
            : next!.EntityType().IsAssignableTo(acc.EntityType())
              ? next
              : acc)
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
        .Where(
          converter =>
            converter.CanConvertToModel(entity.GetType()))
        .DefaultIfEmpty(null)
        .Aggregate((acc, next) =>
          acc is null
            ? null
            : next!.EntityType().IsAssignableTo(acc.EntityType())
              ? next
              : acc)
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
