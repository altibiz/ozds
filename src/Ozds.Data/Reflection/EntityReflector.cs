using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Extensions;

namespace Ozds.Data.Reflection;

public sealed class EntityReflector(
  IDbContextFactory<DataDbContext> factory,
  ITypeQueries typeQueries
) : IAsyncDisposable
{
  private readonly DataDbContext context = factory.CreateDbContext();

  private readonly Assembly entitiesAssembly =
    typeof(EntityReflector).Assembly;

  private readonly string entitiesNamespace = "Ozds.Data.Entities";

  private readonly ConcurrentDictionary<string, Type> nameToTypeCache =
    new();

  private readonly ConcurrentDictionary<string, Type> tableToTypeCache =
    new();

  private readonly ConcurrentDictionary<Type, string> typeToNameCache =
    new();

  private readonly ConcurrentDictionary<Type, string> typeToTableCache =
    new();

  public ValueTask DisposeAsync()
  {
    return context.DisposeAsync();
  }

  public string ResolveEntityTable(Type entityType)
  {
    if (typeToTableCache.TryGetValue(entityType, out var name))
    {
      return name;
    }

    name = context.GetTableName(entityType)
      ?? throw new InvalidOperationException("Table name not found");
    typeToTableCache.TryAdd(entityType, name);

    return name;
  }

  public string ResolveEntityName(Type entityType)
  {
    if (entityType.Namespace == null
      || !entityType.Namespace.StartsWith(entitiesNamespace))
    {
      throw new InvalidOperationException("Entity type not found");
    }

    if (typeToNameCache.TryGetValue(entityType, out var name))
    {
      return name;
    }

    name = typeQueries.ResolveHumanFriendlyTypeName(entityType);
    typeToNameCache.TryAdd(entityType, name);

    return name;
  }

  public Type ResolveEntityTypeFromTable(string name)
  {
    if (tableToTypeCache.TryGetValue(name, out var type))
    {
      return type;
    }

    type = context.Model
        .GetEntityTypes()
        .FirstOrDefault(entity => entity.GetTableName() == name)
        ?.ClrType
      ?? throw new InvalidOperationException("Entity type not found");

    tableToTypeCache.TryAdd(name, type);

    return type;
  }

  public Type ResolveEntityTypeFromName(string name)
  {
    if (nameToTypeCache.TryGetValue(name, out var type))
    {
      return type;
    }

    type = typeQueries.ResolveTypeFromHumanFriendlyName(
      entitiesAssembly,
      entitiesNamespace,
      name
    ) ?? throw new InvalidOperationException("Entity type not found");

    nameToTypeCache.TryAdd(name, type);

    return type;
  }
}
