using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class ApiKeyAuthModel : ICachedComposite
{
  public string CacheId => ApiKey.Id;

  public ApiKeyModel ApiKey { get; set; } = default!;

  public List<ScopeModel> Scopes { get; set; } = default!;

  public Dictionary<string, List<RegisterModel>> Registers { get; set; } =
    default!;
}
