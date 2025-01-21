namespace Ozds.Business.Models.Abstractions;

public interface IResolvableNotification : INotification
{
  public string? ResolvedById { get; }

  public DateTimeOffset? ResolvedOn { get; }
}
