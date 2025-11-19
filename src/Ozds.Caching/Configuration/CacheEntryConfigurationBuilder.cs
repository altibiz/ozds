namespace Ozds.Caching.Configuration;

public class CacheEntryConfigurationBuilder
{
  private readonly CacheEntryConfiguration entry = new();

  public CacheEntryConfigurationBuilder WithHardTtl(
    TimeSpan? hardTtl
  )
  {
    entry.HardTtl = hardTtl;
    return this;
  }

  public CacheEntryConfigurationBuilder WithSoftTtl(
    TimeSpan? softTtl
  )
  {
    entry.SoftTtl = softTtl;
    return this;
  }

  public CacheEntryConfiguration Build()
  {
    return entry;
  }
}
