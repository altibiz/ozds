using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Test.Specimens;

public class IgnoreKeysSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo propertyInfo
      && keys.Value.Contains(propertyInfo))
    {
      return new OmitSpecimen();
    }

    if (request is FieldInfo fieldInfo
      && keys.Value.Contains(fieldInfo))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }

  private readonly Lazy<HashSet<object>> keys = new(() =>
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .Select(e => e.GetKeys())
      .OfType<IKey>()
      .SelectMany(n => n.Properties
        .Select(p => (object?)p.PropertyInfo ?? p.FieldInfo))
      .OfType<object>()
      .ToHashSet();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic)
        .OfType<object>()
        .Concat(e.ClrType
          .GetFields(
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic))
          .OfType<object>())
      .ToList();

    var ignored = clr
      .Where(i =>
        i switch
        {
          PropertyInfo p => entity
            .OfType<PropertyInfo>()
            .Any(e => e.Name == p.Name && e.DeclaringType == p.DeclaringType),
          FieldInfo f => entity
            .OfType<FieldInfo>()
            .Any(e => e.Name == f.Name && e.DeclaringType == f.DeclaringType),
          _ => false
        })
      .ToHashSet();

    return ignored;
  });
}
