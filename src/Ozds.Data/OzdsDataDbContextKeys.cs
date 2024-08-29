using System.Linq.Expressions;

// TODO: through model
// TODO: cache expressions/compilations
// TODO: check if conversion to string is needed for all properties
// TODO: remove coupling with KeyJoin

namespace Ozds.Data;

public partial class OzdsDataDbContext
{
  public const string KeyJoin = "@";

  public Func<T, string> PrimaryKeyOfCompiled<T>()
  {
    return PrimaryKeyOf<T>().Compile();
  }

  public Expression<Func<T, string>> PrimaryKeyOf<T>()
  {
    var original = PrimaryKeyOfAgnostic(typeof(T));
    var parameter = Expression.Parameter(typeof(T));
    return Expression.Lambda<Func<T, string>>(
      Expression.Invoke(
        original,
        Expression.Convert(parameter, typeof(object))),
      parameter
    );
  }

  public Func<object, string> PrimaryKeyOfAgnosticCompiled(Type type)
  {
    return PrimaryKeyOfAgnostic(type).Compile();
  }

  public Expression<Func<object, string>> PrimaryKeyOfAgnostic(Type type)
  {
    var entityType = Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}");
    var key = entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {type}");
    var properties = key.Properties;
    var objectParameter = Expression.Parameter(typeof(object));
    var parameter = Expression.Convert(objectParameter, type);
    var propertyExpressions = properties
      .Select(
        property =>
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
          Expression.Convert(propertyExpression, typeof(object)),
          typeof(string));
      return Expression.Lambda<Func<object, string>>(
        propertyToStringExpression, objectParameter);
    }

    var stringJoinWithDashExpression = Expression.Call(
      typeof(string).GetMethod(
        nameof(string.Join),
        [typeof(string), typeof(string[])]) ??
      throw new InvalidOperationException(
        $"No {nameof(string.Join)} method found in {typeof(string)}"),
      Expression.Constant(KeyJoin),
      Expression.NewArrayInit(typeof(string), propertyExpressions));

    return Expression.Lambda<Func<object, string>>(
      stringJoinWithDashExpression, objectParameter);
  }

  public Func<T, string> PrimaryKeyOfCompiled<T, TConverted>(
    Expression<Func<T, TConverted>> prefix)
  {
    return PrimaryKeyOf(prefix).Compile();
  }

  public Expression<Func<T, string>> PrimaryKeyOf<T, TConverted>(
    Expression<Func<T, TConverted>> prefix)
  {
    var parameter = Expression.Parameter(typeof(T));
    var callExpression = Expression.Invoke(prefix, parameter);
    var primaryKeyExpression = PrimaryKeyOf<TConverted>();
    return Expression.Lambda<Func<T, string>>(
      Expression.Invoke(primaryKeyExpression, callExpression), parameter);
  }

  public Func<T, bool> PrimaryKeyEqualsCompiled<T>(params string[] ids)
  {
    return PrimaryKeyEquals<T>(ids).Compile();
  }

  public Expression<Func<T, bool>> PrimaryKeyEquals<T>(params string[] ids)
  {
    var original = PrimaryKeyEqualsAgnostic(typeof(T), ids);
    var parameter = Expression.Parameter(typeof(T));
    return Expression.Lambda<Func<T, bool>>(
      Expression.Invoke(
        original,
        Expression.Convert(parameter, typeof(object))),
      parameter
    );
  }

  public Func<object, bool> PrimaryKeyEqualsAgnosticCompiled(
    Type type,
    params string[] ids)
  {
    return PrimaryKeyEqualsAgnostic(type, ids).Compile();
  }

  public Expression<Func<object, bool>> PrimaryKeyEqualsAgnostic(
    Type type,
    params string[] ids)
  {
    var objectParameter = Expression.Parameter(typeof(object));
    var parameter = Expression.Convert(objectParameter, type);
    var primaryKeyExpression = PrimaryKeyOfAgnostic(type);
    var primaryKeyEqualsExpression = Expression.Equal(
      Expression.Invoke(primaryKeyExpression, parameter),
      Expression.Constant(string.Join(KeyJoin, ids)));
    return Expression.Lambda<Func<object, bool>>(
      primaryKeyEqualsExpression,
      objectParameter
    );
  }

  public Func<T, bool> PrimaryKeyInCompiled<T>(ICollection<string> ids)
  {
    return PrimaryKeyIn<T>(ids).Compile();
  }

  public Expression<Func<T, bool>> PrimaryKeyIn<T>(ICollection<string> ids)
  {
    var original = PrimaryKeyInAgnostic(typeof(T), ids);
    var parameter = Expression.Parameter(typeof(T));
    return Expression.Lambda<Func<T, bool>>(
      Expression.Invoke(
        original,
        Expression.Convert(parameter, typeof(object))),
      parameter
    );
  }

  public Func<object, bool> PrimaryKeyInAgnosticCompiled(
    Type type,
    ICollection<string> ids)
  {
    return PrimaryKeyInAgnostic(type, ids).Compile();
  }

  public Expression<Func<object, bool>> PrimaryKeyInAgnostic(
    Type type,
    ICollection<string> ids)
  {
    var objectParameter = Expression.Parameter(typeof(object));
    var parameter = Expression.Convert(objectParameter, type);
    var primaryKeyExpression = PrimaryKeyOfAgnostic(type);
    var primaryKeyInExpression = Expression.Call(
      Expression.Constant(ids),
      typeof(ICollection<string>).GetMethod(
        nameof(ICollection<string>.Contains)) ??
      throw new InvalidOperationException(
        $"No {
          nameof(ICollection<string>.Contains)
        } method found in {
          typeof(ICollection<string>)
        }"),
      Expression.Invoke(primaryKeyExpression, parameter));
    return Expression.Lambda<Func<object, bool>>(
      primaryKeyInExpression,
      objectParameter
    );
  }

  public Func<T, string> ForeignKeyOfCompiled<T>(string property)
  {
    return ForeignKeyOf<T>(property).Compile();
  }

  public Expression<Func<T, string>> ForeignKeyOf<T>(string property)
  {
    var original = ForeignKeyOfAgnostic(typeof(T), property);
    var parameter = Expression.Parameter(typeof(T));
    return Expression.Lambda<Func<T, string>>(
      Expression.Invoke(
        original,
        Expression.Convert(parameter, typeof(object))),
      parameter
    );
  }

  public Func<T, bool> ForeignKeyEqualsCompiled<T>(
    string property,
    params string[] ids)
  {
    return ForeignKeyEquals<T>(property, ids).Compile();
  }

  public Expression<Func<T, bool>> ForeignKeyEquals<T>(
    string property,
    params string[] ids)
  {
    var original = ForeignKeyEqualsAgnostic(typeof(T), property, ids);
    var parameter = Expression.Parameter(typeof(T));
    return Expression.Lambda<Func<T, bool>>(
      Expression.Invoke(
        original,
        Expression.Convert(parameter, typeof(object))),
      parameter
    );
  }

  public Func<object, bool> ForeignKeyEqualsAgnosticCompiled(
    Type type,
    string property,
    params string[] ids)
  {
    return ForeignKeyEqualsAgnostic(type, property, ids).Compile();
  }

  public Expression<Func<object, bool>> ForeignKeyEqualsAgnostic(
    Type type,
    string property,
    params string[] ids)
  {
    var objectParameter = Expression.Parameter(typeof(object));
    var parameter = Expression.Convert(objectParameter, type);
    var foreignKeyExpression = ForeignKeyOfAgnostic(type, property);
    var foreignKeyEqualsExpression = Expression.Equal(
      Expression.Invoke(foreignKeyExpression, parameter),
      Expression.Constant(string.Join(KeyJoin, ids)));
    return Expression.Lambda<Func<object, bool>>(
      foreignKeyEqualsExpression,
      objectParameter
    );
  }

  public Func<object, string> ForeignKeyOfAgnosticCompiled(
    Type type,
    string property)
  {
    return ForeignKeyOfAgnostic(type, property).Compile();
  }

  public Expression<Func<object, string>> ForeignKeyOfAgnostic(
    Type type,
    string property)
  {
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
    var objectParameter = Expression.Parameter(typeof(object));
    var parameter = Expression.Convert(objectParameter, type);
    var propertyExpressions = properties
      .Select(
        property =>
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
          Expression.Convert(propertyExpression, typeof(object)),
          typeof(string));
      return Expression.Lambda<Func<object, string>>(
        propertyToStringExpression, objectParameter);
    }

    var stringJoinWithDashExpression = Expression.Call(
      typeof(string).GetMethod(
        nameof(string.Join),
        [typeof(string), typeof(string[])]) ??
      throw new InvalidOperationException(
        $"No {nameof(string.Join)} method found in {typeof(string)}"),
      Expression.Constant(KeyJoin),
      Expression.NewArrayInit(typeof(string), propertyExpressions));
    return Expression.Lambda<Func<object, string>>(
      stringJoinWithDashExpression, objectParameter);
  }

  public Func<T, string> ForeignKeyOfCompiled<T, TConverted>(
    Expression<Func<T, TConverted>> prefix,
    string property)
  {
    return ForeignKeyOf(prefix, property).Compile();
  }

  public Expression<Func<T, string>> ForeignKeyOf<T, TConverted>(
    Expression<Func<T, TConverted>> prefix,
    string property)
  {
    var original = ForeignKeyOfAgnostic(prefix, property);
    var parameter = Expression.Parameter(typeof(T));
    return Expression.Lambda<Func<T, string>>(
      Expression.Invoke(
        original,
        Expression.Convert(parameter, typeof(object))),
      parameter
    );
  }

  public Func<object, string> ForeignKeyOfAgnosticCompiled<T, TConverted>(
    Expression<Func<T, TConverted>> prefix,
    string property
  )
  {
    return ForeignKeyOfAgnostic(prefix, property).Compile();
  }

  public Expression<Func<object, string>> ForeignKeyOfAgnostic<T, TConverted>(
    Expression<Func<T, TConverted>> prefix,
    string property
  )
  {
    var objectParameter = Expression.Parameter(typeof(object));
    var parameter = Expression.Convert(objectParameter, typeof(T));
    var callExpression = Expression.Invoke(prefix, parameter);
    var foreignKeyExpression = ForeignKeyOf<TConverted>(property);
    return Expression.Lambda<Func<object, string>>(
      Expression.Invoke(foreignKeyExpression, callExpression),
      objectParameter
    );
  }
}
