using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataEntityQueries = Ozds.Data.Queries.EntityQueries;

namespace Ozds.Business.Queries;

public class ModelQueries(
  DataEntityQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<T?> ReadById<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IModel
  {
    var model = await ReadById(typeof(T), id, cancellationToken);
    return model is null ? default : (T)model;
  }

  public async Task<object?> ReadById(
    Type modelType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(IModel)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IModel)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entity = await queries.ReadById(
      entityType, id, cancellationToken);
    if (entity is null)
    {
      return default;
    }

    var model = modelEntityConverter.ToModel(entity);
    return model;
  }

  public async Task<List<T>> ReadByIds<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByIds(typeof(T), ids, cancellationToken);
    return models.OfType<T>().ToList();
  }

  public async Task<List<object>> ReadByIds(
    Type modelType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(IModel)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IModel)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadByIds(
      entityType,
      ids,
      cancellationToken
    );

    return entities.OfType<object>().ToList();
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var models = await Read(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount
    );

    return models.Items
      .OfType<T>()
      .ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type modelType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!modelType.IsAssignableTo(typeof(IModel)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IModel)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.Read(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel)
      .ToPaginatedList(entities.TotalCount);
  }
}
