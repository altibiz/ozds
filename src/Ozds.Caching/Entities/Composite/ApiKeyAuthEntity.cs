using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Composite;

public class ApiKeyAuthEntity : ICompositeEntity
{
  public string Id
  {
    get => ApiKey?.Id ?? string.Empty;
    set
    {
      if (ApiKey is not null)
      {
        ApiKey.Id = value;
      }
    }
  }

  public string Title { get => ApiKey.Title; }

  public ApiKeyEntity ApiKey { get; set; } = default!;

  public List<ScopeEntity> Scopes { get; set; } = default!;

  public List<RegisterEntity> Registers { get; set; } =
    default!;
}
