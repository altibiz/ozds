using Ozds.Data.Entities.Composite;
using Ozds.Data.Queries;
using Ozds.Data.Test.Context;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(IServiceProvider services)
{
  [Fact]
  public async Task IsValidTest()
  {
    var results = await Task.WhenAll(Enumerable
      .Range(1, Constants.DefaultRepeatCount)
      .Select(async i =>
      {
        await using var manager = services
          .GetRequiredService<EphemeralDataDbContextManager>();
        var context = await manager.GetContext();
        var factory = new AnalysisBasisEntityFactory(context);

        var representativeId = i.ToString();
        var dateTo = DateTime.UtcNow;
        var dateFrom = dateTo.AddMonths(-i);

        var expected = await factory.Create(
          representativeId,
          dateFrom,
          dateTo
        );

        var representative = expected.First().Representative;
        var fromDate = expected.First().FromDate;
        var toDate = expected.First().ToDate;

        var queries = services.GetRequiredService<AnalysisQueries>();
        var actual = await queries.ReadAnalysisBasesByRepresentative(
          context,
          representative.Id,
          representative.Role,
          fromDate,
          toDate,
          CancellationToken.None
        );

        expected = expected.OrderBy(x => x.MeasurementLocation.Id).ToList();
        actual = actual.OrderBy(x => x.MeasurementLocation.Id).ToList();

        return new TestResult<List<AnalysisBasisEntity>>(expected, actual);
      })
    );

    await using var manager = services
      .GetRequiredService<DataDbContextManager>();
    var context = await manager.GetContext();

    results.Should().AllSatisfy(result =>
    {
      result.Actual.Should().HaveCount(result.Actual.Count);

      result.Actual.Zip(result.Expected).Should().AllSatisfy(x =>
      {
        var (actual, expected) = x;

        actual.Representative
          .Should()
          .BeContextuallyEquivalentTo(context, expected.Representative);
      });
    });
  }
}
