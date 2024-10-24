using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Queries.Abstractions;
using DataAuditableQueries = Ozds.Data.Queries.Agnostic.AuditableQueries;

namespace Ozds.Business.Queries.Agnostic;

// TODO: figure out clauses

public class AuditableQueries(
  DataAuditableQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<T?> ReadSingle<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IAuditableEntity
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

    var model = modelEntityConverter.ToEntity(entity);
    return model;
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var models = await ReadDynamic(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount
    );

    return models.Items
      .OfType<T>()
      .ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> ReadDynamic(
    Type modelType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!modelType.IsAssignableTo(typeof(IAuditable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditable)}");
    }

    var entities = await queries.ReadDynamic(
      modelType,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToEntity)
      .ToPaginatedList(entities.TotalCount);
  }
}