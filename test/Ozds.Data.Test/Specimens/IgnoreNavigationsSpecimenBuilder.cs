using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Test.Specimens;

public class IgnoreNavigationsSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo propertyInfo
      && navigations.Value.Contains(propertyInfo))
    {
      return new OmitSpecimen();
    }

    if (request is FieldInfo fieldInfo
      && navigations.Value.Contains(fieldInfo))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }

  private readonly Lazy<HashSet<object>> navigations = new(() =>
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetDeclaredNavigations()
        .OfType<INavigationBase>()
        .Concat(e.GetDeclaredSkipNavigations()))
      .Select(n => (object?)n.PropertyInfo ?? n.FieldInfo)
      .Where(p => p != null)
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
