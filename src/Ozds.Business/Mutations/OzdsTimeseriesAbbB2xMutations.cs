using Ozds.Business.Models;

namespace Ozds.Business.Mutations;

public partial class OzdsTimeseriesMutations
{
  public async Task CreateAbbB2xMeasurement(
    AbbB2xMeasurementModel model
  )
  {
    await _context.AbbB2xAggregates.Upsert(model, cancellationToken);
  }
}