using System.Text.Json;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Abstractions;

public interface IEventEntity : IIdentifiableEntity, IReadonlyEntity
{
  public DateTimeOffset Timestamp { get; }

  public LevelEntity Level { get; }

  public JsonDocument Content { get; set; }

  public List<CategoryEntity> Categories { get; }
}
