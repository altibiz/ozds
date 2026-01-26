using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

public static class ITypeBaseExtensions
{
  public static IEnumerable<IProperty> GetScalarPropertiesRecursive(
    this ITypeBase type
  )
  {
    foreach (var property in type.GetProperties())
    {
      yield return property;
    }

    foreach (var complexProperty in type.GetComplexProperties())
    {
      foreach (var property in complexProperty.ComplexType
        .GetScalarPropertiesRecursive())
      {
        yield return property;
      }
    }
  }
}
