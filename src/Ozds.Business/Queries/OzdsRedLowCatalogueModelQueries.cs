using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsRedLowCatalogueModelQueries
{
  protected readonly OzdsDbContext context;

  public OzdsRedLowCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<RedLowCatalogueModel?>
    BlueLowCatalogueById(string id)
  {
    var catalogueModel =
      await context.Catalogues
        .OfType<RedLowCatalogueEntity>()
        .WithId(id)
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }
    else
    {
      return null;
    }
  }
}
