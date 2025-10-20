namespace Ozds.Business.Models.Composite;

public class ApiKeyAuthModel
{
  public ApiKeyModel ApiKey { get; set; } = default!;

  public List<ScopeModel> Scopes { get; set; } = default!;

  public Dictionary<string, List<RegisterModel>> Registers { get; set; } =
    default!;
}
