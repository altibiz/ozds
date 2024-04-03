using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries.Agnotic;

public class OzdsAuditableQueries : IOzdsQueries
{
  private readonly OzdsDbContext _context;

  private readonly IServiceProvider _serviceProvider;

  public OzdsAuditableQueries(OzdsDbContext context,
    IServiceProvider serviceProvider)
  {
    _context = context;
    _serviceProvider = serviceProvider;
  }

  public async Task<T?> ReadSingle<T>(string id) where T : class, IAuditable
  {
    var modelEntityConverter = _serviceProvider
      .GetServices<IModelEntityConverter>()
      .FirstOrDefault(converter => converter
        .CanConvertToModel(typeof(T))) ?? throw new InvalidOperationException(
      $"No model entity converter found for {typeof(T)}");
    var queryable = _context.GetDbSet(typeof(T))
                      as IQueryable<AuditableEntity>
                    ?? throw new InvalidOperationException();
    var item = await queryable.WithId(id).FirstOrDefaultAsync();
    return item is null ? null : modelEntityConverter.ToModel(item) as T;
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    IEnumerable<string> orderByDescClauses,
    IEnumerable<string> orderByAscClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IAuditable
  {
    var modelEntityConverter = _serviceProvider
      .GetServices<IModelEntityConverter>()
      .FirstOrDefault(converter => converter
        .CanConvertToModel(typeof(T))) ?? throw new InvalidOperationException(
      $"No model entity converter found for {typeof(T)}");
    var queryable = _context.GetDbSet(typeof(T))
                      as IQueryable<AuditableEntity>
                    ?? throw new InvalidOperationException();
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var count = await filtered.CountAsync();
    var orderedByDesc = orderByDescClauses.Aggregate(filtered,
      (current, clause) => current.OrderByDescendingDynamic(clause));
    var orderedByAsc = orderByAscClauses.Aggregate(orderedByDesc,
      (current, clause) => current.OrderByDynamic(clause));
    var items = await orderedByAsc.Skip((pageNumber - 1) * pageCount)
      .Take(pageCount).ToListAsync();
    return items
      .Select(item => modelEntityConverter.ToModel(item))
      .OfType<T>()
      .ToPaginatedList(count);
  }
}
