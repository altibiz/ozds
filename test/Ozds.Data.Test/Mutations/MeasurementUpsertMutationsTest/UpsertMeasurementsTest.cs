using System.Diagnostics;
using Ozds.Data.Mutations;
using Ozds.Data.Test.Context;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Mutations.MeasurementUpsertMutationsTest;

// TODO: check upsert logic

public class UpsertMeasurementsTest(IServiceProvider services)
{
  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  public async Task FinishesInTimeTest(int _)
  {
    await using var manager = services
      .GetRequiredService<EphemeralDataDbContextManager>();
    var context = await manager.GetContext();
    var factory = new MeasurementUpsertFactory(context);

    var expected = await factory.CreateMany(CancellationToken.None);

    var mutations = services.GetRequiredService<MeasurementUpsertMutations>();
    var stopwatch = Stopwatch.StartNew();
    await mutations.UpsertMeasurements(
      context,
      expected,
      CancellationToken.None
    );
    stopwatch.Stop();
    stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(10));
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  public async Task IsValidTest(int _)
  {
    await using var manager = services
      .GetRequiredService<EphemeralDataDbContextManager>();
    var context = await manager.GetContext();
    var factory = new MeasurementUpsertFactory(context);

    var expected = await factory.CreateDerivedNull(CancellationToken.None);

    var mutations = services.GetRequiredService<MeasurementUpsertMutations>();
    var results = await mutations.UpsertMeasurements(
      context,
      expected,
      CancellationToken.None
    );

    results.Should().BeContextuallyEquivalentTo(context, expected);
  }
}
