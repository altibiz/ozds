using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Entities.Enums;
using static Dapper.SqlMapper;

// TODO: complex properties
// try make the complex property a list of simple properties

// TODO: generic type handlers for generic properties
// when https://github.com/DapperLib/Dapper/issues/1924
// for now crawl over the model and find all list properties and add that way

namespace Ozds.Data.Extensions;

public static class DataDbContextDapperExtensions
{
  public static async Task<List<T>> DapperCommand<T>(
    this DbContext context,
    string sql,
    CancellationToken cancellationToken,
    object? parameters = null
  )
    where T : class
  {
    var connection = context.GetDapperDbConnection();
    var command = new CommandDefinition(
      sql,
      parameters,
      cancellationToken: cancellationToken
    );

    var results = await (MultiMapAsync
      .MakeGenericMethod(typeof(T))
      .Invoke(
        null,
        new object[]
        {
          connection,
          command,
          context.GetDapperTypes(typeof(T)),
          context.GetDapperMap<T>(),
          context.GetDapperSplitOn(typeof(T))
        }) as Task<IEnumerable<T>>)!;

    return results.ToList();
  }

  private static DbConnection GetDapperDbConnection(this DbContext context)
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

    AddTypeHandler(
      typeof(List<PhaseEntity>),
      new GenericListTypeHandler<PhaseEntity>()
    );

    _lock.Release();

    return context.Database.GetDbConnection();
  }

#pragma warning disable IDE0060 // Remove unused parameter
  private static Type[] GetDapperTypes(this DbContext context, Type type)
#pragma warning restore IDE0060 // Remove unused parameter
  {
    return type
      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
      .Select(property => property.PropertyType)
      .ToArray();
  }

  private static Func<object[], T> GetDapperMap<T>(
    this DbContext context
  )
    where T : class
  {
    return objects =>
    {
      var instance = Activator.CreateInstance(typeof(T))
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {typeof(T).Name}");

      foreach (var (property, @object) in typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Zip(objects))
      {
        property.SetValue(instance, @object);
      }

      return instance as T
        ?? throw new InvalidOperationException(
          $"Failed to cast instance to type {typeof(T).Name}");
    };
  }

  private static string GetDapperSplitOn(this DbContext context, Type type)
  {
    return string.Join(
      ",",
      type
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Select(property =>
        {
          var entityType = context.Model.FindEntityType(property.PropertyType)
            ?? throw new InvalidOperationException(
              $"Entity type not found for {property.PropertyType.Name}");

          var primaryKey = entityType.FindPrimaryKey()?.Properties[0]
            ?? throw new InvalidOperationException(
              $"Primary key not found for entity {entityType.ClrType.Name}");

          var columnName = primaryKey.GetColumnName();

          return columnName;
        })
    );
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
      .FirstOrDefault(property => property
        .GetColumnName()
        .Equals(columnName, StringComparison.OrdinalIgnoreCase));
    return property;
  }

  private static readonly SemaphoreSlim _lock = new(1, 1);

  private static readonly MethodInfo MultiMapAsync =
    typeof(SqlMapper)
      .GetMethods(
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
        BindingFlags.Static | BindingFlags.NonPublic
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
      )
      .FirstOrDefault(method =>
        method.Name == "MultiMapAsync" &&
        method.GetGenericArguments().Length == 1)
      ?? throw new InvalidOperationException("MultiMapAsync doesn't exist");

  private static bool _isRegistered = false;

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

    public IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
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
            ParameterInfo? parameter = Parameter;
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

    public SimpleMemberMap(string columnName, PropertyInfo property)
    {
      ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
      Property = property ?? throw new ArgumentNullException(nameof(property));
    }

    public SimpleMemberMap(string columnName, FieldInfo field)
    {
      ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
      Field = field ?? throw new ArgumentNullException(nameof(field));
    }
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
}
