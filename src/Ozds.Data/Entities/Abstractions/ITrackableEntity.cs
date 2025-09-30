namespace Ozds.Data.Entities.Abstractions;

public interface ITrackableEntity : IAuditableEntity
{
  public DateTimeOffset? LastUpdatedOn { get; set; }

  public string? LastUpdatedById { get; set; }

  public RepresentativeEntity? LastUpdatedBy { get; set; }

  public bool IsDeleted { get; set; }

  public DateTimeOffset? DeletedOn { get; set; }

  public string? DeletedById { get; set; }

  public bool Forget { get; set; }

  public bool Restore { get; set; }
}

public interface ITrackableIdentifiableEntity
  : ITrackableEntity, IIdentifiableEntity
{
}
