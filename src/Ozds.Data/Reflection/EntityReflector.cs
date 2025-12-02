using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Options;

namespace Ozds.Data.Reflection;

public sealed class EntityReflector : IAsyncDisposable
{
  private readonly Lazy<List<Type>> aggregateTypes;

  private readonly Lazy<Dictionary<Type, Type>> aggregateTypeToMeterType;

  private readonly DataDbContext context;

  private readonly Assembly entitiesAssembly =
    typeof(EntityReflector).Assembly;

  private readonly string entitiesNamespace = "Ozds.Data.Entities";
#pragma warning disable S4487 // Unread "private" fields should be removed
  private readonly IDbContextFactory<DataDbContext> factory;
#pragma warning restore S4487 // Unread "private" fields should be removed

  private readonly Lazy<List<Type>> measurementTypes;

  private readonly ConcurrentDictionary<string, Type> nameToTypeCache =
    new();

  private readonly IOptions<OzdsDataOptions> options;

  private readonly ConcurrentDictionary<string, Type> tableToTypeCache =
    new();

  private readonly ITypeQueries typeQueries;

  private readonly ConcurrentDictionary<Type, string> typeToNameCache =
    new();

  private readonly ConcurrentDictionary<Type, string> typeToTableCache =
    new();

  public EntityReflector(
    IDbContextFactory<DataDbContext> factory,
    ITypeQueries typeQueries,
    IOptions<OzdsDataOptions> options
  )
  {
    this.factory = factory;
    this.typeQueries = typeQueries;
    this.options = options;

    context = factory.CreateDbContext();

    aggregateTypes = new Lazy<List<Type>>(
      () => context.Model
        .GetEntityTypes()
        .Where(
          x =>
            x.ClrType.IsAssignableTo(typeof(IAggregateEntity))
            && !x.ClrType.IsAbstract
            && !x.ClrType.IsGenericType)
        .Select(x => x.ClrType)
        .ToList());

    measurementTypes = new Lazy<List<Type>>(
      () => context.Model
        .GetEntityTypes()
        .Where(
          x =>
            x.ClrType.IsAssignableTo(typeof(IMeasurementEntity))
            && !x.ClrType.IsAbstract
            && !x.ClrType.IsGenericType)
        .Select(x => x.ClrType)
        .ToList());

    aggregateTypeToMeterType = new Lazy<Dictionary<Type, Type>>(
      () => aggregateTypes.Value
        .ToDictionary(
          x => x,
          x => x.GetProperty("Meter")?.PropertyType
            ?? throw new InvalidOperationException(
              $"No meter property found for {x.Name}.")));
  }

  public List<Type> AggregateTypes
  {
    get { return aggregateTypes.Value; }
  }

  public List<Type> MeasurementTypes
  {
    get { return measurementTypes.Value; }
  }

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

    if (options.Value.SelfContainedReflection)
    {
      try
      {
        name = context.GetTableName(entityType)
          ?? throw new InvalidOperationException("Table name not found");
      }
      catch
      {
        name = entityType.Name;
      }
    }
    else
    {
      name = context.GetTableName(entityType)
        ?? throw new InvalidOperationException("Table name not found");
    }

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

  public Type ResolveAggregateMeterType(Type aggregateType)
  {
    return aggregateTypeToMeterType.Value[aggregateType];
  }
}
