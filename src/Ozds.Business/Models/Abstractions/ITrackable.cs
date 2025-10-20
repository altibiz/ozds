namespace Ozds.Business.Models.Abstractions;

public interface ITrackable : IAuditable
{
  public DateTimeOffset? LastUpdatedOn { get; }

  public string? LastUpdatedById { get; }

  public bool IsDeleted { get; }

  public DateTimeOffset? DeletedOn { get; }

  public string? DeletedById { get; }
}

public interface ITrackableIdentifiable : ITrackable, IIdentifiable
{
}
