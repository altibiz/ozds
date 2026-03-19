using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Report.Serialization.Abstractions;

// NOTE: \n is ok here because we're creating a CSV file
// which is going to depend on an unknown platform anyway

namespace Ozds.Report.Serialization.Implementations;

public class CsvSerialization(IServiceProvider serviceProvider)
  : IExporter,
    IImporter
{
  private const char Separator = ',';

  private const char Newline = '\n';

  string IExporter.Extension
  {
    get { return "csv"; }
  }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public async Task<string> Export<T>(
    CultureInfo culture,
    IEnumerable<T> models,
    CancellationToken cancellationToken
  )
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
    var localizationQueries =
      serviceProvider.GetRequiredService<ILocalizationQueries>();
    var stringBuilder = new StringBuilder();

    Type type;
    var properties = new List<PropertyInfo>();
    foreach (
      var (model, index) in models
        .Where(x => x is not null)
        .Select((x, i) => (x, i))
    )
    {
      if (index == 0)
      {
        type = model!.GetType();
        properties = type.GetProperties().ToList();

        foreach (var property in properties.Select(property => property.Name))
        {
          var translation = localizationQueries.Translate(
            culture,
            type,
            property
          );
          stringBuilder.Append(translation);
          stringBuilder.Append(Separator);
        }

        if (stringBuilder.Length > 0)
        {
          stringBuilder.Remove(stringBuilder.Length - 1, 1);
          stringBuilder.Append(Newline);
        }
      }

      foreach (var property in properties)
      {
        var value = property.GetValue(model);
        if (value is string)
        {
          stringBuilder.Append($"\"{value}\"");
        }
        else
        {
          stringBuilder.Append(value);
        }

        stringBuilder.Append(Separator);
      }

      if (stringBuilder.Length > 0)
      {
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(Newline);
      }
    }

    if (stringBuilder.Length > 0)
    {
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    }

    return stringBuilder.ToString();
  }

  public async Task<string> Export<T>(
    CultureInfo culture,
    IAsyncEnumerable<T> models,
    CancellationToken cancellationToken
  )
  {
    var localizationQueries =
      serviceProvider.GetRequiredService<ILocalizationQueries>();
    var stringBuilder = new StringBuilder();

    Type type;
    var properties = new List<PropertyInfo>();
    await foreach (
      var (model, index) in models
        .Where(x => x is not null)
        .Select((x, i) => (x, i))
    )
    {
      if (index == 0)
      {
        type = model!.GetType();
        properties = type.GetProperties().ToList();

        foreach (var property in properties.Select(property => property.Name))
        {
          var translation = localizationQueries.Translate(
            culture,
            type,
            property
          );
          stringBuilder.Append(translation);
          stringBuilder.Append(Separator);
        }

        if (stringBuilder.Length > 0)
        {
          stringBuilder.Remove(stringBuilder.Length - 1, 1);
          stringBuilder.Append(Newline);
        }
      }

      foreach (var property in properties)
      {
        var value = property.GetValue(model);
        stringBuilder.Append(value);
        stringBuilder.Append(Separator);
      }

      if (stringBuilder.Length > 0)
      {
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(Newline);
      }
    }

    if (stringBuilder.Length > 0)
    {
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    }

    return stringBuilder.ToString();
  }

  string IImporter.Extension
  {
    get { return "csv"; }
  }

  public IImportStreamer<T> Import<T>(CultureInfo culture, Stream csvStream)
  {
    var streamReader = new StreamReader(csvStream);
    var config = new CsvConfiguration(CultureInfo.InvariantCulture);
    var csvReader = new CsvReader(streamReader, config);
    return new CsvImportStreamer<T>(
      serviceProvider,
      culture,
      streamReader,
      csvReader
    );
  }

  public IImportStreamer Import(
    CultureInfo culture,
    Type type,
    Stream csvStream
  )
  {
    var streamReader = new StreamReader(csvStream);
    var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    return new CsvImportStreamer(
      serviceProvider,
      culture,
      type,
      streamReader,
      csvReader
    );
  }

  public IAsyncImportStreamer<T> Import<T>(
    CultureInfo culture,
    Stream csvStream,
    CancellationToken cancellationToken
  )
  {
    var streamReader = new StreamReader(csvStream);
    var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    return new CsvAsyncImportStreamer<T>(
      serviceProvider,
      culture,
      streamReader,
      csvReader,
      cancellationToken
    );
  }

  public IAsyncImportStreamer Import(
    CultureInfo culture,
    Type type,
    Stream csvStream,
    CancellationToken cancellationToken
  )
  {
    var streamReader = new StreamReader(csvStream);
    var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    return new CsvAsyncImportStreamer(
      serviceProvider,
      culture,
      type,
      streamReader,
      csvReader,
      cancellationToken
    );
  }
}

