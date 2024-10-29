using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Queries;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(
  DataDbContextFixture fixture,
  AnalysisQueries queries
)
{
  public static readonly TheoryData<AnalysisBasisEntity> TestData =
    new(
      new Fixture()
        .CreateMany<AnalysisBasisEntity>()
    );

  [Theory]
  [MemberAutoData("id")]
  public async Task IsValidForOperator(string id)
  {
    var context = await fixture.Context.Value;

    var result = await queries.ReadAnalysisBasesByRepresentative(
      context,
      id,
      RoleEntity.OperatorRepresentative,
      DateTimeOffset.MinValue,
      DateTimeOffset.MaxValue,
      CancellationToken.None
    );

    Assert.NotNull(result);
  }

  [Theory]
  [InlineData("id")]
  public async Task IsValidForLocation(string id)
  {
    var context = await fixture.Context.Value;

    var result = await queries.ReadAnalysisBasesByRepresentative(
      context,
      id,
      RoleEntity.LocationRepresentative,
      DateTimeOffset.MinValue,
      DateTimeOffset.MaxValue,
      CancellationToken.None
    );

    Assert.NotNull(result);
  }

  [Theory]
  [InlineData("id")]
  public async Task IsValidForNetworkUser(string id)
  {
    var context = await fixture.Context.Value;

    var result = await queries.ReadAnalysisBasesByRepresentative(
      context,
      id,
      RoleEntity.NetworkUserRepresentative,
      DateTimeOffset.MinValue,
      DateTimeOffset.MaxValue,
      CancellationToken.None
    );

    Assert.NotNull(result);
  }
}
