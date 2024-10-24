using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsMessengerModelQueries(DataDbContext context) : IQueries
{
  private readonly DataDbContext context = context;

  public async Task<PidgeonMessengerModel?>
    MessengerById(string id)
  {
    var messengerEntity =
      await context.Messengers
        .OfType<PidgeonMessengerEntity>()
        .Where(context.PrimaryKeyEquals<PidgeonMessengerEntity>(id))
        .FirstOrDefaultAsync();
    if (messengerEntity is not null)
    {
      return messengerEntity.ToModel();
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
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<LocationEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }
}
