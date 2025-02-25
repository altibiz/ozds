using System.Collections.Concurrent;
using Ozds.Client.Conversion.Abstractions;

namespace Ozds.Client.Conversion;

public class ModelRecordConverter(IServiceProvider serviceProvider)
{
  private readonly
    ConcurrentDictionary<Type, IModelRecordConverter> modelCache = new();

  private readonly
    ConcurrentDictionary<Type, IModelRecordConverter> recordCache = new();

  private readonly ConcurrentDictionary<Type, List<Type>> subtypeCache = new();

  public TRecord ToRecord<TRecord>(object model)
  {
    return (TRecord)ToRecord(model);
  }

  public TModel ToModel<TModel>(object record)
  {
    return (TModel)ToModel(record);
  }

  public object ToModel(object record)
  {
    if (modelCache.TryGetValue(record.GetType(), out var converter))
    {
      return converter.ToModel(record);
    }

    converter = FindModelConverter(record.GetType());

    modelCache.TryAdd(record.GetType(), converter);

    if (!converter.CanConvertToModel(record.GetType()))
    {
      throw new InvalidOperationException(
        $"No model converter found for record {record.GetType()}.");
    }

    return converter.ToModel(record);
  }

  public Type RecordType(Type type)
  {
    if (recordCache.TryGetValue(type, out var converter))
    {
      return converter.RecordType;
    }

    converter = FindRecordConverter(type);

    recordCache.TryAdd(type, converter);

    return converter.RecordType;
  }

  public Type ModelType(Type type)
  {
    if (modelCache.TryGetValue(type, out var converter))
    {
      return converter.ModelType;
    }

    converter = FindModelConverter(type);

    modelCache.TryAdd(type, converter);

    return converter.ModelType;
  }

  public List<Type> ConvertableRecordSubtypes(Type type)
  {
    if (subtypeCache.TryGetValue(type, out var subtypes))
    {
      return subtypes;
    }

    subtypes = serviceProvider
      .GetServices<IModelRecordConverter>()
      .Where(
        converter =>
          !converter.RecordType.IsAbstract
          && !converter.RecordType.IsInterface
          && converter.RecordType.IsAssignableTo(type))
      .Select(converter => converter.RecordType)
      .ToList();

    subtypes = subtypes
      .Where(subtype => !subtypes.Exists(type => type.BaseType == subtype))
      .ToList();

    subtypeCache.TryAdd(type, subtypes);

    return subtypes;
  }

  public object ToRecord(object model)
  {
    if (recordCache.TryGetValue(model.GetType(), out var converter))
    {
      return converter.ToRecord(model);
    }

    converter = FindRecordConverter(model.GetType());

    recordCache.TryAdd(model.GetType(), converter);

    if (!converter.CanConvertToRecord(model.GetType()))
    {
      throw new InvalidOperationException(
        $"No record converter found for model {model.GetType()}.");
    }

    return converter.ToRecord(model);
  }

  private IModelRecordConverter FindRecordConverter(Type type)
  {
    return serviceProvider
        .GetServices<IModelRecordConverter>()
        .Where(converter => type.IsAssignableTo(converter.ModelType))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.ModelType.IsAssignableTo(acc.ModelType)
                ? next
                : acc)
      ?? serviceProvider
        .GetServices<IModelRecordConverter>()
        .Where(converter => converter.ModelType.IsAssignableTo(type))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.ModelType.IsAssignableTo(acc.ModelType)
                ? acc
                : next)
      ?? throw new InvalidOperationException(
        $"No converter found for model {type}.");
  }

  private IModelRecordConverter FindModelConverter(Type type)
  {
    return serviceProvider
        .GetServices<IModelRecordConverter>()
        .Where(converter => type.IsAssignableTo(converter.RecordType))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.RecordType.IsAssignableTo(acc.RecordType)
                ? next
                : acc)
      ?? serviceProvider
        .GetServices<IModelRecordConverter>()
        .Where(converter => converter.RecordType.IsAssignableTo(type))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.RecordType.IsAssignableTo(acc.RecordType)
                ? acc
                : next)
      ?? throw new InvalidOperationException(
        $"No converter found for record {type}.");
  }
}
