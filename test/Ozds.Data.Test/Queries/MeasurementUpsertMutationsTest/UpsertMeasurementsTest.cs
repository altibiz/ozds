using System.Diagnostics;
using Ozds.Data.Mutations;
using Ozds.Data.Test.Context;

namespace Ozds.Data.Test.Queries.MeasurementUpsertMutationsTest;

// TODO: check upsert logic

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
        var stopwatch = Stopwatch.StartNew();
        await mutations.UpsertMeasurements(
          context,
          expected,
          CancellationToken.None
        );
        stopwatch.Stop();
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(10));
      })
    );

    Assert.True(true);
  }
}
