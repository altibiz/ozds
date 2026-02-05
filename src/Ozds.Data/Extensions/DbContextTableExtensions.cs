using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

public static class DbContextTableExtensions
{
  public static string? GetTableName(this DbContext context, Type type)
  {
    return context.Model.FindEntityType(type)?.GetTableName();
  }

  public static string? GetColumnName(
    this DbContext context,
    Type type,
    IEnumerable<string> propertyNames
  )
  {
    string? Recursive(
      ITypeBase type,
      StoreObjectIdentifier storeObjectIdentifier,
      string[] propertyNames
    )
    {
      if (propertyNames.Length == 0)
      {
        throw new ArgumentException("Property name is required.");
      }

      if (propertyNames.Length == 1)
      {
        return type?.FindProperty(propertyNames[0])?.GetColumnName();
      }

      var complexType = type.GetComplexProperties()
        .FirstOrDefault(x => x.Name == propertyNames[0]);
      if (complexType is null)
      {
        throw new InvalidOperationException(
          $"No property {propertyNames[0]} on type {type.Name} "
            + $"while resolving {string.Join(".", propertyNames)}."
        );
      }

      return Recursive(
        complexType.ComplexType,
        storeObjectIdentifier,
        propertyNames.Skip(1).ToArray()
      );
    }

    var entityType = context.Model.FindEntityType(type);
    if (entityType is null)
    {
      throw new InvalidOperationException($"Entity type {type.Name} not found");
    }

    var storeObjectIdentifier = StoreObjectIdentifier.Create(
      entityType,
      StoreObjectType.Table
    );
    if (storeObjectIdentifier is null)
    {
      throw new InvalidOperationException(
        $"Store object identifier for entity type {type.Name} not found"
      );
    }

    return Recursive(
      entityType,
      storeObjectIdentifier.Value,
      propertyNames.ToArray()
    );
  }
}
