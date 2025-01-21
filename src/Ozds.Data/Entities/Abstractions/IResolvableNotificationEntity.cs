namespace Ozds.Data.Entities.Abstractions;

public interface IResolvableNotificationEntity : INotificationEntity
{
  public string? ResolvedById { get; set; }

  public DateTimeOffset? ResolvedOn { get; set; }
}
