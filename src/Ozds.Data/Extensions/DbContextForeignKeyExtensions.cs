using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Context;

namespace Ozds.Data.Extensions;

public static class DbContextForeignKeyExtensions
{
  private static readonly
    ConcurrentDictionary<(Type dbContextType, Type entityType, string foreignKey
      ), Delegate>
    _foreignKeyGetterCompiledCache = new();

  private static readonly
    ConcurrentDictionary<(Type dbContextType, Type entityType, string foreignKey
      ), Expression>
    _foreignKeyGetterExpressionCache = new();

  public static Func<T, object> ForeignKeyOfCompiled<T>(
    this DbContext context,
    string property)
  {
    var typeBasedFunc = context.ForeignKeyOfCompiled(typeof(T), property);
    return entity => typeBasedFunc(entity!);
  }

  public static Expression<Func<T, object>> ForeignKeyOf<T>(
    this DbContext context,
    string property)
  {
    var typeBasedExpr = context.ForeignKeyOf(typeof(T), property);
    var parameter = Expression.Parameter(typeof(T), "entity");
    var cast = Expression.Convert(parameter, typeof(object));
    var body = Expression.Invoke(typeBasedExpr, cast);
    return Expression.Lambda<Func<T, object>>(body, parameter);
  }

  public static Func<T, bool> ForeignKeyEqualsCompiled<T>(
    this DbContext context,
    string property,
    string id)
  {
    var typeBasedFunc = context
      .ForeignKeyEqualsCompiled(typeof(T), property, id);
    return entity => typeBasedFunc(entity!);
  }

