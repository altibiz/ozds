using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class InheritingModelCachingEntityConverter<
  TModel,
  TSuperModel,
  TEntity,
  TSuperEntity
>(IServiceProvider serviceProvider)
  : ConcreteModelCachingEntityConverter<TModel, TEntity>
  where TModel : notnull, TSuperModel
  where TEntity : notnull, TSuperEntity
  where TSuperModel : notnull
  where TSuperEntity : notnull
{
  private InitializingModelCachingEntityConverter? _baseEntityConverter;

  private InitializingModelCachingEntityConverter? _baseModelConverter;

  public override void InitializeEntity(TModel model, TEntity entity)
  {
    _baseEntityConverter ??=
      serviceProvider
        .GetServices<IModelCachingEntityConverter>()
        .FirstOrDefault(x => x.EntityType == typeof(TSuperEntity))
        as InitializingModelCachingEntityConverter
      ?? throw new InvalidOperationException(
        $"No model entity converter found for type {typeof(TSuperEntity)}"
      );

    base.InitializeEntity(model, entity);
    _baseEntityConverter.InitializeEntity(model, entity);
  }

  public override void InitializeModel(TEntity entity, TModel model)
  {
    _baseModelConverter ??=
      serviceProvider
        .GetServices<IModelCachingEntityConverter>()
        .FirstOrDefault(x => x.ModelType == typeof(TSuperModel))
        as InitializingModelCachingEntityConverter
      ?? throw new InvalidOperationException(
        $"No model entity converter found for type {typeof(TSuperModel)}"
      );

    base.InitializeModel(entity, model);
    _baseModelConverter.InitializeModel(entity, model);
  }
}
