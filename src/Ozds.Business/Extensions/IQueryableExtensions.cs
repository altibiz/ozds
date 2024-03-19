using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Queries;

namespace Ozds.Business.Extensions;

public static class IQueryableExtensions
{
  public static async Task<PaginatedList<TModel>> QueryPaged<TEntity, TModel>(
    this IQueryable<TEntity> queryable,
    Func<TEntity, TModel> toModel,
    Expression<Func<TEntity, bool>>? filter,
    int pageNumber = QueryConstants.StartingPage,
    int pageSize = QueryConstants.DefaultPageCount
  )
  {
    var query = queryable.Where(filter ?? (_ => true));
    var count = await query.CountAsync();
    var items = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize)
      .ToListAsync();
    return items.Select(toModel).ToPaginatedList(count);
  }
}
