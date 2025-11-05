using Ozds.Caching.Entities.Base;

namespace Ozds.Caching.Entities;

public class ApiKeyEntity : TrackableEntity
{
  public string PrincipalModelType { get; set; } = default!;

  public string PrincipalModelId { get; set; } = default!;

  public string Hash { get; set; } = default!;

  public DateTimeOffset? ExpiresOn { get; set; } = default!;
}
