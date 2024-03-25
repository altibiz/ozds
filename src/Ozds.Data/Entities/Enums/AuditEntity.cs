using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Enums;

[PostgresqlEnum]
public enum AuditEntity
{
  Query,
  Creation,
  Modification,
  Deletion
}
