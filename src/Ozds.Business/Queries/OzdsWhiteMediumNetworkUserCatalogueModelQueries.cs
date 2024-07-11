using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsWhiteMediumNetworkUserCatalogueModelQueries(
  OzdsDataDbContext context) : IOzdsQueries
{
  private readonly OzdsDataDbContext context = context;

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
