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
  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IModel
  {
    var models = await Read(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount
    );

    return models.Items.OfType<T>().ToPaginatedList(models.TotalCount);
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
        $"Type {modelType} is not assignable to {typeof(IModel)}"
      );
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.Read(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities
      .Items.Select(modelEntityConverter.ToModel)
      .ToPaginatedList(entities.TotalCount);
  }
}
