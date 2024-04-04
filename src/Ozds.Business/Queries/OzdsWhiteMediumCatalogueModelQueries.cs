using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsWhiteMediumCatalogueModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsWhiteMediumCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<WhiteMediumCatalogueModel?>
    BlueLowCatalogueById(string id)
  {
    var catalogueModel =
      await context.Catalogues
        .OfType<WhiteMediumCatalogueEntity>()
        .WithId(id)
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
