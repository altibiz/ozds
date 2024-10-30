namespace Ozds.Data.Test;

public class NoiseFixtureService(
  DataDbContextFixtureFactory contextFixture
)
{
  public async Task<List<T>> CreateMany<T>()
  {
    var fixture = new Fixture();
    var context = await contextFixture.Context.Value;
    var entities = fixture
      .CreateMany<T>(Constants.DefaultFuzzCount)
      .ToList();
    context.AddRange(entities);
    await context.SaveChangesAsync();
    return entities;
  }

  public async Task<List<T>> Create<T>()
  {
    var fixture = new Fixture();
    var context = await contextFixture.Context.Value;
    var entities = fixture
      .CreateMany<T>(Constants.DefaultFuzzCount)
      .ToList();
    context.AddRange(entities);
    await context.SaveChangesAsync();
    return entities.ToList();
  }
}
