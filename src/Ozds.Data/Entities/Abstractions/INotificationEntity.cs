using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Abstractions;

public interface INotificationEntity : IIdentifiableEntity
{
  public DateTimeOffset Timestamp { get; }

  public string? EventId { get; }

  public string Summary { get; }

  public string Content { get; }

  public List<TopicEntity> Topics { get; }
}
