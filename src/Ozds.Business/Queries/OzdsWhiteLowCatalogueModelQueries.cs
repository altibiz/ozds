using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsWhiteLowCatalogueModelQueries
{
  protected readonly OzdsDbContext context;

  public OzdsWhiteLowCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<WhiteLowCatalogueModel?>
    BlueLowCatalogueById(string id)
  {
    var catalogueModel =
      await context.Catalogues
        .OfType<WhiteLowCatalogueEntity>()
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
