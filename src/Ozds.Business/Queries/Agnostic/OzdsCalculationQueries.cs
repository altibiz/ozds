using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsCalculationQueries(
  OzdsDataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IOzdsQueries
{
  private readonly OzdsDataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<T?> ReadSingle<T>(string id)
    where T : class, INetworkUserCalculation
  {
    var entityType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetDbSet(entityType)
        as IQueryable<NetworkUserCalculationEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {entityType}");
    var item = await queryable
      .Where(_context.PrimaryKeyEqualsAgnostic(entityType, id))
      .FirstOrDefaultAsync();
    return item is null ? null : _modelEntityConverter.ToModel(item) as T;
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    IEnumerable<string> orderByDescClauses,
    IEnumerable<string> orderByAscClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, INetworkUserCalculation
  {
    var queryable = _context.GetDbSet(typeof(T))
        as IQueryable<NetworkUserCalculationEntity>
      ?? throw new InvalidOperationException();
    var filtered = whereClauses.Aggregate(
      queryable,
      (current, clause) => current.WhereDynamic(clause));
    var count = await filtered.CountAsync();
    var orderedByDesc = orderByDescClauses.Aggregate(
      filtered,
      (current, clause) => current.OrderByDescendingDynamic(clause));
    var orderedByAsc = orderByAscClauses.Aggregate(
      orderedByDesc,
      (current, clause) => current.OrderByDynamic(clause));
    var items = await orderedByAsc.Skip((pageNumber - 1) * pageCount)
      .Take(pageCount).ToListAsync();
    return items
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}
