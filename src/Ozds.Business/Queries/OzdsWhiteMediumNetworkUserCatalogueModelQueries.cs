using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsWhiteMediumNetworkUserCatalogueModelQueries(
  OzdsDbContext context) : IOzdsQueries
{
  private readonly OzdsDbContext context = context;

  public async Task<WhiteMediumNetworkUserCatalogueModel?>
    BlueLowNetworkUserCatalogueById(string id)
  {
    var catalogueModel =
      await context.NetworkUserCatalogues
        .OfType<WhiteMediumNetworkUserCatalogueEntity>()
        .Where(
          context.PrimaryKeyEquals<WhiteMediumNetworkUserCatalogueEntity>(id))
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
