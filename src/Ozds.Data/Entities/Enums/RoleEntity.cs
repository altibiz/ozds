using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum RoleEntity
{
  OperatorRepresentative,

  LocationRepresentative,

  NetworkUserRepresentative
}

public class RoleEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<RoleEntity>();
  }
}

public class RoleEntityNpgsqlDataSourceConfiguration : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<RoleEntity>();
  }
}
