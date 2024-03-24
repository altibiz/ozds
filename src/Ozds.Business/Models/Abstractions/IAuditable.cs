namespace Ozds.Business.Models.Abstractions;

public interface IAuditable : IIdentifiable
{
  public DateTimeOffset CreationDate { get; }

  public string? CreatedById { get; }

  public DateTimeOffset? LastUpdateDate { get; }

  public string? LastUpdatedById { get; }

  public bool IsDeleted { get; }

  public DateTimeOffset? DeletionDate { get; }

  public string? DeletedById { get; }
}
