using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Dapper.SqlMapper;

namespace Ozds.Data.Extensions;

// TODO: generic type handlers for generic properties
// when https://github.com/DapperLib/Dapper/issues/1924

public static class DataDbContextDapperConnectionExtensions
{
  private static readonly SemaphoreSlim _lock = new(1, 1);

  private static bool _isRegistered;

  public static DbConnection GetDapperDbConnection(this DbContext context)
  {
    _lock.Wait();

    if (_isRegistered)
    {
      _lock.Release();
      return context.Database.GetDbConnection();
    }

    _isRegistered = true;

    foreach (var type in context.Model.GetEntityTypes())
    {
      SetTypeMap(
        type.ClrType,
        new CustomPropertyOrFieldTypeMap(
          type.ClrType,
          (type, columnName) =>
          {
            var property = GetMappedProperty(context, type, columnName);

            var info = (MemberInfo?)property?.PropertyInfo
              ?? property?.FieldInfo;

            return info;
          }));
    }

    var listOfEnumTypes = new HashSet<Type>();
    foreach (var type in context.Model.GetEntityTypes())
    {
      foreach (var propertyType in type
        .GetScalarPropertiesRecursive()
        .Select(x => x.ClrType))
      {
        if (propertyType.IsGenericType &&
          propertyType.GetGenericTypeDefinition() == typeof(List<>)
          && propertyType.GetGenericArguments()[0].IsEnum)
        {
          listOfEnumTypes.Add(propertyType.GetGenericArguments()[0]);
        }
      }
    }

    foreach (var type in listOfEnumTypes)
    {
      var listType = typeof(List<>).MakeGenericType(type);
      var handler = typeof(GenericListTypeHandler<>)
            .MakeGenericType(type)
            .GetConstructor(Array.Empty<Type>())
            ?.Invoke(Array.Empty<object>())
          as ITypeHandler
        ?? throw new InvalidOperationException(
          $"Handler construction failed for {listType}.");
      AddTypeHandler(listType, handler);
    }

    _lock.Release();

    return context.Database.GetDbConnection();
  }

  private static IProperty? GetMappedProperty(
    DbContext context,
    Type entityType,
    string columnName
  )
  {
    var entityTypeModel = context.Model.FindEntityType(entityType);
    var property = entityTypeModel
      ?.GetProperties()
      .FirstOrDefault(
        property => property
          .GetColumnName()
          .Equals(columnName, StringComparison.OrdinalIgnoreCase));
    return property;
  }

  private sealed class GenericListTypeHandler<T> : TypeHandler<List<T>>
  {
    public override List<T>? Parse(object value)
    {
      return (value as IEnumerable)?.OfType<T>().ToList();
    }

    public override void SetValue(IDbDataParameter parameter, List<T>? value)
    {
      parameter.Value = value?.ToArray();
    }
  }

  private sealed class CustomPropertyOrFieldTypeMap(
    Type type,
    Func<Type, string, MemberInfo?> selector) : ITypeMap
  {
    public ConstructorInfo? FindConstructor(string[] names, Type[] types)
    {
      return type.GetConstructor(Array.Empty<Type>());
    }

    public ConstructorInfo? FindExplicitConstructor()
    {
      return null;
    }

    public IMemberMap GetConstructorParameter(
      ConstructorInfo constructor,
      string columnName)
    {
      throw new NotSupportedException();
    }

    public IMemberMap? GetMember(string columnName)
    {
      var info = selector(type, columnName);
      return info switch
      {
        PropertyInfo propertyInfo =>
          new SimpleMemberMap(columnName, propertyInfo),
        FieldInfo fieldInfo => new SimpleMemberMap(columnName, fieldInfo),
        _ => null
      };
    }
  }

  private sealed class SimpleMemberMap : IMemberMap
  {
    public SimpleMemberMap(string columnName, PropertyInfo property)
    {
      ColumnName = columnName
        ?? throw new ArgumentNullException(nameof(columnName));
      Property = property ?? throw new ArgumentNullException(nameof(property));
    }

    public SimpleMemberMap(string columnName, FieldInfo field)
    {
      ColumnName = columnName
        ?? throw new ArgumentNullException(nameof(columnName));
      Field = field ?? throw new ArgumentNullException(nameof(field));
    }

    public string ColumnName { get; }

    public Type MemberType
    {
      get
      {
        object? obj = Field?.FieldType;
        if (obj == null)
        {
          obj = Property?.PropertyType;
          if (obj == null)
          {
            var parameter = Parameter;
            if (parameter == null)
            {
              return default!;
            }

            obj = parameter.ParameterType;
          }
        }

        return (Type)obj;
      }
    }

    public PropertyInfo? Property { get; }

    public FieldInfo? Field { get; }

    public ParameterInfo? Parameter { get; }
  }
}
