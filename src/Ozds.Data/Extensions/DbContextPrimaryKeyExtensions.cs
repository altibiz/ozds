using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Context;

namespace Ozds.Data.Extensions;

public static class DbContextPrimaryKeyExtensions
{
  private static readonly
    ConcurrentDictionary<(Type dbContextType, Type entityType), Delegate>
    _primaryKeyGetterCompiledCache = new();

  private static readonly
    ConcurrentDictionary<(Type dbContextType, Type entityType), Expression>
    _primaryKeyGetterExpressionCache = new();

  public static Func<T, object> PrimaryKeyOfCompiled<T>(
    this DbContext context)
  {
    var typeBasedFunc = context.PrimaryKeyOfCompiled(typeof(T));
    return entity => typeBasedFunc(entity!);
  }

  public static Expression<Func<T, object>> PrimaryKeyOf<T>(
    this DbContext context)
  {
    var typeBasedExpr = context.PrimaryKeyOf(typeof(T));
    var parameter = Expression.Parameter(typeof(T), "entity");
    var cast = Expression.Convert(parameter, typeof(object));
    var body = Expression.Invoke(typeBasedExpr, cast);
    return Expression.Lambda<Func<T, object>>(body, parameter);
  }

  public static Func<T, bool> PrimaryKeyEqualsCompiled<T>(
    this DbContext context,
    string id)
  {
    var typeBasedFunc = context.PrimaryKeyEqualsCompiled(typeof(T), id);
    return entity => typeBasedFunc(entity!);
  }

