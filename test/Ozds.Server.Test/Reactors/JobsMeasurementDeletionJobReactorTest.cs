using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Fake.Identification;
using Ozds.Server.Test.Base;
using DataEntityReflector = Ozds.Data.Reflection.EntityReflector;
using DataTimescaleChunkIntervalQueries =
  Ozds.Data.Queries.TimescaleChunkIntervalQueries;

namespace Ozds.Server.Test.Reactors;

public class JobsMeasurementDeletionJobReactorTest : OzdsServerTestBase
{
  public JobsMeasurementDeletionJobReactorTest()
  {
    Interval = TimeSpan.FromHours(1);

    Configure(
      x =>
      {
        x.Ozds.ConfigureHost(
          builder =>
          {
            builder.Configuration.AddInMemoryCollection(
              new Dictionary<string, string?>
              {
                ["Ozds:Jobs:Archival:DailyMeasurementDeletionCron"] =
                  "0 * * * * ?", // NOTE: on the first second of every minute
                ["Ozds:Business:Reactor:MeasurementDeletionJobIntervalSeconds"] =
                  Interval.TotalSeconds.ToString()
              });
          });
      });
  }

  private TimeSpan Interval { get; }

  // TODO: make it work with delete_chunks
  [Test]
  public async Task MeasurementDeletionJobReactor_Reacts(
    CancellationToken cancellationToken
  )
  {
    var x = await MeasurementLocation.Create(cancellationToken);

    var dateTo = Services.GetRequiredService<ClockQueries>().Now();
    var dateFrom = dateTo - Interval * 2;
    var deletionCutoff = dateFrom + Interval;

    var reflector = Services.GetRequiredService<DataEntityReflector>();

    var itemsBefore = await Measurement
      .Insert(
        [
          new MeasurementLocationMeterIdWithValidator(
            x.MeasurementLocation.Id,
            x.Meter.Id,
            x.MeasurementValidator)
        ],
        dateFrom,
        dateTo,
        cancellationToken,
        false)
      .Where(x => x is not IAggregate)
      .ToListAsync(cancellationToken);

    var lastChunkByModelType = (await Services
        .GetRequiredService<DataTimescaleChunkIntervalQueries>()
        .GetLatestChunkIntervalBeforeCutoff(
          deletionCutoff,
          reflector.MeasurementTypes,
          cancellationToken)
      ).ToDictionary(
        x =>
          Services.GetRequiredService<ModelEntityConverter>()
            .ModelType(
              reflector.ResolveEntityTypeFromTable(x.HypertableName)
            )
      );

    itemsBefore.Should().AllSatisfy(
      x =>
        x.Timestamp.Should().BeAfter(dateFrom));
    itemsBefore.Should().AllSatisfy(
      x =>
        x.Timestamp.Should().BeBefore(dateTo));

    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

    var itemsAfter = await Services.GetRequiredService<MeasurementQueries>()
      .ReadByMeasurementLocationIds(
        [x.MeasurementLocation.Id],
        null,
        dateFrom,
        dateTo,
        0,
        cancellationToken
      );

    var determinedItemsAfter = itemsBefore.Where(
      x =>
        !lastChunkByModelType.TryGetValue(x.GetType(), out var chunkInfo)
        || x.Timestamp > chunkInfo.RangeEnd
    ).ToList();

    var determinedTimestampMinimum = itemsBefore.Min(x => x.Timestamp);

    itemsAfter.TotalCount.Should()
      .BeLessThanOrEqualTo(determinedItemsAfter.Count);

    itemsAfter.Items.Should().AllSatisfy(
      x =>
        x.Timestamp.Should().BeOnOrAfter(determinedTimestampMinimum));
  }
}
