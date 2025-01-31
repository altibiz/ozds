namespace Ozds.Business.Conversion.Base;

public abstract class ConcreteModelEntityConverter<TModel, TEntity>
  : InitializingModelEntityConverter
  where TModel : notnull
  where TEntity : notnull
{
  public override Type EntityType
  {
    get { return typeof(TEntity); }
  }

  public override Type ModelType
  {
    get { return typeof(TModel); }
  }

  public virtual void InitializeEntity(TModel model, TEntity entity)
  {
  }

  public virtual void InitializeModel(TEntity entity, TModel model)
  {
  }

  public virtual TEntity ToEntity(TModel model)
  {
    var entity = CreateEntity();
    InitializeEntity(model, entity);
    return entity;
  }

  public virtual TModel ToModel(TEntity entity)
  {
    var model = CreateModel();
    InitializeModel(entity, model);
    return model;
  }

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

  public override object ToEntity(object model)
  {
    return ToEntity((TModel)model);
  }

  public override object ToModel(object entity)
  {
    return ToModel((TEntity)entity);
  }
}
