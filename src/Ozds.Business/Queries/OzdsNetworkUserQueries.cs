using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsNetworkUserQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsNetworkUserQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<NetworkUserModel?>
    NetworkUserById(string id)
  {
    var networkUser =
      await context.NetworkUsers
        .WithId(id)
        .FirstOrDefaultAsync();
    if (networkUser is not null)
    {
      return networkUser.ToModel();
    }

    return null;
  }

  public async Task<PaginatedList<LocationModel>> GetLocations(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Locations
      .Where(catalogue => catalogue.Title
        .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }
}
