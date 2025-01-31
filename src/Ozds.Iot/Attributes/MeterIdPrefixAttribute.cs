using System.Collections.ObjectModel;
using System.Reflection;

namespace Ozds.Iot.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class MeterIdPrefixAttribute(string Prefix) : Attribute
{
  public static readonly ReadOnlyDictionary<string, Type> TypesByMeterIdPrefix =
    GetMeterIdPrefixes();

  public string Prefix { get; set; } = Prefix;

  private static ReadOnlyDictionary<string, Type> GetMeterIdPrefixes()
  {
    var result = new Dictionary<string, Type>();
    var assemblyTypes =
      typeof(MeterIdPrefixAttribute).Assembly
        .GetTypes()
        .Where(t => !t.IsAbstract && !t.IsGenericType);
    foreach (var assemblyType in assemblyTypes)
    {
      if (assemblyType
          .GetCustomAttributes<MeterIdPrefixAttribute>()
          .FirstOrDefault() is { } attribute)
      {
        result[attribute.Prefix] = assemblyType;
      }
    }

    return new ReadOnlyDictionary<string, Type>(result);
  }
}
