using Ozds.Data.Test.Containers;

namespace Ozds.Data.Test.Base;

public class OzdsDataTestBase
{
  private PostgresContainer? postgresContainer;

  private OzdsData? ozdsData;

  public IServiceProvider ServiceProvider =>
    ozdsData?.ServiceProvider
    ?? throw new InvalidOperationException("Test not initialized");

  public TestInfrastructureFixture Infrastructure =>
    ServiceProvider.GetRequiredService<TestInfrastructureFixture>();

  public TestMeasurementFixture Measurements =>
    ServiceProvider.GetRequiredService<TestMeasurementFixture>();

  [Before(HookType.Test)]
  public async Task SetUp(CancellationToken cancellationToken)
  {
    postgresContainer = await PostgresContainer.Create(cancellationToken);
    ozdsData = await OzdsData.Create(postgresContainer, cancellationToken);
  }

  [After(HookType.Test)]
  public async Task TearDown(CancellationToken _)
  {
    if (ozdsData is not null)
    {
      await ozdsData.DisposeAsync();
      ozdsData = null;
    }

    if (postgresContainer is not null)
    {
      await postgresContainer.DisposeAsync();
      postgresContainer = null;
    }
  }
}
