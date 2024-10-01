using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsSchneideriEM3xxxMeasurementValidatorModelQueries(
  DataDbContext context) : IQueries
{
  private readonly DataDbContext context = context;

  public async Task<SchneideriEM3xxxMeasurementValidatorModel?>
    SchneideriEM3xxxMeasurementValidatorById(string id)
  {
    var validatorModel =
      await context.MeasurementValidators
        .OfType<SchneideriEM3xxxMeasurementValidatorEntity>()
        .Where(
          context
            .PrimaryKeyEquals<SchneideriEM3xxxMeasurementValidatorEntity>(id))
        .FirstOrDefaultAsync();
    if (validatorModel is not null)
    {
      return validatorModel.ToModel();
    }

    return null;
  }
}