internal sealed class CsvAsyncImportStreamer(
  IServiceProvider serviceProvider,
  CultureInfo culture,
  Type type,
  StreamReader streamReader,
  CsvReader reader,
  CancellationToken cancellationToken
) : IAsyncImportStreamer
{
  public IAsyncEnumerable<object> Stream()
  {
    return reader
      .Register(type, serviceProvider, culture)
      .GetRecordsAsync(type, cancellationToken);
  }

  public void Dispose()
  {
    streamReader.Dispose();
    reader.Dispose();
  }
}

internal sealed class CsvAsyncImportStreamer<T>(
  IServiceProvider serviceProvider,
  CultureInfo culture,
  StreamReader streamReader,
  CsvReader reader,
  CancellationToken cancellationToken
) : IAsyncImportStreamer<T>
{
  public IAsyncEnumerable<T> Stream()
  {
    return reader
      .Register<T>(serviceProvider, culture)
      .GetRecordsAsync<T>(cancellationToken);
  }

  public void Dispose()
  {
    streamReader.Dispose();
    reader.Dispose();
  }
}

internal sealed class CsvImportStreamer(
  IServiceProvider serviceProvider,
  CultureInfo culture,
  Type type,
  StreamReader streamReader,
  CsvReader reader
) : IImportStreamer
{
  public IEnumerable<object> Stream()
  {
    return reader.Register(type, serviceProvider, culture).GetRecords(type);
  }

  public void Dispose()
  {
    streamReader.Dispose();
    reader.Dispose();
  }
}

internal sealed class CsvImportStreamer<T>(
  IServiceProvider serviceProvider,
  CultureInfo culture,
  StreamReader streamReader,
  CsvReader reader
) : IImportStreamer<T>
{
  public IEnumerable<T> Stream()
  {
    return reader.Register<T>(serviceProvider, culture).GetRecords<T>();
  }

  public void Dispose()
  {
    streamReader.Dispose();
    reader.Dispose();
  }
}

// NOTE: public because ObjectResolver
public sealed class EntityMap<T> : ClassMap<T>
{
  public EntityMap(IServiceProvider serviceProvider, CultureInfo culture)
  {
    var localizationQueries =
      serviceProvider.GetRequiredService<ILocalizationQueries>();

    var entityType = typeof(T);
    var entityProperties = entityType.GetProperties();
    foreach (var entityProperty in entityProperties)
    {
      var parameter = Expression.Parameter(entityType);
      var member = Expression.MakeMemberAccess(parameter, entityProperty);
      var cast = Expression.Convert(member, typeof(object));
      var expression = Expression.Lambda<Func<T, object>>(cast, parameter);
      var translation = localizationQueries.Translate(
        culture,
        entityType,
        entityProperty.Name
      );

      serviceProvider
        .GetRequiredService<ILogger<EntityMap<T>>>()
        .LogDebug("Mapped {Key} -> {Value}", entityProperty.Name, translation);
      Map(expression).Name(translation);
    }
  }
}

public static class EntityMapExtensions
{
  public static CsvReader Register(
    this CsvReader reader,
    Type type,
    IServiceProvider serviceProvider,
    CultureInfo culture
  )
  {
    var entityMapType = typeof(EntityMap<>).MakeGenericType(type);
    var map = (ClassMap)
      ObjectResolver.Current.Resolve(entityMapType, serviceProvider, culture);
    (
      serviceProvider.GetRequiredService(
        typeof(ILogger<>).MakeGenericType(entityMapType)
      ) as ILogger
    )!.LogDebug("Mapped {Map}", map);
    reader.Context.RegisterClassMap(map);
    return reader;
  }

  public static CsvReader Register<T>(
    this CsvReader reader,
    IServiceProvider serviceProvider,
    CultureInfo culture
  )
  {
    var map = ObjectResolver.Current.Resolve<EntityMap<T>>(
      serviceProvider,
      culture
    );
    serviceProvider
      .GetRequiredService<ILogger<EntityMap<T>>>()
      .LogDebug("Mapped {Map}", map);
    reader.Context.RegisterClassMap(map);
    return reader;
  }
}
