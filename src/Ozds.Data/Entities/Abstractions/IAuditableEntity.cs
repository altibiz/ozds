namespace Ozds.Data.Entities.Abstractions;

public interface IAuditableEntity : IIdentifiableEntity
{
  public DateTimeOffset CreatedOn { get; }

  public string? CreatedById { get; }

  public RepresentativeEntity? CreatedBy { get; }

  public DateTimeOffset? LastUpdatedOn { get; }

  public string? LastUpdatedById { get; }

  public RepresentativeEntity? LastUpdatedBy { get; }

  public bool IsDeleted { get; }

  public DateTimeOffset? DeletedOn { get; }

  public string? DeletedById { get; }

  public bool Forget { get; set; }

  public bool Restore { get; set; }
}
