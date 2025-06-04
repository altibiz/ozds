using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;

namespace Ozds.Fake.Client;

public class InsertClient(
  MeasurementMutations mutations,
  MeasurementLocationQueries measurementLocationQueries,
  ClockQueries clock
)
{
  public async Task<List<string>> GetMetersForLocation(
    string? locationId,
    CancellationToken cancellationToken
  )
  {
    var now = clock.Timestamp();
    var analysisBases = await measurementLocationQueries
      .ReadAnalysisBasisByLocationAndRepresentative(
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

  public async Task Insert(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    await mutations.CreateMeasurements(
      measurements,
      cancellationToken,
      false
    );
  }

  public async Task Insert(
    IAsyncEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    await mutations.CreateMeasurements(
      measurements,
      cancellationToken,
      false
    );
  }
}
