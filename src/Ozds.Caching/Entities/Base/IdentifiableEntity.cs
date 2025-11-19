using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Base;

public abstract class IdentifiableEntity : Entity, IIdentifiableEntity
{
  public string Id { get; set; } = default!;

  public string Title { get; set; } = default!;
}
