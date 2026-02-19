namespace Ozds.Caching.Entities.Abstractions;

public interface ITrackableEntity : IAuditableEntity
{
  public DateTimeOffset? LastUpdatedOn { get; }

  public string? LastUpdatedById { get; }

  public bool IsDeleted { get; }

  public DateTimeOffset? DeletedOn { get; }

  public string? DeletedById { get; }
}

public interface ITrackableIdentifiableEntity
  : ITrackableEntity,
    IIdentifiableEntity { }
