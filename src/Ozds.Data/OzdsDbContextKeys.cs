using System.Linq.Expressions;
using System.Reflection;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

// TODO: through model
// TODO: cache expressions/compilations

namespace Ozds.Data;

public partial class OzdsDbContext
{
  public Func<T, object> PrimaryKeyOfCompiled<T>()
  {
    return PrimaryKeyOf<T>().Compile();
  }

  public Expression<Func<T, object>> PrimaryKeyOf<T>()
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var hasStringId =
      type.IsAssignableTo(typeof(RepresentativeEntity))
      || type.IsAssignableTo(typeof(MessengerEntity))
      || type.IsAssignableTo(typeof(MeterEntity));
    var fieldName = hasStringId ? "_stringId" : "_id";
    var field = type
      .GetField(fieldName,
        BindingFlags.NonPublic | BindingFlags.Instance |
        BindingFlags.FlattenHierarchy);
    var fieldExpression = Expression
      .Field(parameter, field
                        ?? throw new InvalidOperationException(
                          $"No {fieldName} field found in {type}"));
    var castExpression = Expression.Convert(fieldExpression, typeof(object));
    return Expression.Lambda(castExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create primary key expression for {type}");
  }

  public Func<T, object> PrimaryKeyOfCompiled<T, U>(Expression<Func<T, U>> prefix)
  {
    return PrimaryKeyOf(prefix).Compile();
  }

  public Expression<Func<T, object>> PrimaryKeyOf<T, U>(Expression<Func<T, U>> prefix)
  {
    var type = typeof(U);
    var parameter = Expression.Parameter(typeof(T));
    var hasStringId =
      type.IsAssignableTo(typeof(RepresentativeEntity))
      || type.IsAssignableTo(typeof(MessengerEntity))
      || type.IsAssignableTo(typeof(MeterEntity));
    var fieldName = hasStringId ? "_stringId" : "_id";
    var field = type
      .GetField(fieldName,
        BindingFlags.NonPublic | BindingFlags.Instance |
        BindingFlags.FlattenHierarchy);
    var callExpression = Expression.Invoke(prefix, parameter);
    var fieldExpression = Expression
      .Field(callExpression, field
                        ?? throw new InvalidOperationException(
                          $"No {fieldName} field found in {type}"));
    var castExpression = Expression.Convert(fieldExpression, typeof(object));
    return Expression.Lambda(castExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create primary key expression for {typeof(T)}");
  }

  public Func<T, bool> PrimaryKeyEqualsCompiled<T>(string id)
  {
    return PrimaryKeyEquals<T>(id).Compile();
  }

  public Expression<Func<T, bool>> PrimaryKeyEquals<T>(string id)
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var hasStringId =
      type.IsAssignableTo(typeof(RepresentativeEntity))
      || type.IsAssignableTo(typeof(MessengerEntity))
      || type.IsAssignableTo(typeof(MeterEntity));
    var fieldName = hasStringId ? "_stringId" : "_id";
    var field = type
      .GetField(fieldName,
        BindingFlags.NonPublic | BindingFlags.Instance |
        BindingFlags.FlattenHierarchy);
    var fieldExpression = Expression
      .Field(parameter, field
                        ?? throw new InvalidOperationException(
                          $"No {fieldName} field found in {type}"));
    var constant = hasStringId
      ? Expression.Constant(id)
      : Expression.Constant(long.Parse(id));
    var body = Expression.Equal(fieldExpression, constant);
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  public Func<T, bool> PrimaryKeyInCompiled<T>(ICollection<string> ids)
  {
    return PrimaryKeyIn<T>(ids).Compile();
  }

  public Expression<Func<T, bool>> PrimaryKeyIn<T>(ICollection<string> ids)
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var hasStringId =
      type.IsAssignableTo(typeof(RepresentativeEntity))
      || type.IsAssignableTo(typeof(MessengerEntity))
      || type.IsAssignableTo(typeof(MeterEntity));
    var fieldName = hasStringId ? "_stringId" : "_id";
    var field = type
      .GetField(fieldName,
        BindingFlags.NonPublic | BindingFlags.Instance |
        BindingFlags.FlattenHierarchy);
    var fieldExpression = Expression
      .Field(parameter, field
                        ?? throw new InvalidOperationException(
                          $"No {fieldName} field found in {type}"));
    var constants = hasStringId
      ? Expression.Constant(ids)
      : Expression.Constant(ids.Select(long.Parse));
    var body = Expression.Call(
      constants,
      typeof(ICollection<string>)
        .GetMethod(nameof(ICollection<string>.Contains))!,
      fieldExpression
    );
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  public Func<T, object> ForeignKeyOfCompiled<T>(Type joinType)
  {
    return ForeignKeyOf<T>(joinType).Compile();
  }

  public Expression<Func<T, object>> ForeignKeyOf<T>(Type joinType)
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var joinTypeName = joinType.Name.Replace("Entity", "");
    var fieldName = $"_{char.ToLower(joinTypeName[0]) + joinTypeName[1..]}Id";
    var field = type.GetField(fieldName,
      BindingFlags.NonPublic | BindingFlags.Instance);
    var propertyName = $"{joinTypeName}Id";
    var property = type.GetProperty(propertyName,
      BindingFlags.Public | BindingFlags.Instance);
    var memberExpression = field != null
      ? Expression.Field(parameter, field)
      : property != null
        ? Expression.Property(parameter, property)
        : throw new InvalidOperationException(
          $"No {fieldName} field found in {type}");
    var castExpression = Expression.Convert(memberExpression, typeof(object));
    return Expression.Lambda(castExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create foreign key expression for {type}");
  }

  public Func<T, object> ForeignKeyOfCompiled<T, U>(Expression<Func<T, U>> prefix, Type joinType)
  {
    return ForeignKeyOf(prefix, joinType).Compile();
  }

  public Expression<Func<T, object>> ForeignKeyOf<T, U>(Expression<Func<T, U>> prefix, Type joinType)
  {
    var parameter = Expression.Parameter(typeof(T));
    var callExpression = Expression.Invoke(prefix, parameter);
    var type = typeof(U);
    var joinTypeName = joinType.Name.Replace("Entity", "");
    var fieldName = $"_{char.ToLower(joinTypeName[0]) + joinTypeName[1..]}Id";
    var field = type.GetField(fieldName,
      BindingFlags.NonPublic | BindingFlags.Instance);
    var propertyName = $"{joinTypeName}Id";
    var property = type.GetProperty(propertyName,
      BindingFlags.Public | BindingFlags.Instance);
    var memberExpression = field != null
      ? Expression.Field(callExpression, field)
      : property != null
        ? Expression.Property(callExpression, property)
        : throw new InvalidOperationException(
          $"No {fieldName} field found in {type}");
    var castExpression = Expression.Convert(memberExpression, typeof(object));
    return Expression.Lambda(castExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create foreign key expression for {typeof(T)}");
  }
}
