using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class InheritingModelUserEntityConverter<
  TModel,
  TSuperModel,
  TEntity,
  TSuperEntity>(IServiceProvider serviceProvider)
  : ConcreteModelUserEntityConverter<TModel, TEntity>
  where TModel : notnull, TSuperModel
  where TEntity : notnull, TSuperEntity
  where TSuperModel : notnull
  where TSuperEntity : notnull
{
  private InitializingModelUserEntityConverter? _baseEntityConverter;

  private InitializingModelUserEntityConverter? _baseModelConverter;

  public override void InitializeEntity(TModel model, TEntity entity)
  {
    _baseEntityConverter ??=
      serviceProvider
          .GetServices<IModelUserEntityConverter>()
          .FirstOrDefault(x => x.EntityType == typeof(TSuperEntity))
        as InitializingModelUserEntityConverter
      ?? throw new InvalidOperationException(
        $"No model entity converter found for type {typeof(TSuperEntity)}");

    base.InitializeEntity(model, entity);
    _baseEntityConverter.InitializeEntity(model, entity);
  }

  public override void InitializeModel(TEntity entity, TModel model)
  {
    _baseModelConverter ??=
      serviceProvider
          .GetServices<IModelUserEntityConverter>()
          .FirstOrDefault(x => x.ModelType == typeof(TSuperModel))
        as InitializingModelUserEntityConverter
      ?? throw new InvalidOperationException(
        $"No model entity converter found for type {typeof(TSuperModel)}");

    base.InitializeModel(entity, model);
    _baseModelConverter.InitializeModel(entity, model);
  }
}
