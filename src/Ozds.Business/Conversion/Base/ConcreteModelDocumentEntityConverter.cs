namespace Ozds.Business.Conversion.Base;

public abstract class ConcreteModelDocumentEntityConverter<TModel, TEntity>
  : InitializingModelDocumentEntityConverter
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

  public virtual TEntity ToEntity(TModel model)
  {
    var entity = CreateEntity();
    InitializeEntity(model, entity);
    return entity;
  }

  public override bool CanConvertToEntity(Type modelType)
  {
    return modelType.IsAssignableTo(typeof(TModel));
  }

  public override object BoxEntity()
  {
    return CreateEntity();
  }

  public override void InitializeEntity(object model, object entity)
  {
    InitializeEntity((TModel)model, (TEntity)entity);
  }

  public virtual TEntity CreateEntity()
  {
    return Activator.CreateInstance<TEntity>();
  }

  public override object ToEntity(object model)
  {
    return ToEntity((TModel)model);
  }
}
