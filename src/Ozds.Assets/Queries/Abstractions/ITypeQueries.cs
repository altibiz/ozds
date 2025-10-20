using System.Reflection;

namespace Ozds.Assets.Queries.Abstractions;

public interface ITypeQueries : IQueries
{
  public string ResolveHumanFriendlyTypeName(
    Type type,
    bool trim = false
  );

  public Type? ResolveTypeFromHumanFriendlyName(
    Assembly assembly,
    string @namespace,
    string name
  );
}
