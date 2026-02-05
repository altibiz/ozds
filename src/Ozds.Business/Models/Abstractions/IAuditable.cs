namespace Ozds.Business.Models.Abstractions;

public interface IAuditable : IModel
{
  public string AuditingId { get; }

  public string AuditingTitle { get; }

  public DateTimeOffset CreatedOn { get; }

  public string? CreatedById { get; }
}

public interface IAuditableIdentifiable : IAuditable, IIdentifiable { }
