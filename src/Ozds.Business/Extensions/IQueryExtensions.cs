using System.Linq.Expressions;
using Ozds.Business.Queries.Abstractions;
using YesSql;
using YesSql.Indexes;

namespace Ozds.Business.Extensions;

public static class IQueryExtensions
{
  public static async Task<PaginatedList<TModel>> QueryPaged<TIndex, TEntity,
    TModel>(
    this IQuery<TEntity, TIndex> queryable,
    Func<TEntity, TModel> toModel,
    Expression<Func<TIndex, bool>>? filter,
    int pageNumber = QueryConstants.StartingPage,
    int pageSize = QueryConstants.DefaultPageCount
  )
    where TIndex : class, IIndex
    where TEntity : class
  {
    var query = queryable.Where(filter ?? (_ => true));
    var count = await query.CountAsync();
    var items = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize)
      .ListAsync();
    return items.Select(toModel).ToPaginatedList(count);
  }
}
