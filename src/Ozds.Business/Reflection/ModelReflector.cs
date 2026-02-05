using System.Collections.Concurrent;
using System.Reflection;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Reflection;

public class ModelReflector(ITypeQueries typeQueries)
{
  private readonly Assembly modelsAssembly = typeof(ModelReflector).Assembly;

  private readonly string modelsNamespace = "Ozds.Business.Models";

  private readonly ConcurrentDictionary<string, Type> nameToTypeCache = new();

  private readonly ConcurrentDictionary<Type, string> typeToNameCache = new();

  public List<Type?> ScopeTypeList
  {
    get
    {
      return
      [
        typeof(LocationModel),
        typeof(NetworkUserModel),
        typeof(NetworkUserMeasurementLocationModel),
        null,
      ];
    }
  }

  public List<Type> PrincipalTypeList
  {
    get { return [typeof(RepresentativeModel), typeof(IMessenger)]; }
  }

  public string ResolveModelName(Type modelType)
  {
    if (
      modelType.Namespace == null
      || !modelType.Namespace.StartsWith(modelsNamespace)
    )
    {
      throw new InvalidOperationException("Model type not found");
    }

    if (typeToNameCache.TryGetValue(modelType, out var name))
    {
      return name;
    }

    name = typeQueries.ResolveHumanFriendlyTypeName(modelType);
    typeToNameCache.TryAdd(modelType, modelType.Name);

    return name;
  }

  public Type ResolveModelType(string name)
  {
    if (nameToTypeCache.TryGetValue(name, out var type))
    {
      return type;
    }

    type =
      typeQueries.ResolveTypeFromHumanFriendlyName(
        modelsAssembly,
        modelsNamespace,
        name
      ) ?? throw new InvalidOperationException("Model type not found");

    nameToTypeCache.TryAdd(name, type);

    return type;
  }
}
