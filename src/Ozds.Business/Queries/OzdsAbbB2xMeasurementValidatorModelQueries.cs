using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsAbbB2xMeasurementValidatorModelQueries(
  DataDbContext context)
  : IQueries
{
  private readonly DataDbContext context = context;

  public async Task<AbbB2xMeasurementValidatorModel?>
    AbbB2xMeasurementValidatorById(string id)
  {
    var validatorModel =
      await context.MeasurementValidators
        .OfType<AbbB2xMeasurementValidatorEntity>()
        .Where(context.PrimaryKeyEquals<AbbB2xMeasurementValidatorEntity>(id))
        .FirstOrDefaultAsync();
    if (validatorModel is not null)
    {
      return validatorModel.ToModel();
    }

    return null;
  }
}
