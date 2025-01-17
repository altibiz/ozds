namespace Ozds.Business.Conversion.Base;

public abstract class ConcreteModelEntityConverter<TModel, TEntity>
  : InitializingModelEntityConverter
  where TModel : notnull
  where TEntity : notnull
{
  public virtual void InitializeEntity(TModel model, TEntity entity) { }

  public virtual void InitializeModel(TEntity entity, TModel model) { }

  public override Type EntityType => typeof(TEntity);

  public override Type ModelType => typeof(TModel);

  public override bool CanConvertToEntity(Type modelType)
  {
    return modelType.IsAssignableTo(typeof(TModel));
  }

  public override bool CanConvertToModel(Type entityType)
  {
    return entityType.IsAssignableTo(typeof(TEntity));
  }

  public override object BoxEntity()
  {
    return CreateEntity();
  }

  public override object BoxModel()
  {
    return CreateModel();
  }

  public override void InitializeModel(object entity, object model)
  {
    InitializeModel((TEntity)entity, (TModel)model);
  }

  public override void InitializeEntity(object model, object entity)
  {
    InitializeEntity((TModel)model, (TEntity)entity);
  }

  public virtual TEntity CreateEntity()
  {
    return Activator.CreateInstance<TEntity>();
  }

  public virtual TModel CreateModel()
  {
    return Activator.CreateInstance<TModel>();
  }
}
