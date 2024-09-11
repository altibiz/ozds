using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Extensions;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsRegulatoryCatalogueModelQueries(DataDbContext context)
  : IOzdsQueries
{
  private readonly DataDbContext context = context;

  public async Task<RegulatoryCatalogueModel?>
    RegulatoryCatalogueById(string id)
  {
    var catalogueModel =
      await context.RegulatoryCatalogues
        .Where(context.PrimaryKeyEquals<RegulatoryCatalogueEntity>(id))
        .FirstOrDefaultAsync();
    if (catalogueModel is not null)
    {
      return catalogueModel.ToModel();
    }

    return null;
  }
}
