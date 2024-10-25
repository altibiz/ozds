using Ozds.Data.Entities.Enums;
using Ozds.Data.Queries;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(
  AnalysisQueries queries
)
{
  [Theory]
  [InlineData("id")]
  public async Task IsValidForOperator(string id)
  {
    var result = await queries.ReadAnalysisBasesByRepresentative(
      id,
      RoleEntity.OperatorRepresentative,
      DateTimeOffset.MinValue,
      DateTimeOffset.MaxValue,
      CancellationToken.None
    );

    Assert.NotNull(result);
  }
}
