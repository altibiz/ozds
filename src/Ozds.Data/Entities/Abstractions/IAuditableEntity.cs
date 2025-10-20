namespace Ozds.Data.Entities.Abstractions;

public interface IAuditableEntity : IEntity
{
  public string AuditingId { get; }

  public string AuditingTitle { get; }

  public DateTimeOffset CreatedOn { get; set; }

  public string? CreatedById { get; set; }

  public RepresentativeEntity? CreatedBy { get; set; }

  public string? AuditingRepresentativeId { get; set; }
}

public interface IAuditableIdentifiableEntity
  : IAuditableEntity, IIdentifiableEntity
{
}
