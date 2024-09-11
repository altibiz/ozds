using Microsoft.EntityFrameworkCore;
using Npgsql;
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

public class
  TopicEntityNpgsqlDataSourceConfiguration : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<TopicEntity>();
  }
}
