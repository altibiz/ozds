using System.Linq.Expressions;
using System.Reflection;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

// TODO: through model
// TODO: cache expressions/compilations
// TODO: https://github.com/dotnet/efcore/issues/20604 for string casts

namespace Ozds.Data;

public partial class OzdsDbContext
{
  public Func<T, object> PrimaryKeyOfCompiled<T>()
  {
    return PrimaryKeyOf<T>().Compile();
  }

  public Expression<Func<T, object>> PrimaryKeyOf<T>()
  {
    var entityType = Model.FindEntityType(typeof(T))
      ?? throw new InvalidOperationException(
        $"No entity type found for {typeof(T)}");
    var key = entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {typeof(T)}");
    var properties = key.Properties;
    var parameter = Expression.Parameter(typeof(T));
    var propertyExpressions = properties
      .Select(property =>
        property.PropertyInfo is { } propertyInfo
          ? Expression.Property(parameter, propertyInfo)
          : property.FieldInfo is { } fieldInfo
            ? Expression.Field(parameter, fieldInfo)
            : throw new InvalidOperationException(
              $"No property or field found for {property}"));
    if (properties.Count == 1)
    {
      var propertyExpression = propertyExpressions.Single();
      var propertyToStringExpression =
      Expression.Convert(
        Expression.Convert(propertyExpression, typeof(object)), typeof(string));
      return Expression.Lambda<Func<T, object>>(
        propertyToStringExpression, parameter);
    }

    var propertyToStringExpressions =
      propertyExpressions.Select(propertyExpression =>
      Expression.Convert(
        Expression.Convert(propertyExpression, typeof(object)), typeof(string))
          );
    var stringJoinWithDashExpression = Expression.Call(
      typeof(string).GetMethod(nameof(string.Join),
        new[] { typeof(string), typeof(string[]) }) ??
          throw new InvalidOperationException(
              $"No {nameof(string.Join)} method found in {typeof(string)}"),
      Expression.Constant("-"),
      Expression.NewArrayInit(typeof(string), propertyExpressions));

    return Expression.Lambda<Func<T, object>>(
      stringJoinWithDashExpression, parameter);
  }

  public Func<T, object> PrimaryKeyOfCompiled<T, U>(Expression<Func<T, U>> prefix)
  {
    return PrimaryKeyOf(prefix).Compile();
  }

  public Expression<Func<T, object>> PrimaryKeyOf<T, U>(Expression<Func<T, U>> prefix)
  {
    var parameter = Expression.Parameter(typeof(T));
    var callExpression = Expression.Invoke(prefix, parameter);
    var primaryKeyExpression = PrimaryKeyOf<U>();
    return Expression.Lambda<Func<T, object>>(
      Expression.Invoke(primaryKeyExpression, callExpression), parameter);
  }

  public Func<T, bool> PrimaryKeyEqualsCompiled<T>(string id)
  {
    return PrimaryKeyEquals<T>(id).Compile();
  }

  public Expression<Func<T, bool>> PrimaryKeyEquals<T>(string id)
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var primaryKeyExpression = PrimaryKeyOf<T>();
    var primaryKeyEqualsExpression = Expression.Equal(
      Expression.Invoke(primaryKeyExpression, parameter),
      Expression.Constant(id));
    return Expression.Lambda<Func<T, bool>>(
      primaryKeyEqualsExpression,
      parameter
    );
  }

  public Func<T, bool> PrimaryKeyInCompiled<T>(ICollection<string> ids)
  {
    return PrimaryKeyIn<T>(ids).Compile();
  }

  public Expression<Func<T, bool>> PrimaryKeyIn<T>(ICollection<string> ids)
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var primaryKeyExpression = PrimaryKeyOf<T>();
    var primaryKeyInExpression = Expression.Call(
      typeof(Enumerable),
      nameof(Enumerable.Contains),
      new[] { typeof(string) },
      Expression.Constant(ids),
      Expression.Invoke(primaryKeyExpression, parameter));
    return Expression.Lambda<Func<T, bool>>(
      primaryKeyInExpression,
      parameter
    );
  }

  public Func<T, object> ForeignKeyOfCompiled<T>(string property)
  {
    return ForeignKeyOf<T>(property).Compile();
  }

  public Expression<Func<T, object>> ForeignKeyOf<T>(string property)
  {
    var type = typeof(T);
    var entityType = Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}");
    var navigation = entityType.FindNavigation(property)
      ?? throw new InvalidOperationException(
        $"No navigation found for {type} {property}");
    var foreignKey = navigation.ForeignKey ??
      throw new InvalidOperationException(
        $"No foreign key found for {type} {property}");
    var properties = foreignKey.Properties;
    var parameter = Expression.Parameter(type);
    var propertyExpressions = properties
      .Select(property =>
        property.PropertyInfo is { } propertyInfo
          ? Expression.Property(parameter, propertyInfo)
          : property.FieldInfo is { } fieldInfo
            ? Expression.Field(parameter, fieldInfo)
            : throw new InvalidOperationException(
              $"No property or field found for {property}"));
    if (properties.Count == 1)
    {
      var propertyExpression = propertyExpressions.Single();
      var propertyToStringExpression =
      Expression.Convert(
        Expression.Convert(propertyExpression, typeof(object)), typeof(string));
      return Expression.Lambda<Func<T, object>>(
        propertyToStringExpression, parameter);
    }
    var propertyToStringExpressions =
      propertyExpressions.Select(propertyExpression =>
      Expression.Convert(
        Expression.Convert(propertyExpression, typeof(object)), typeof(string))
          );
    var stringJoinWithDashExpression = Expression.Call(
      typeof(string).GetMethod(nameof(string.Join),
        new[] { typeof(string), typeof(string[]) }) ??
          throw new InvalidOperationException(
              $"No {nameof(string.Join)} method found in {typeof(string)}"),
      Expression.Constant("-"),
      Expression.NewArrayInit(typeof(string), propertyExpressions));
    return Expression.Lambda<Func<T, object>>(
      stringJoinWithDashExpression, parameter);
  }

  public Func<T, object> ForeignKeyOfCompiled<T, U>(Expression<Func<T, U>> prefix, string property)
  {
    return ForeignKeyOf(prefix, property).Compile();
  }

  public Expression<Func<T, object>> ForeignKeyOf<T, U>(Expression<Func<T, U>> prefix, string property)
  {
    var parameter = Expression.Parameter(typeof(T));
    var callExpression = Expression.Invoke(prefix, parameter);
    var foreignKeyExpression = ForeignKeyOf<U>(property);
    return Expression.Lambda<Func<T, object>>(
      Expression.Invoke(foreignKeyExpression, callExpression), parameter);
  }
}
