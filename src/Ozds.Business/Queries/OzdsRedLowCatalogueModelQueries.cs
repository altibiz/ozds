using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsRedLowCatalogueModelQueries : IOzdsQueries
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

    return null;
  }
}
