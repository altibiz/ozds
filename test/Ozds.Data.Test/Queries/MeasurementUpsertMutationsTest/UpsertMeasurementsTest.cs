using Ozds.Data.Mutations;
using Ozds.Data.Test.Context;

namespace Ozds.Data.Test.Queries.MeasurementUpsertMutationsTest;

public class UpsertMeasurementsTest(IServiceProvider services)
{
  [Fact]
  public async Task IsValidTest()
  {
    await Task.WhenAll(Enumerable
      .Range(1, Constants.DefaultRepeatCount)
      .Select(async i =>
      {
        await using var manager = services
          .GetRequiredService<EphemeralDataDbContextManager>();
        var context = await manager.GetContext();
        var factory = new MeasurementUpsertFactory(context);

        var expected = await factory.Create(CancellationToken.None);

        var mutations = services.GetRequiredService<MeasurementUpsertMutations>();
        await mutations.UpsertMeasurements(
          context,
          expected,
          CancellationToken.None
        );
      })
    );

    Assert.True(true);
  }
}
