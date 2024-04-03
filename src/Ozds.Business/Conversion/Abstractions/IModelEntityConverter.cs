namespace Ozds.Business.Conversion.Abstractions;

public interface IModelEntityConverter
{
  public Type ModelType { get; }

  public Type EntityType { get; }

  object ToEntity(object model);

  object ToModel(object entity);
}
