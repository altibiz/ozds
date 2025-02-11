using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class InheritingModelDocumentEntityConverter<
  TModel,
  TSuperModel,
  TEntity,
  TSuperEntity>(IServiceProvider serviceProvider)
  : ConcreteModelDocumentEntityConverter<TModel, TEntity>
  where TModel : notnull, TSuperModel
  where TEntity : notnull, TSuperEntity
  where TSuperModel : notnull
  where TSuperEntity : notnull
{
  private InitializingModelDocumentEntityConverter? _baseEntityConverter;

  public override void InitializeEntity(TModel model, TEntity entity)
  {
    _baseEntityConverter ??=
      serviceProvider
          .GetServices<IModelDocumentEntityConverter>()
          .FirstOrDefault(x => x.EntityType == typeof(TSuperEntity))
        as InitializingModelDocumentEntityConverter
      ?? throw new InvalidOperationException(
        $"No model entity converter found for type {typeof(TSuperEntity)}");

    base.InitializeEntity(model, entity);
    _baseEntityConverter.InitializeEntity(model, entity);
  }
}
