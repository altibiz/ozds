using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataAuditableQueries = Ozds.Data.Queries.AuditableQueries;

namespace Ozds.Business.Queries;

public class AuditableQueries(
  DataAuditableQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<T?> ReadSingle<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IAuditable
  {
    var model = await ReadSingleDynamic(typeof(T), id, cancellationToken);
    return model is null ? default : (T)model;
  }

  public async Task<object?> ReadSingleDynamic(
    Type modelType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(IAuditable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditable)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entity = await queries.ReadSingleDynamic(entityType, id, cancellationToken);
    if (entity is null)
    {
      return default;
    }

    var model = modelEntityConverter.ToModel(entity);
    return model;
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    var models = await ReadDynamic(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount,
      deleted
    );

    return models.Items
      .OfType<T>()
      .ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> ReadDynamic(
    Type modelType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    if (!modelType.IsAssignableTo(typeof(IAuditable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditable)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadDynamic(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount,
      where: !deleted ? x => !((IAuditableEntity)x).IsDeleted : null
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel)
      .ToPaginatedList(entities.TotalCount);
  }
}
