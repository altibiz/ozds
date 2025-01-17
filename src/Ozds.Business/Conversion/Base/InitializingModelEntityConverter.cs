using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class InitializingModelEntityConverter : IModelEntityConverter
{
  public abstract Type EntityType { get; }

  public abstract Type ModelType { get; }

  public abstract bool CanConvertToEntity(Type modelType);

  public abstract bool CanConvertToModel(Type entityType);

  public abstract object BoxEntity();

  public abstract object BoxModel();

  public virtual void InitializeEntity(object model, object entity) { }

  public virtual void InitializeModel(object entity, object model) { }

  public virtual object ToEntity(object model)
  {
    var entity = BoxEntity();
    InitializeEntity(model, entity);
    return entity;
  }

  public virtual object ToModel(object entity)
  {
    var model = BoxModel();
    InitializeModel(entity, model);
    return model;
  }
}
