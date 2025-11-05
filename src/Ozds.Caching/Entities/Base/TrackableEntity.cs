using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Base;

public abstract class TrackableEntity : IdentifiableEntity, ITrackableEntity
{
  public DateTimeOffset CreatedOn { get; set; } = default!;
  public required string? CreatedById { get; set; }
  public required DateTimeOffset? LastUpdatedOn { get; set; }
  public required string? LastUpdatedById { get; set; }
  public bool IsDeleted { get; set; } = default!;
  public required DateTimeOffset? DeletedOn { get; set; }
  public required string? DeletedById { get; set; }
}
