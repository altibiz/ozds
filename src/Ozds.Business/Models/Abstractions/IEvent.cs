using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IEvent : IIdentifiable, IReadonly
{
  public DateTimeOffset Timestamp { get; }

  public LevelModel Level { get; }

  public string Description { get; }
}
