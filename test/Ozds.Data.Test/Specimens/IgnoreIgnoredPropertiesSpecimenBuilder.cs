using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Test.Specimens;

public class IgnoreIgnoredPropertiesSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo propertyInfo
      && ignoredProperties.Value.Contains(propertyInfo))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }

  private readonly Lazy<HashSet<PropertyInfo>> ignoredProperties = new(() =>
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e
        .GetDeclaredProperties()
        .OfType<IPropertyBase>()
        .Concat(e.GetDeclaredComplexProperties())
        .Concat(e.GetDeclaredSkipNavigations())
        .Concat(e.GetDeclaredNavigations())
        .Concat(e.GetDeclaredKeys().SelectMany(p => p.Properties))
        .Select(p => p.PropertyInfo))
      .OfType<PropertyInfo>()
      .ToList();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic))
      .ToList();

    var ignored = clr
      .Where(x => !entity
        .Exists(e => e.DeclaringType == x.DeclaringType && e.Name == x.Name))
      .ToHashSet();

    return ignored;
  });
}
