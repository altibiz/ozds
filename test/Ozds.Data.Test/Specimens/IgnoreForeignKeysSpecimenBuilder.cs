using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Entities;

namespace Ozds.Data.Test.Specimens;

public class IgnoreForeignKeysSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo propertyInfo
      && foreignKeys.Value.Contains(propertyInfo))
    {
      return new OmitSpecimen();
    }

    if (request is FieldInfo fieldInfo
      && foreignKeys.Value.Contains(fieldInfo))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }

  private readonly Lazy<HashSet<object>> foreignKeys = new(() =>
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetDeclaredForeignKeys())
      .OfType<IForeignKey>()
      .SelectMany(n => n.Properties
        .Select(p => (object?)p.PropertyInfo ?? p.FieldInfo))
      .OfType<object>()
      .ToList();

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
