using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsAuditableQueries(
  OzdsDataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IOzdsQueries
{
  private readonly OzdsDataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<T?> ReadSingle<T>(string id)
    where T : class, IAuditable
  {
    var entityType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetDbSet(entityType);
    var item = await queryable
      .Where(_context.PrimaryKeyEqualsAgnostic(entityType, id))
      .FirstOrDefaultAsync();
    return item is null ? null : _modelEntityConverter.ToModel(item) as T;
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<Expression<Func<AuditableEntity, bool>>> whereClauses,
    IEnumerable<Expression<Func<AuditableEntity, object>>> orderByDescClauses,
    IEnumerable<Expression<Func<AuditableEntity, object>>> orderByAscClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IAuditable
  {
    var queryable =
      _context.GetDbSet(_modelEntityConverter.EntityType(typeof(T)))
        as IQueryable<AuditableEntity>
      ?? throw new InvalidOperationException();

    var filtered = whereClauses.Aggregate(
      queryable,
      (current, clause) => current.Where(clause));

    var count = await filtered.CountAsync();

    var orderedByDesc = orderByDescClauses.Aggregate(
      filtered,
      (current, clause) => current.OrderByDescending(clause));

    var orderedByAsc = orderByAscClauses.Aggregate(
      orderedByDesc,
      (current, clause) => current.OrderBy(clause));

    var items = await orderedByAsc.Skip((pageNumber - 1) * pageCount)
      .Take(pageCount).ToListAsync();

    return items
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}
