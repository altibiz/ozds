namespace Ozds.Caching.Entities.Abstractions;

public interface IAuditableEntity : IEntity
{
  public DateTimeOffset CreatedOn { get; }

  public string? CreatedById { get; }
}

public interface IAuditableIdentifiableEntity
  : IAuditableEntity,
    IIdentifiableEntity { }
