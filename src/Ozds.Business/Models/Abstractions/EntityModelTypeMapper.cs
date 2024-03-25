using Microsoft.EntityFrameworkCore;
using Ozds.Data;

namespace Ozds.Business.Models.Abstractions;

public static class EntityModelTypeMapper
{
  public static IQueryable<object> GetDbSet(DbContext context, Type modelType)
  {
    var entityType = GetEntityType(modelType);
    var setMethod = typeof(DbContext).GetMethod("Set") ?? throw new InvalidOperationException("Set method not found in DbContext");
    var dbSet = setMethod.MakeGenericMethod(entityType).Invoke(context, null) as IQueryable<object>;
    return dbSet ?? throw new InvalidOperationException($"DbSet not found for entity type {entityType.FullName}");
  }

  public static object ToEntity(object model)
  {
    var modelType = model.GetType();
    var modelConversionExtensions = GetModelConversionExtensionsType(modelType);
    var toEntityMethod = modelConversionExtensions.GetMethod("ToEntity") ?? throw new InvalidOperationException($"ToEntity method not found in model conversion extensions type {modelConversionExtensions.FullName}");
    var entity = toEntityMethod.Invoke(null, new[] { model });
    return entity ?? throw new InvalidOperationException($"ToEntity method returned null for model {modelType.FullName}");
  }

  public static T ToModel<T>(object entity) where T : class
  {
    var modelType = GetModelType(entity.GetType());
    var modelConversionExtensions = GetModelConversionExtensionsType(modelType);
    var toModelMethod = modelConversionExtensions.GetMethod("ToModel") ?? throw new InvalidOperationException($"ToModel method not found in model conversion extensions type {modelConversionExtensions.FullName}");
    var model = toModelMethod.Invoke(null, new[] { entity });
    return model as T ?? throw new InvalidOperationException($"ToModel method returned null for entity {entity.GetType().FullName}");
  }

  private static Type GetEntityType(Type modelType)
  {
    var entityTypeName = modelType.Name.Replace("Model", "Entity");
    var entityNamespace = typeof(OzdsDbContext).Namespace + ".Entities";
    var entityFullName = $"{entityNamespace}.{entityTypeName}";
    var entityType = typeof(OzdsDbContext).Assembly.GetType(entityFullName);
    return entityType ?? throw new InvalidOperationException($"Entity type not found for model type {modelType.FullName}");
  }

  private static Type GetModelType(Type entityType)
  {
    var modelTypeName = entityType.Name.Replace("Entity", "Model");
    var modelNamespace = typeof(EntityModelTypeMapper).Namespace?.Replace(".Abstractions", string.Empty);
    var modelFullName = $"{modelNamespace}.{modelTypeName}";
    var modelType = typeof(EntityModelTypeMapper).Assembly.GetType(modelFullName);
    return modelType ?? throw new InvalidOperationException($"Model type not found for entity type {entityType.FullName}");
  }

  private static Type GetModelConversionExtensionsType(Type modelType)
  {
    var modelTypeName = modelType.Name + "Extensions";
    var modelNamespace = typeof(EntityModelTypeMapper).Namespace?.Replace(".Abstractions", string.Empty);
    var modelFullName = $"{modelNamespace}.{modelTypeName}";
    var modelConversionExtensions = typeof(EntityModelTypeMapper).Assembly.GetType(modelFullName);
    return modelConversionExtensions ?? throw new InvalidOperationException($"Model conversion extensions type not found for model type {modelType.FullName}");
  }
}
