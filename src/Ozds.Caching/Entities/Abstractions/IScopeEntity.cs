using Ozds.Caching.Entities.Enums;

namespace Ozds.Caching.Entities.Abstractions;

public interface IScopeEntity : ITrackableIdentifiableEntity
{
  public string? ScopeModelId { get; }

  public string? ScopeModelType { get; }

  public ActionEntity ScopeAction { get; }
}
