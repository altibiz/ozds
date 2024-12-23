using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Test.Extensions;

public static class DbContextExtensions
{
  public static HashSet<MemberInfo> GetNavigations(
    this DbContext dbContext
  )
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetDeclaredNavigations()
        .OfType<INavigationBase>()
        .Concat(e.GetDeclaredSkipNavigations()))
      .Select(n => (MemberInfo?)n.PropertyInfo ?? n.FieldInfo)
      .OfType<MemberInfo>()
      .ToHashSet();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic)
        .OfType<MemberInfo>()
        .Concat(e.ClrType
          .GetFields(
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic))
          .OfType<MemberInfo>())
      .ToList();

    var navigations = clr
      .Where(member =>
        member switch
        {
          PropertyInfo property => entity
            .OfType<PropertyInfo>()
            .Any(entity => entity.Name == property.Name
              && entity.DeclaringType == property.DeclaringType),
          FieldInfo field => entity
            .OfType<FieldInfo>()
            .Any(entity => entity.Name == field.Name
              && entity.DeclaringType == field.DeclaringType),
          _ => false
        })
      .ToHashSet();

    return navigations;
  }

  public static HashSet<MemberInfo> GetForeignKeys(
    this DbContext dbContext
  )
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetDeclaredForeignKeys())
      .OfType<IForeignKey>()
      .SelectMany(n => n.Properties
        .Select(p => (MemberInfo?)p.PropertyInfo ?? p.FieldInfo))
      .OfType<MemberInfo>()
      .ToList();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic)
        .OfType<MemberInfo>()
        .Concat(e.ClrType
          .GetFields(
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic))
          .OfType<MemberInfo>())
      .ToList();

    var foreignKeys = clr
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

    return foreignKeys;
  }

  public static HashSet<MemberInfo> GetIgnoredProperties(
    this DbContext dbContext
  )
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e
        .GetDeclaredProperties()
        .OfType<IPropertyBase>()
        .Concat(e.GetDeclaredComplexProperties())
        .Concat(e
          .GetDeclaredComplexProperties()
          .SelectMany(p => p.ComplexType
            .GetProperties()))
        .Concat(e.GetDeclaredSkipNavigations())
        .Concat(e.GetDeclaredNavigations())
        .Concat(e.GetDeclaredKeys().SelectMany(p => p.Properties))
        .Select(p => (MemberInfo?)p.PropertyInfo ?? p.FieldInfo))
      .OfType<MemberInfo>()
      .Distinct()
      .ToList();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic))
      .OfType<MemberInfo>()
      .Concat(dbContext.Model.GetEntityTypes()
        .SelectMany(e => e.GetComplexProperties()
          .SelectMany(p => p.ComplexType.ClrType
            .GetProperties(
              BindingFlags.Instance
              | BindingFlags.Public
              | BindingFlags.NonPublic))))
      .Concat(dbContext.Model
        .GetEntityTypes()
        .SelectMany(e => e.ClrType
          .GetFields(
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic)))
      .Concat(dbContext.Model.GetEntityTypes()
        .SelectMany(e => e.GetComplexProperties()
          .SelectMany(p => p.ComplexType.ClrType
            .GetFields(
              BindingFlags.Instance
              | BindingFlags.Public
              | BindingFlags.NonPublic))))
      .Distinct()
      .ToList();

    var ignoredProperties = clr
      .Where(x => !entity
        .Exists(e => e.DeclaringType == x.DeclaringType && e.Name == x.Name))
      .ToHashSet();

    return ignoredProperties;
  }

  public static HashSet<MemberInfo> GetKeys(
    this DbContext dbContext
  )
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .Select(e => e.GetKeys())
      .OfType<IKey>()
      .SelectMany(n => n.Properties
        .Select(p => (MemberInfo?)p.PropertyInfo ?? p.FieldInfo))
      .OfType<MemberInfo>()
      .ToHashSet();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic)
        .OfType<MemberInfo>()
        .Concat(e.ClrType
          .GetFields(
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic))
          .OfType<MemberInfo>())
      .ToList();

    var keys = clr
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

    return keys;
  }

  public static HashSet<MemberInfo> GetDiscriminatorColumns(
    this DbContext dbContext
  )
  {
    var entity = dbContext.Model
      .GetEntityTypes()
      .Select(e => e.FindDiscriminatorProperty())
      .Select(p => (MemberInfo?)p?.PropertyInfo ?? p?.FieldInfo)
      .OfType<MemberInfo>()
      .ToHashSet();

    var clr = dbContext.Model
      .GetEntityTypes()
      .SelectMany(e => e.ClrType
        .GetProperties(
          BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic)
        .OfType<MemberInfo>()
        .Concat(e.ClrType
          .GetFields(
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic))
          .OfType<MemberInfo>())
      .ToList();

    var keys = clr
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

    return keys;
  }
}
