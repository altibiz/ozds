using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsWhiteLowNetworkUserCatalogueModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsWhiteLowNetworkUserCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<WhiteLowNetworkUserCatalogueModel?>
    BlueLowNetworkUserCatalogueById(string id)
  {
    var catalogueModel =
      await context.NetworkUserCatalogues
        .OfType<WhiteLowNetworkUserCatalogueEntity>()
        .Where(context.PrimaryKeyEquals<WhiteLowNetworkUserCatalogueEntity>(id))
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
