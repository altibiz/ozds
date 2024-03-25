using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;
using ISession = YesSql.ISession;

namespace Ozds.Business.Queries.Base;

public class OzdsAuditableQueries
{
  protected readonly OzdsDbContext context;

  protected readonly ISession session;

  protected readonly UserManager<IUser> userManager;

  public OzdsAuditableQueries(OzdsDbContext context,
    UserManager<IUser> userManager, ISession session)
  {
    this.context = context;
    this.userManager = userManager;
    this.session = session;
  }

  public async Task<T?> ReadSingle<T>(string id) where T : class, IAuditable
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var item =
      await queryable.FirstOrDefaultAsync(x =>
        (x as AuditableEntity)!.Id == id);
    return item is null ? null : EntityModelTypeMapper.ToModel<T>(item);
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    IEnumerable<string> orderByDescClauses,
    IEnumerable<string> orderByAscClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IAuditable
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var count = await filtered.CountAsync();
    var orderedByDesc = orderByDescClauses.Aggregate(filtered,
      (current, clause) => current.OrderByDescendingDynamic(clause));
    var orderedByAsc = orderByAscClauses.Aggregate(orderedByDesc,
      (current, clause) => current.OrderByDynamic(clause));
    var items = await orderedByAsc.Skip((pageNumber - 1) * pageCount)
      .Take(pageCount).ToListAsync();
    return items.Select(item => EntityModelTypeMapper.ToModel<T>(item))
      .ToPaginatedList(count);
  }
}