  public static Expression<Func<T, bool>> ForeignKeyEquals<T>(
    this DbContext context,
    string property,
    string id)
  {
    var typeBasedExpr = context.ForeignKeyEquals(typeof(T), property, id);
    var parameter = Expression.Parameter(typeof(T), "entity");
    var cast = Expression.Convert(parameter, typeof(object));
    var body = Expression.Invoke(typeBasedExpr, cast);
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  public static Func<T, bool> ForeignKeyInCompiled<T>(
    this DbContext context,
    string property,
    IEnumerable<string> ids)
  {
    var typeBasedFunc = context.ForeignKeyInCompiled(typeof(T), property, ids);
    return entity => typeBasedFunc(entity!);
  }

  public static Expression<Func<T, bool>> ForeignKeyIn<T>(
    this DbContext context,
    string property,
    IEnumerable<string> ids)
  {
    var typeBasedExpr = context.ForeignKeyIn(typeof(T), property, ids);
    var parameter = Expression.Parameter(typeof(T), "entity");
    var cast = Expression.Convert(parameter, typeof(object));
    var body = Expression.Invoke(typeBasedExpr, cast);
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  public static Func<object, object> ForeignKeyOfCompiled(
    this DbContext context,
    Type entityType,
    string property)
  {
    var key = (context.GetType(), entityType, property);

    if (_foreignKeyGetterCompiledCache.TryGetValue(key, out var cachedFunc))
    {
      return (Func<object, object>)cachedFunc;
    }

    var expr = context.ForeignKeyOfUncached(entityType, property);
    var compiled = expr.Compile();
    _foreignKeyGetterCompiledCache[key] = compiled;

    return compiled;
  }

  public static Expression<Func<object, object>> ForeignKeyOf(
    this DbContext context,
    Type entityType,
    string property)
  {
    var key = (context.GetType(), entityType, property);

    if (_foreignKeyGetterExpressionCache.TryGetValue(key, out var cachedExpr))
    {
      return (Expression<Func<object, object>>)cachedExpr;
    }

    var expr = context.ForeignKeyOfUncached(entityType, property);
    _foreignKeyGetterExpressionCache[key] = expr;

    return expr;
  }

  public static Func<object, bool> ForeignKeyEqualsCompiled(
    this DbContext context,
    Type entityType,
    string property,
    string id)
  {
    return context.ForeignKeyEqualsUncached(entityType, property, id).Compile();
  }

  public static Expression<Func<object, bool>> ForeignKeyEquals(
    this DbContext context,
    Type entityType,
    string property,
    string id)
  {
    return context.ForeignKeyEqualsUncached(entityType, property, id);
  }

  public static Func<object, bool> ForeignKeyInCompiled(
    this DbContext context,
    Type entityType,
    string property,
    IEnumerable<string> ids)
  {
    return context.ForeignKeyInUncached(entityType, property, ids).Compile();
  }

  public static Expression<Func<object, bool>> ForeignKeyIn(
    this DbContext context,
    Type entityType,
    string property,
    IEnumerable<string> ids)
  {
    return context.ForeignKeyInUncached(entityType, property, ids);
  }

  private static Expression<Func<object, object>> ForeignKeyOfUncached(
    this DbContext context,
    Type entityType,
    string property)
  {
    var keyProperties = context.GetForeignKeyProperties(entityType, property);
    var parameter = Expression.Parameter(typeof(object));
    var convertedParameter = Expression.Convert(parameter, entityType);

    var propertyExpressions = keyProperties
      .Select(
        p =>
          p.PropertyInfo is { } propertyInfo
            ? Expression.Property(convertedParameter, propertyInfo)
            : Expression.Field(
              convertedParameter, p.FieldInfo
              ?? throw new InvalidOperationException(
                $"No field info found for {p}")))
      .ToList();

    Expression resultExpression;
    if (propertyExpressions.Count == 1)
    {
      resultExpression = propertyExpressions.Single();
    }
    else
    {
      var genericTupleType =
        propertyExpressions.Count == 1
          ? typeof(ValueTuple<>)
          : propertyExpressions.Count == 2
            ? typeof(ValueTuple<,>)
            : propertyExpressions.Count == 3
              ? typeof(ValueTuple<,,>)
              : propertyExpressions.Count == 4
                ? typeof(ValueTuple<,,,>)
                : propertyExpressions.Count == 5
                  ? typeof(ValueTuple<,,,,>)
                  : propertyExpressions.Count == 6
                    ? typeof(ValueTuple<,,,,,>)
                    : propertyExpressions.Count == 7
                      ? typeof(ValueTuple<,,,,,,>)
                      : typeof(ValueTuple<,,,,,,,>);
      var tupleType = genericTupleType.MakeGenericType(
        propertyExpressions.Select(p => p.Type).ToArray());
      var constructor = tupleType.GetConstructors().Single();

      resultExpression = Expression.New(
        constructor,
        propertyExpressions
      );
    }

    return Expression.Lambda<Func<object, object>>(
      Expression.Convert(resultExpression, typeof(object)),
      parameter);
  }

  private static Expression<Func<object, bool>> ForeignKeyEqualsUncached(
    this DbContext context,
    Type entityType,
    string property,
    string id)
  {
    var keyProperties = context.GetForeignKeyProperties(entityType, property);
    var idParts = id.Split(DataDbContext.KeyJoin);

    if (keyProperties.Count != idParts.Length)
    {
      throw new ArgumentException(
        "The number of ids must match the number of key properties.");
    }

    var parameter = Expression.Parameter(typeof(object));
    var convertedParameter = Expression.Convert(parameter, entityType);

    Expression? equalityExpression = null;

    foreach (var (keyProperty, idValue) in keyProperties.Zip(idParts))
    {
      var propertyExpression =
        keyProperty.PropertyInfo is { } propertyInfo
          ? Expression.Property(convertedParameter, propertyInfo)
          : Expression.Field(
            convertedParameter, keyProperty.FieldInfo
            ?? throw new InvalidOperationException(
              $"No field info found for {property}"));
      var convertedId = Expression.Constant(
        Convert.ChangeType(idValue, keyProperty.ClrType));
      var equalsExpression = Expression.Equal(propertyExpression, convertedId);

      equalityExpression = equalityExpression == null
        ? equalsExpression
        : Expression.AndAlso(equalityExpression, equalsExpression);
    }

    return Expression.Lambda<Func<object, bool>>(
      equalityExpression!, parameter);
  }

  private static Expression<Func<object, bool>> ForeignKeyInUncached(
    this DbContext context,
    Type entityType,
    string property,
    IEnumerable<string> ids)
  {
    var keyProperties = context.GetForeignKeyProperties(entityType, property);
    var parameter = Expression.Parameter(typeof(object));
    var convertedParameter = Expression.Convert(parameter, entityType);

    var idExpressions = new List<Expression>();

    foreach (var id in ids)
    {
      var idParts = id.Split(DataDbContext.KeyJoin);
      if (idParts.Length != keyProperties.Count)
      {
        throw new ArgumentException(
          "The number of id parts must match the number of key properties.");
      }

      Expression? keyMatchExpression = null;

      foreach (var (propertyExpression, idPart) in keyProperties.Zip(idParts))
      {
        var propertyExpressionConverted =
          propertyExpression.PropertyInfo is { } propertyInfo
            ? Expression.Property(convertedParameter, propertyInfo)
            : Expression.Field(
              convertedParameter, propertyExpression.FieldInfo
              ?? throw new InvalidOperationException(
                $"No field info found for {property}"));
        var convertedIdPart = Expression.Constant(
          Convert.ChangeType(idPart, propertyExpression.ClrType));

        var equalsExpression = Expression.Equal(
          propertyExpressionConverted,
          convertedIdPart);

        keyMatchExpression = keyMatchExpression == null
          ? equalsExpression
          : Expression.AndAlso(keyMatchExpression, equalsExpression);
      }

      idExpressions.Add(keyMatchExpression!);
    }

    var finalOrExpression = idExpressions
      .DefaultIfEmpty(Expression.Constant(false))
      .Aggregate(Expression.OrElse);

    return Expression.Lambda<Func<object, bool>>(finalOrExpression, parameter);
  }

  private static IReadOnlyList<IProperty> GetForeignKeyProperties(
    this DbContext context,
    Type entityType,
    string navigationProperty)
  {
    var entityTypeInfo = context.Model.FindEntityType(entityType)
      ?? throw new InvalidOperationException(
        $"No entity type found for {entityType}");

    var navigation = entityTypeInfo.FindNavigation(navigationProperty)
      ?? throw new InvalidOperationException(
        $"No navigation found for {entityType}"
        + $" with property {navigationProperty}");

    var foreignKey = navigation.ForeignKey
      ?? throw new InvalidOperationException(
        $"No foreign key found for {entityType}"
        + $" with property {navigationProperty}");

    return foreignKey.Properties;
  }
}
