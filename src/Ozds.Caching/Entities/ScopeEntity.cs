using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Caching.Entities;

public class ScopeEntity : TrackableEntity, IScopeEntity
{
  public string? ScopeModelId { get; set; }

  public string? ScopeModelType { get; set; }

  public ActionEntity ScopeAction { get; set; } = default!;
}
