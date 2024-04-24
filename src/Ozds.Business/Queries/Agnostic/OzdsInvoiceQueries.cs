using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsInvoiceQueries : IOzdsQueries
{
  private readonly OzdsDbContext _context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter;

  public OzdsInvoiceQueries(
    OzdsDbContext context,
    AgnosticModelEntityConverter modelEntityConverter
  )
  {
    _context = context;
    _modelEntityConverter = modelEntityConverter;
  }


  public async Task<T?> ReadSingle<T>(string id) where T : class, IInvoice
  {
    var entityType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetDbSet(entityType);
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
  ) where T : class, IInvoice
  {
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetDbSet(dbSetType)
                      as IQueryable<InvoiceEntity>
                    ?? throw new InvalidOperationException(
                      $"No DbSet found for {dbSetType}");
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
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}
