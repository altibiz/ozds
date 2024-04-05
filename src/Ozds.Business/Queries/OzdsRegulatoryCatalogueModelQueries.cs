using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsRegulatoryCatalogueModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsRegulatoryCatalogueModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<RegulatoryCatalogueModel?>
    RegulatoryCatalogueById(string id)
  {
    var catalogueModel =
      await context.Catalogues
        .OfType<RegulatoryCatalogueEntity>()
        .WithId(id)
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
