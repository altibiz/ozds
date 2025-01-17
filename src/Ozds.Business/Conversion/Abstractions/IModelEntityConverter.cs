namespace Ozds.Business.Conversion.Abstractions;

public interface IModelEntityConverter
{
  Type EntityType { get; }

  Type ModelType { get; }

  bool CanConvertToEntity(Type modelType);

  bool CanConvertToModel(Type entityType);

  object ToEntity(object model);

  object ToModel(object entity);
}
