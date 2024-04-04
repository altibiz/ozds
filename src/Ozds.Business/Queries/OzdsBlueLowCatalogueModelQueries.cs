using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsBlueLowCatalogueModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsBlueLowCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<BlueLowCatalogueModel?>
    BlueLowCatalogueById(string id)
  {
    var catalogueModel =
      await context.Catalogues
        .OfType<BlueLowCatalogueEntity>()
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
