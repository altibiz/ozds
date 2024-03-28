using Microsoft.EntityFrameworkCore;
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
