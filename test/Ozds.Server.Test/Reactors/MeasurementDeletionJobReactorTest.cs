using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Fake.Identification;
using Ozds.Server.Test.Base;

namespace Ozds.Server.Test.Reactors;

public class MeasurementDeletionJobReactorTest : OzdsServerTestBase
{
  public MeasurementDeletionJobReactorTest()
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

  [Test]
  public async Task MeasurementDeletionJobReactor_Reacts(
    CancellationToken cancellationToken
  )
  {
    var x = await MeasurementLocation.Create(cancellationToken);

    var dateTo = Services.GetRequiredService<ClockQueries>().Now();
    var dateFrom = dateTo - Interval * 2;
    var deletionCutoff = dateFrom + Interval;

    var itemsBefore = await Measurement
      .Insert(
        [new MeasurementLocationMeterId(x.MeasurementLocation.Id, x.Meter.Id)],
        dateFrom,
        dateTo,
        cancellationToken,
        aggregatesOnly: false)
      .Where(x => x is not IAggregate)
      .ToListAsync(cancellationToken);

    itemsBefore.Should().AllSatisfy(x =>
      x.Timestamp.Should().BeAfter(dateFrom));
    itemsBefore.Should().AllSatisfy(x =>
      x.Timestamp.Should().BeBefore(dateTo));

    await Task.Delay(TimeSpan.FromMinutes(2), cancellationToken);

    var itemsAfter = await Services.GetRequiredService<MeasurementQueries>()
      .ReadByMeasurementLocationIds(
        [x.MeasurementLocation.Id],
        null,
        dateFrom,
        dateTo,
        0,
        cancellationToken
      );

    itemsAfter.TotalCount.Should().BeLessThan(itemsBefore.Count);

    itemsAfter.Items.Should().AllSatisfy(x =>
      x.Timestamp.Should().BeAfter(deletionCutoff));
  }
}
