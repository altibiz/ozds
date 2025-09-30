namespace Ozds.Data.Entities.Composite;

public class ApiKeyAuthEntity
{
  public ApiKeyEntity ApiKey { get; set; } = default!;

  public List<ScopeEntity> Scopes { get; set; } = default!;

  public Dictionary<string, List<RegisterEntity>> Registers { get; set; } =
    default!;
}
