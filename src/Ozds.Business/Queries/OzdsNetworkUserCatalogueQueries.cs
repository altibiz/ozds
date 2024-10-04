using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries;

public class OzdsNetworkUserCatalogueQueries(DataDbContext context)
  : IQueries
{
  private readonly DataDbContext context = context;

  public async Task<NetworkUserCatalogueModel?>
    NetworkUserCatalogueById(string id)
  {
    var catalogueModel =
      await context.NetworkUserCatalogues
        .Where(context.PrimaryKeyEquals<NetworkUserCatalogueEntity>(id))
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
