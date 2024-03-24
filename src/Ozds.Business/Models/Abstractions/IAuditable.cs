namespace Ozds.Business.Models.Abstractions;

public interface IAuditable : IIdentifiable, IModel
{
  public DateTimeOffset CreatedOn { get; }

  public string? CreatedById { get; }

  public DateTimeOffset? LastUpdatedOn { get; }

  public string? LastUpdatedById { get; }

  public bool IsDeleted { get; }

  public DateTimeOffset? DeletedOn { get; }

  public string? DeletedById { get; }
}
