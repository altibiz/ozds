using System.Reflection;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Queries.Implementations;

public class TypeQueries : ITypeQueries
{
  public string ResolveHumanFriendlyTypeName(Type type, bool trim = false)
  {
    return ResolveHumanFriendlyTypeName(type, type.Name, trim);
  }

  public Type? ResolveTypeFromHumanFriendlyName(
    Assembly assembly,
    string @namespace,
    string name
  )
  {
    var direct = Type.GetType(name);
    if (direct is not null)
    {
      return direct;
    }

    var candidates = assembly
      .GetTypes()
      .Where(type =>
        type.Namespace != null && type.Namespace.StartsWith(@namespace)
      )
      .ToList();

    return candidates.FirstOrDefault(type =>
      ResolveHumanFriendlyTypeName(type, type.Name, true)
      == ResolveHumanFriendlyTypeName(type, name, true)
    );
  }

  public IEnumerable<Type> ResolveSubtypes(
    Type type,
    Assembly? assembly = null,
    string? @namespace = null
  )
  {
    var assemblies = assembly is null
      ? AppDomain.CurrentDomain.GetAssemblies().ToList()
      : [assembly];

    return @namespace is null
      ? assemblies.SelectMany(assembly =>
        assembly
          .GetTypes()
          .Where(assemblyType =>
            assemblyType.IsAssignableTo(type) && !assemblyType.IsAbstract
          )
      )
      : assemblies.SelectMany(assembly =>
        assembly
          .GetTypes()
          .Where(assemblyType =>
            assemblyType.Namespace is not null
            && assemblyType.Namespace.StartsWith(@namespace)
            && assemblyType.IsAssignableTo(type)
            && !assemblyType.IsAbstract
          )
      );
  }

  private static string ResolveHumanFriendlyTypeName(
    Type type,
    string baseName,
    bool trim = false
  )
  {
    if (!type.IsGenericType)
    {
      if (trim)
      {
        if (type.IsInterface && baseName.StartsWith('I'))
        {
          baseName = baseName[1..];
        }

        if (baseName.LastIndexOf("Model") is > 0 and var modelIndex)
        {
          baseName = baseName[..modelIndex];
        }

        if (baseName.LastIndexOf("Entity") is > 0 and var entityIndex)
        {
          baseName = baseName[..entityIndex];
        }
      }

      return baseName;
    }

    var backtickIndex = baseName.IndexOf('`');
    if (backtickIndex > 0)
    {
      baseName = baseName[..backtickIndex];
    }

    var lowerThanIndex = baseName.IndexOf('<');
    if (lowerThanIndex > 0)
    {
      baseName = baseName[..lowerThanIndex];
    }

    var genericArgs = string.Join(
      ", ",
      type.GetGenericArguments().Select(x => x.Name)
    );
    return $"{baseName}<{genericArgs}>";
  }
}
