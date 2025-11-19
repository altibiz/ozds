using System.Text.Json;
using Ozds.Caching.Policies.Abstractions;

namespace Ozds.Caching.Configuration;

public class CacheEntryConfiguration
{
  public TimeSpan? HardTtl { get; set; }

  public TimeSpan? SoftTtl { get; set; }
}

public class CacheConfiguration
{
  public required List<IPolicy> Policies { get; set; }

  public required JsonSerializerOptions JsonSerializerOptions { get; set; }

  public required CacheEntryConfiguration Entry { get; set; }

  public static CacheConfiguration Default
  {
    get
    {
      return new CacheConfiguration
      {
        Policies = new List<IPolicy>(),
        JsonSerializerOptions = new JsonSerializerOptions(),
        Entry = new CacheEntryConfiguration()
      };
    }
  }
}
