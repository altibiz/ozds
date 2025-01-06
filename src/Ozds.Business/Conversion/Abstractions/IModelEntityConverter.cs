namespace Ozds.Business.Conversion.Abstractions;

// TODO: overhaul model conversion to hierarchically aware conversion

public interface IModelEntityConverter
{
  bool CanConvertToEntity(Type modelType);

  bool CanConvertToModel(Type entityType);

  object ToEntity(object model);

  object ToModel(object entity);

  Type EntityType();
}
