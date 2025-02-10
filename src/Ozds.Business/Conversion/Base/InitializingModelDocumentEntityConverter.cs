using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class InitializingModelDocumentEntityConverter : IModelDocumentEntityConverter
{
  public abstract Type EntityType { get; }

  public abstract Type ModelType { get; }

  public abstract bool CanConvertToEntity(Type modelType);

  public virtual object ToEntity(object model)
  {
    var entity = BoxEntity();
    InitializeEntity(model, entity);
    return entity;
  }

  public abstract object BoxEntity();

  public virtual void InitializeEntity(object model, object entity)
  {
  }
}
