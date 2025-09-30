namespace Ozds.Data.Entities.Abstractions;

public interface IResolvableNotificationEntity : INotificationEntity
{
  public string? AuditingRepresentativeId { get; set; }

  public string? ResolvedById { get; set; }

  public DateTimeOffset? ResolvedOn { get; set; }
}
