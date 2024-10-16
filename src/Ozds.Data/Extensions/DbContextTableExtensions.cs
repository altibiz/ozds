using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Extensions;

public static class DbContextTableExtensions
{
  public static string? GetTableNameFromEntityType(
    this DbContext context,
    Type entityType
  )
  {
    return context.Model.FindEntityType(entityType)?.GetTableName();
  }

  public static Type? GetEntityTypeFromTableName(
    this DbContext context,
    string tableName
  )
  {
    return context.Model
      .GetEntityTypes()
      .FirstOrDefault(entity => entity.GetTableName() == tableName)
      ?.ClrType;
  }

  public static string? GetEntityTypeNameFromEntityType(
    this DbContext context,
    Type entityType
  )
  {
    return context.Model.FindEntityType(entityType)?.ClrType.Name;
  }

  public static Type? GetEntityTypeFromEntityTypeName(
    this DbContext context,
    string entityTypeName
  )
  {
    return context.Model
      .GetEntityTypes()
      .FirstOrDefault(entity => entity.ClrType.Name == entityTypeName)
      ?.ClrType;
  }
}
