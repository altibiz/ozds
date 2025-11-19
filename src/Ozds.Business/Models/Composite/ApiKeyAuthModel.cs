using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class ApiKeyAuthModel : ICachedComposite
{
  public ApiKeyModel ApiKey { get; set; } = default!;

  public List<ScopeModel> Scopes { get; set; } = default!;

  public Dictionary<string, List<RegisterModel>> Registers { get; set; } =
    default!;

  public string CacheId
  {
    get { return ApiKey.Id; }
  }
}
