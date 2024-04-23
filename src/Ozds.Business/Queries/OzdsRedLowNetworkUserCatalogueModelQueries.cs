using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsRedLowNetworkUserCatalogueModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsRedLowNetworkUserCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

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
