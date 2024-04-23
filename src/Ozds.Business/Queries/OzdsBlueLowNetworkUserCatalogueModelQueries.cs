using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsBlueLowNetworkUserCatalogueModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsBlueLowNetworkUserCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<BlueLowNetworkUserCatalogueModel?>
    BlueLowNetworkUserCatalogueById(string id)
  {
    var catalogueModel =
      await context.NetworkUserCatalogues
        .OfType<BlueLowNetworkUserCatalogueEntity>()
        .Where(context.PrimaryKeyEquals<BlueLowNetworkUserCatalogueEntity>(id))
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
