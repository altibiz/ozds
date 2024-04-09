using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsSchneideriEM3xxxMeasurementValidatorModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsSchneideriEM3xxxMeasurementValidatorModelQueries(
    OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<SchneideriEM3xxxMeasurementValidatorModel?>
    SchneideriEM3xxxMeasurementValidatorById(string id)
  {
    var validatorModel =
      await context.MeasurementValidators
        .OfType<SchneideriEM3xxxMeasurementValidatorEntity>()
        .WithId(id)
        .FirstOrDefaultAsync();
    if (validatorModel is not null)
    {
      return validatorModel.ToModel();
    }

    return null;
  }
}
