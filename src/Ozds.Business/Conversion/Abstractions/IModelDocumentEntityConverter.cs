namespace Ozds.Business.Conversion.Abstractions;

public interface IModelDocumentEntityConverter
{
  public Type EntityType { get; }

  public Type ModelType { get; }

  public bool CanConvertToEntity(Type modelType);

  public object ToEntity(object model);
}
