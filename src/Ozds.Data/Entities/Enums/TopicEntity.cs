using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum TopicEntity
{
  All,
  Messenger,
  MessengerInactivity
}

public class TopicEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<TopicEntity>();
  }
}
