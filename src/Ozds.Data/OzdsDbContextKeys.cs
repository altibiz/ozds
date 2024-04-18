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
    return Expression.Lambda(fieldExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create primary key expression for {type}");
  }

  public Func<T, object> PrimaryKeyOfCompiled<T>(Expression<Func<T, object>> prefix, Type type)
  {
    return PrimaryKeyOf(prefix, type).Compile();
  }

  public Expression<Func<T, object>> PrimaryKeyOf<T>(Expression<Func<T, object>> prefix, Type type)
  {
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
    return Expression.Lambda(fieldExpression, parameter)
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
    var fieldName = $"_{char.ToLower(joinType.Name[0]) + joinType.Name[1..]}";
    var field = type.GetField(fieldName,
      BindingFlags.NonPublic | BindingFlags.Instance);
    var fieldExpression = Expression.Field(parameter, field
      ?? throw new InvalidOperationException(
        $"No {joinType} field found in {type}"));
    return Expression.Lambda(fieldExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create foreign key expression for {type}");
  }

  public Func<T, object> ForeignKeyOfCompiled<T>(Expression<Func<T, object>> prefix, Type joinType)
  {
    return ForeignKeyOf(prefix, joinType).Compile();
  }

  public Expression<Func<T, object>> ForeignKeyOf<T>(Expression<Func<T, object>> prefix, Type joinType)
  {
    var parameter = Expression.Parameter(typeof(T));
    var fieldName = $"_{char.ToLower(joinType.Name[0]) + joinType.Name[1..]}";
    var field = typeof(T).GetField(fieldName,
      BindingFlags.NonPublic | BindingFlags.Instance);
    var callExpression = Expression.Invoke(prefix, parameter);
    var fieldExpression = Expression.Field(callExpression, field
      ?? throw new InvalidOperationException(
        $"No {joinType} field found in {typeof(T)}"));
    return Expression.Lambda(fieldExpression, parameter)
      as Expression<Func<T, object>>
      ?? throw new InvalidOperationException(
        $"Failed to create foreign key expression for {typeof(T)}");
  }
}
