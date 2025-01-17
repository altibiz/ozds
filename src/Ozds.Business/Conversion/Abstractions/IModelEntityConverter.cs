namespace Ozds.Business.Conversion.Abstractions;

public interface IModelEntityConverter
{
  public Type EntityType { get; }

  public Type ModelType { get; }

  public bool CanConvertToEntity(Type modelType);

  public bool CanConvertToModel(Type entityType);

  public object ToEntity(object model);

  public object ToModel(object entity);
}
