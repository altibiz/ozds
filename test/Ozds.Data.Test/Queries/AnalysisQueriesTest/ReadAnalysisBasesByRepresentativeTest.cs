using Ozds.Data.Queries;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(
  DataDbContextFixtureFactory contextFixture,
  AnalysisBasisEntityFactory analysisBasisFixture,
  AnalysisQueries queries
)
{
  public record TestRecord(
    string RepresentativeId,
    DateTimeOffset DateFrom,
    DateTimeOffset DateTo
  );

  public static readonly TheoryData<TestRecord> TestData =
    new(
      Enumerable.Range(1, Constants.DefaultFuzzCount)
        .Select(i => new TestRecord(
          i.ToString(),
          DateTimeOffset.UtcNow.AddMonths(-i),
          DateTimeOffset.UtcNow
        ))
    );

  [Theory]
  [MemberData(nameof(TestData))]
  public async Task IsValid(TestRecord @record)
  {
    var analysisBases = await analysisBasisFixture.Create(
      @record.RepresentativeId,
      @record.DateFrom,
      @record.DateTo
    );

    var representative = analysisBases.First().Representative;
    var fromDate = analysisBases.First().FromDate;
    var toDate = analysisBases.First().ToDate;

    var context = await contextFixture.Context.Value;
    var result = await queries.ReadAnalysisBasesByRepresentative(
      context,
      representative.Id,
      representative.Role,
      fromDate,
      toDate,
      CancellationToken.None
    );

    Assert.Equal(analysisBases, result);
  }
}
