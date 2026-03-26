using System.Globalization;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Mutations;
using Ozds.Data.Queries;
using Ozds.Data.Test.Base;

namespace Ozds.Data.Test.Queries.ReportQueriesTest;

// TODO: find out how I can bypass this warning
[Skip("ManyServiceProvidersCreatedWarning - too many parallel test containers")]
public class ReadLoadCurveReportBasisTest : OzdsDataTestBase
{
  private const string Oct1 = "2023-10-01T00:00:00+00:00";
  private const string Nov1 = "2023-11-01T00:00:00+00:00";
  private const string Dec1 = "2023-12-01T00:00:00+00:00";

  public static IEnumerable<LoadCurveScenario> Scenarios()
  {
    return new[]
    {
      new LoadCurveScenario(
        "1. Normal - data in queried window returns all aggregates in order",
        new Dictionary<string, int> { { Nov1, 10 } },
        Nov1,
        Dec1,
        true,
        10
      ),
      new LoadCurveScenario(
        "2. No data - returns null",
        new Dictionary<string, int>(),
        Nov1,
        Dec1,
        false,
        0
      ),
      new LoadCurveScenario(
        "3. Data only in adjacent months - Dec1 boundary included in queried window",
        new Dictionary<string, int> { { Oct1, 5 }, { Dec1, 5 } },
        Nov1,
        Dec1,
        true,
        1
      ),
      new LoadCurveScenario(
        "4. Data in Oct and Nov - query Nov returns only Nov aggregates",
        new Dictionary<string, int> { { Oct1, 5 }, { Nov1, 8 } },
        Nov1,
        Dec1,
        true,
        8
      ),
      new LoadCurveScenario(
        "5. Single aggregate in window - returns that one aggregate",
        new Dictionary<string, int> { { Nov1, 1 } },
        Nov1,
        Dec1,
        true,
        1
      ),
    };
  }

  [Test]
  [MethodDataSource(nameof(Scenarios))]
  public async Task Returns_Correct_Load_Curve_Aggregates(
    LoadCurveScenario scenario,
    CancellationToken cancellationToken
  )
  {
    var fromDate = ParseDate(scenario.QueryFrom);
    var toDate = ParseDate(scenario.QueryTo);

    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(AbbB2xMeterEntity))
    );

    var allMeasurements = new List<IMeasurementEntity>();

    foreach (var (dateStr, count) in scenario.MeasurementsMap)
    {
      var monthStart = ParseDate(dateStr);

      var monthData = await Measurements.Create(
        infrastructure,
        cancellationToken,
        x =>
          x.WithInterval(IntervalEntity.QuarterHour)
            .WithFromDate(monthStart)
            .WithToDate(monthStart.AddMonths(1))
            .WithCount(count)
      );

      allMeasurements.AddRange(monthData);
    }

    await ServiceProvider
      .GetRequiredService<MeasurementMutations>()
      .Create(allMeasurements, cancellationToken);

    var result = await ServiceProvider
      .GetRequiredService<ReportQueries>()
      .ReadLoadCurveReportBasis(
        infrastructure.MeasurementLocation.Id,
        fromDate,
        toDate,
        cancellationToken
      );

    if (!scenario.ExpectResult)
    {
      result
        .Should()
        .BeNull(
          $"Scenario '{scenario.Name}': expected null when no data in range"
        );
      return;
    }

    result
      .Should()
      .NotBeNull(
        $"Scenario '{scenario.Name}': expected a result when data exists"
      );

    result!
      .Aggregates.Should()
      .HaveCount(
        scenario.ExpectedAggregateCount,
        $"Scenario '{scenario.Name}': wrong number of load curve aggregates"
      );

    result
      .Aggregates.Should()
      .BeInAscendingOrder(
        x => x.Timestamp,
        $"Scenario '{scenario.Name}': aggregates must be ordered by timestamp"
      );

    result
      .Aggregates.Should()
      .AllSatisfy(
        a =>
        {
          a.Timestamp.Should()
            .BeOnOrAfter(
              fromDate,
              $"Scenario '{scenario.Name}': aggregate at {a.Timestamp} "
                + "is before fromDate"
            );
          a.Timestamp.Should()
            .BeOnOrBefore(
              toDate,
              $"Scenario '{scenario.Name}': aggregate at {a.Timestamp} "
                + "is after toDate"
            );
          a.Interval.Should()
            .Be(
              IntervalEntity.QuarterHour,
              $"Scenario '{scenario.Name}': load curve must use "
                + "QuarterHour interval"
            );
          a.MeasurementLocationId.Should()
            .Be(
              infrastructure.MeasurementLocation.Id,
              $"Scenario '{scenario.Name}': aggregate must belong to "
                + "the queried measurement location"
            );
        },
        $"Scenario '{scenario.Name}': all aggregates must be in-window "
          + "QuarterHour records for the correct location"
      );

    var inWindowAggregates = allMeasurements
      .OfType<AggregateEntity>()
      .Where(x => x.Interval == IntervalEntity.QuarterHour)
      .Where(x => x.Timestamp >= fromDate && x.Timestamp <= toDate)
      .OrderBy(x => x.Timestamp)
      .ToList();

    result
      .Aggregates.Select(x => x.Timestamp)
      .Should()
      .Equal(
        inWindowAggregates.Select(x => x.Timestamp),
        $"Scenario '{scenario.Name}': returned aggregates must match "
          + "exactly the in-window aggregates by timestamp"
      );
  }

  private static DateTimeOffset ParseDate(string isoString)
  {
    return new DateTimeOffset(
      DateTime.SpecifyKind(
        DateTimeOffset
          .Parse(isoString, CultureInfo.InvariantCulture)
          .UtcDateTime,
        DateTimeKind.Utc
      ),
      TimeSpan.Zero
    );
  }

  public record LoadCurveScenario(
    string Name,
    Dictionary<string, int> MeasurementsMap,
    string QueryFrom,
    string QueryTo,
    bool ExpectResult,
    int ExpectedAggregateCount
  );
}
