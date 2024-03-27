using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Enums;

[PostgresqlEnum]
public enum RoleEntity
{
  OperatorRepresentative,

  LocationRepresentative,

  NetworkUserRepresentative
}
