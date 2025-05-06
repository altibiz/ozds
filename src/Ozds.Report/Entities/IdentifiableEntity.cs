using Ozds.Report.Entities.Abstractions;

namespace Ozds.Report.Entities;

public abstract class IdentifiableEntity : IEntity
{
  public string Title { get; set; } = default!;
}