  public static Expression<Func<T, bool>> PrimaryKeyEquals<T>(
    this DbContext context,
    string id)
  {
    var typeBasedExpr = context.PrimaryKeyEquals(typeof(T), id);
    var parameter = Expression.Parameter(typeof(T), "entity");
    var cast = Expression.Convert(parameter, typeof(object));
    var body = Expression.Invoke(typeBasedExpr, cast);
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  public static Func<T, bool> PrimaryKeyInCompiled<T>(
    this DbContext context,
    IEnumerable<string> ids)
  {
    var typeBasedFunc = context.PrimaryKeyInCompiled(typeof(T), ids);
    return entity => typeBasedFunc(entity!);
  }

  public static Expression<Func<T, bool>> PrimaryKeyIn<T>(
    this DbContext context,
    IEnumerable<string> ids)
  {
    var typeBasedExpr = context.PrimaryKeyIn(typeof(T), ids);
    var parameter = Expression.Parameter(typeof(T), "entity");
    var cast = Expression.Convert(parameter, typeof(object));
    var body = Expression.Invoke(typeBasedExpr, cast);
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  public static Func<object, object> PrimaryKeyOfCompiled(
    this DbContext context,
    Type entityType)
  {
    var key = (context.GetType(), entityType);

    if (_primaryKeyGetterCompiledCache.TryGetValue(key, out var cachedFunc))
    {
      return (Func<object, object>)cachedFunc;
    }

    var expr = context.PrimaryKeyOfUncached(entityType);
    var compiled = expr.Compile();
    _primaryKeyGetterCompiledCache[key] = compiled;

    return compiled;
  }

  public static Expression<Func<object, object>> PrimaryKeyOf(
    this DbContext context,
    Type entityType)
  {
    var key = (context.GetType(), entityType);

    if (_primaryKeyGetterExpressionCache.TryGetValue(key, out var cachedExpr))
    {
      return (Expression<Func<object, object>>)cachedExpr;
    }

    var expr = context.PrimaryKeyOfUncached(entityType);
    _primaryKeyGetterExpressionCache[key] = expr;

    return expr;
  }

  public static Func<object, bool> PrimaryKeyEqualsCompiled(
    this DbContext context,
    Type entityType,
    string id)
  {
    return context.PrimaryKeyEqualsUncached(entityType, id).Compile();
  }

  public static Expression<Func<object, bool>> PrimaryKeyEquals(
    this DbContext context,
    Type entityType,
    string id)
  {
    return context.PrimaryKeyEqualsUncached(entityType, id);
  }

  public static Func<object, bool> PrimaryKeyInCompiled(
    this DbContext context,
    Type entityType,
    IEnumerable<string> ids)
  {
    return context.PrimaryKeyInUncached(entityType, ids).Compile();
  }

  public static Expression<Func<object, bool>> PrimaryKeyIn(
    this DbContext context,
    Type entityType,
    IEnumerable<string> ids)
  {
    return context.PrimaryKeyInUncached(entityType, ids);
  }

  private static Expression<Func<object, object>> PrimaryKeyOfUncached(
    this DbContext context,
    Type entityType)
  {
    var keyProperties = context.GetPrimaryKeyProperties(entityType);
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

  private static Expression<Func<object, bool>> PrimaryKeyEqualsUncached(
    this DbContext context,
    Type entityType,
    string id)
  {
    var keyProperties = context.GetPrimaryKeyProperties(entityType);
    var idParts = id.Split(DataDbContext.KeyJoin);

    if (keyProperties.Count != idParts.Length)
    {
      throw new ArgumentException(
        "The number of ids must match the number of key properties.");
    }

    var parameter = Expression.Parameter(typeof(object));
    var convertedParameter = Expression.Convert(parameter, entityType);

    Expression? equalityExpression = null;

    foreach (var (property, idValue) in keyProperties.Zip(idParts))
    {
      var propertyExpression =
        property.PropertyInfo is { } propertyInfo
          ? Expression.Property(convertedParameter, propertyInfo)
          : Expression.Field(
            convertedParameter, property.FieldInfo
            ?? throw new InvalidOperationException(
              $"No field info found for {property}"));
      var convertedId = Expression.Constant(
        Convert.ChangeType(idValue, property.ClrType));

      var equalsExpression = Expression.Equal(propertyExpression, convertedId);

      equalityExpression = equalityExpression == null
        ? equalsExpression
        : Expression.AndAlso(equalityExpression, equalsExpression);
    }

    return Expression.Lambda<Func<object, bool>>(
      equalityExpression!, parameter);
  }

  private static Expression<Func<object, bool>> PrimaryKeyInUncached(
    this DbContext context,
    Type entityType,
    IEnumerable<string> ids)
  {
    var keyProperties = context.GetPrimaryKeyProperties(entityType);
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

      foreach (var (property, idPart) in keyProperties.Zip(idParts))
      {
        var propertyExpression =
          property.PropertyInfo is { } propertyInfo
            ? Expression.Property(convertedParameter, propertyInfo)
            : Expression.Field(
              convertedParameter, property.FieldInfo
              ?? throw new InvalidOperationException(
                $"No field info found for {property}"));
        var convertedIdPart = Expression.Constant(
          Convert.ChangeType(idPart, property.ClrType));

        var equalsExpression =
          Expression.Equal(propertyExpression, convertedIdPart);

        keyMatchExpression = keyMatchExpression == null
          ? equalsExpression
          : Expression.AndAlso(keyMatchExpression, equalsExpression);
      }

      idExpressions.Add(keyMatchExpression!);
    }

    var finalOrExpression = idExpressions.Count == 0
      ? Expression.Constant(false)
      : idExpressions.Aggregate(Expression.OrElse);

    return Expression.Lambda<Func<object, bool>>(finalOrExpression, parameter);
  }

  private static IReadOnlyList<IProperty> GetPrimaryKeyProperties(
    this DbContext context,
    Type entityType)
  {
    var entityTypeInfo = context.Model.FindEntityType(entityType)
      ?? throw new InvalidOperationException(
        $"No entity type found for {entityType}");
    var key = entityTypeInfo.FindPrimaryKey() ??
      throw new InvalidOperationException(
        $"No primary key found for {entityType}");
    return key.Properties;
  }
}
