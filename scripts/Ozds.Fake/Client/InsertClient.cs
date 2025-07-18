using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;

namespace Ozds.Fake.Client;

public class InsertClient(
  MeasurementMutations mutations,
  AnalysisQueries analysisQueries,
  ClockQueries clock
)
{
  public async Task<List<string>> GetMetersForLocation(
    string? locationId,
    CancellationToken cancellationToken
  )
  {
    var now = clock.Timestamp();
    var analysisBases = await analysisQueries
      .ReadByLocationIdAndRepresentative(
        locationId,
        null,
        now,
        now,
        cancellationToken
      );
    return analysisBases
      .Select(x => $"{x.MeasurementLocation.Id}:{x.Meter.Id}")
      .ToList();
  }

  public async Task<List<IMeasurement>> Insert(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    return await mutations.Create(
      measurements,
      cancellationToken,
      false
    );
  }

  public async Task<List<IMeasurement>> Insert(
    IAsyncEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    return await mutations.Create(
      measurements,
      cancellationToken,
      false
    );
  }
}
