using System.Linq.Expressions;
using System.Reflection;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;

// TODO: cache compilations ?
// TODO: in queries check that the calling type is accurate

namespace Ozds.Data.Extensions;

public static class IQueryableExtensions
{
  public static IQueryable<T> WithId<T>(this IQueryable<T> query, string id) where T : IIdentifiableEntity =>
    query.Where(MakeWithIdExpression<T>(id));

  public static IEnumerable<T> WithId<T>(this IEnumerable<T> query, string id) where T : IIdentifiableEntity =>
    query.Where(MakeWithIdExpression<T>(id).Compile());

  public static IQueryable<T> WithIdFrom<T>(this IQueryable<T> query, ICollection<string> ids) where T : IIdentifiableEntity =>
    query.Where(MakeWithIdFromExpression<T>(ids));

  public static IEnumerable<T> WithIdFrom<T>(this IEnumerable<T> query, ICollection<string> ids) where T : IIdentifiableEntity =>
    query.Where(MakeWithIdFromExpression<T>(ids).Compile());

  private static Expression<Func<T, bool>> MakeWithIdExpression<T>(string id) where T : IIdentifiableEntity
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var hasStringId =
      type.IsAssignableTo(typeof(RepresentativeEntity))
      || type.IsAssignableTo(typeof(MessengerEntity))
      || type.IsAssignableTo(typeof(MeterEntity));
    var fieldName = hasStringId ? "_stringId" : "_id";
    var field = type
      .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
    var fieldExpression = Expression
      .Field(parameter, field
        ?? throw new InvalidOperationException($"No {fieldName} field found in {type}"));
    var constant = hasStringId
      ? Expression.Constant(id)
      : Expression.Constant(long.Parse(id));
    var body = Expression.Equal(fieldExpression, constant);
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }

  private static Expression<Func<T, bool>> MakeWithIdFromExpression<T>(ICollection<string> ids) where T : IIdentifiableEntity
  {
    var type = typeof(T);
    var parameter = Expression.Parameter(type);
    var hasStringId =
      type.IsAssignableTo(typeof(RepresentativeEntity))
      || type.IsAssignableTo(typeof(MessengerEntity))
      || type.IsAssignableTo(typeof(MeterEntity));
    var fieldName = hasStringId ? "_stringId" : "_id";
    var field = type
      .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
    var fieldExpression = Expression
      .Field(parameter, field
        ?? throw new InvalidOperationException($"No {fieldName} field found in {type}"));
    var constant = hasStringId
      ? Expression.Constant(ids)
      : Expression.Constant(ids.Select(long.Parse));
    var body = Expression.Call(
      constant,
      typeof(ICollection<string>)
        .GetMethod(nameof(ICollection<string>.Contains))!,
      fieldExpression
    );
    return Expression.Lambda<Func<T, bool>>(body, parameter);
  }
}
