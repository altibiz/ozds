using System.Diagnostics;
using Ozds.Business.Conversion;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Queries;
using Ozds.Data.Test.Context;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

[Collection(nameof(EphemeralDataDbContextManager))]
public class ReadAnalysisBasesByRepresentativeTest(
  EphemeralDataDbContextManager manager,
  AnalysisQueries queries,
  ILogger<ReadAnalysisBasesByRepresentativeTest> logger,
  ModelEntityConverter modelEntityConverter
)
{
  [Theory]
  [InlineData("1")]
  public async Task IsValidTest(string representativeId)
  {
    var context = await manager.GetContext(CancellationToken.None);
    var factory = new AnalysisBasisEntityFactory(context);

    var dateTo = DateTime.UtcNow;
    var dateFrom = dateTo.AddMonths(-1);

    var noise = await factory.Create(
      representativeId,
      dateFrom,
      dateTo,
      CancellationToken.None
    );
    var expectedResults = noise.Take(1).ToList();

    var representative = expectedResults.First().Representative;
    var location = expectedResults.First().Location;
    var fromDate = expectedResults.First().FromDate;
    var toDate = expectedResults.First().ToDate;

    var stopwatch = Stopwatch.StartNew();
    var actualResults = await queries.ReadAnalysisBasesByRepresentativeAndLocation(
      context,
      representative.Id,
      representative.Role,
      fromDate,
      toDate,
      location.Id,
      CancellationToken.None
    );
    stopwatch.Stop();
    logger.LogInformation("Read in {Elapsed}", stopwatch.Elapsed);
    stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(1));

    expectedResults = expectedResults
      .Select(x => new AnalysisBasisEntity
      {
        Calculations = x.Calculations.OrderBy(y => y.Id).ToList(),
        Invoices = x.Invoices.OrderBy(y => y.Id).ToList()
      })
      .OrderBy(x => x.MeasurementLocation.Id)
      .ToList();
    actualResults = actualResults
      .Select(x => new AnalysisBasisEntity
      {
        Calculations = x.Calculations.OrderBy(y => y.Id).ToList(),
        Invoices = x.Invoices.OrderBy(y => y.Id).ToList()
      })
      .OrderBy(x => x.MeasurementLocation.Id)
      .ToList();

    actualResults.Should().HaveCount(expectedResults.Count);

    actualResults.Should().AllSatisfy(actual =>
    {
      var expected = expectedResults.FirstOrDefault(expected =>
        expected.MeasurementLocation.Id == actual.MeasurementLocation.Id)!;

      expected.Should().NotBeNull();

      actual.Representative
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Representative);

      actual.FromDate.Should().BeExactly(expected.FromDate);
      actual.ToDate.Should().BeExactly(expected.ToDate);

      actual.Location
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Location);

      actual.NetworkUser
        .Should()
        .BeContextuallyEquivalentTo(context, expected.NetworkUser);

      actual.MeasurementLocation
        .Should()
        .BeContextuallyEquivalentTo(context, expected.MeasurementLocation);

      actual.Meter
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Meter);

      actual.Calculations
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Calculations);

      actual.Invoices
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Invoices);

      actual.LastMeasurement
        .Should()
        .BeContextuallyEquivalentTo(context, expected.LastMeasurement);

      actual.MonthlyAggregates
        .Should()
        .BeContextuallyEquivalentTo(context, expected.MonthlyAggregates);

      var actualModel = modelEntityConverter.ToModel(actual);
      var expectedModel = modelEntityConverter.ToModel(expected);

      actualModel.Should().BeBusinesswiseEquivalentTo(expectedModel);
    });
  }
}
