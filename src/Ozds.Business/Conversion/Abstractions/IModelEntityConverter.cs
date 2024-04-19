namespace Ozds.Business.Conversion.Abstractions;

public interface IModelEntityConverter
{
  bool CanConvertToEntity(Type modelType);

  bool CanConvertToModel(Type entityType);

  object ToEntity(object model);

  object ToModel(object entity);

  Type EntityType();
}
