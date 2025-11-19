using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IScope : ITrackableIdentifiable, ICachedIdentifiable
{
  public string? ScopeModelId { get; }

  public string? ScopeModelType { get; }

  public ActionModel ScopeAction { get; }
}
