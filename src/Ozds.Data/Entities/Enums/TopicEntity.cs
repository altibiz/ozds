using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum TopicEntity
{
  General
}

public class TopicEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<TopicEntity>();
  }
}
