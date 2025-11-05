using System.Collections.Concurrent;
using System.Reflection;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Reflection;

public class EntityReflector(
  ITypeQueries typeQueries
)
{
  private const string CacheKeySeparator = "@";

  private readonly Assembly entitiesAssembly =
    typeof(EntityReflector).Assembly;

  private readonly string entitiesNamespace = "Ozds.Caching.Entities";

  private readonly ConcurrentDictionary<string, Type> nameToTypeCache =
    new();

  private readonly ConcurrentDictionary<Type, string> typeToNameCache =
    new();

  private readonly ConcurrentDictionary<Type, IReadOnlyList<Type>> subtypesCache =
    new();

  public string ResolveEntityKeyFromIdentifiable(
    IIdentifiableEntity identifiable
  )
  {
    return ResolveEntityKeyFromId(
      identifiable.GetType(),
      identifiable.Id
    );
  }

  public string ResolveEntityKeyFromComposite(
    ICompositeEntity composite
  )
  {
    return ResolveEntityKeyFromId(
      composite.GetType(),
      composite.Id
    );
  }

  public string ResolveEntityKeyFromJoin(IJoinEntity join)
  {
    return ResolveEntityKeyFromId(
      join.GetType(),
      join.Id
    );
  }

  public string ResolveEntityKeyFromId<T>(
    string id
  )
    where T : IEntity
  {
    return ResolveEntityKeyFromId(typeof(T), id);
  }

  public string ResolveEntityKeyFromId(
    Type entityType,
    string id
  )
  {
    return $"{ResolveEntityName(entityType)}{CacheKeySeparator}{id}";
  }

  public Type ResolveEntityTypeFromKey(string key)
  {
    return ResolveEntityType(key.Split(CacheKeySeparator)[0]);
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
    typeToNameCache.TryAdd(entityType, entityType.Name);

    return name;
  }

  public Type ResolveEntityType(string name)
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

  public IEnumerable<Type> ResolveSubtypes<T>()
  {
    return ResolveSubtypes(typeof(T));
  }

  public IEnumerable<Type> ResolveSubtypes(Type type)
  {
    if (subtypesCache.TryGetValue(type, out var subtypes))
    {
      return subtypes;
    }

    var types = typeQueries
      .ResolveSubtypes(type, entitiesAssembly, entitiesNamespace)
      .ToList();

    subtypesCache.TryAdd(type, types);

    return types;
  }
}
