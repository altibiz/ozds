using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsRedLowNetworkUserCatalogueModelQueries(
  OzdsDataDbContext context)
  : IOzdsQueries
{
  private readonly OzdsDataDbContext context = context;

  public async Task<RedLowNetworkUserCatalogueModel?>
    BlueLowNetworkUserCatalogueById(string id)
  {
    var catalogueModel =
      await context.NetworkUserCatalogues
        .OfType<RedLowNetworkUserCatalogueEntity>()
        .Where(context.PrimaryKeyEquals<RedLowNetworkUserCatalogueEntity>(id))
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
