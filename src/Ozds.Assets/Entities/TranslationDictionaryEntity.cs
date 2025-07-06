using System.Collections.Concurrent;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Ozds.Assets.Extensions;
using Tomlyn;

// NOTE: \n is ok here because we don't want to confuse git

namespace Ozds.Assets.Entities;

public sealed class TranslationDictionaryEntity
{
  private static readonly Encoding Encoding = new UTF8Encoding(
    false,
    true
  );

  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

  private static readonly XmlWriterSettings XmlSettings = new()
  {
    Encoding = Encoding,
    Indent = true
  };

  private readonly ConcurrentDictionary<string, Item> items;

  private TranslationDictionaryEntity()
  {
    items = new ConcurrentDictionary<string, Item>();
  }

  private TranslationDictionaryEntity(
    ConcurrentDictionary<string, Item> dictionary
  )
  {
    items = dictionary;
  }

  private TranslationDictionaryEntity(
    TranslationDictionaryContent content
  )
  {
    items = ContentToDictionary(content);
  }

  public static TranslationDictionaryEntity Empty
  {
    get { return new TranslationDictionaryEntity(); }
  }

  public static TranslationDictionaryEntity FromJson(string json)
  {
    var content = JsonSerializer.Deserialize<
      TranslationDictionaryContent
    >(json) ?? throw new InvalidOperationException("Invalid JSON.");

    return new TranslationDictionaryEntity(content);
  }

  public string ToJson()
  {
    return JsonSerializer.Serialize(
      DictionaryToContent(items, Format.Json),
      JsonOptions
    );
  }

  public static TranslationDictionaryEntity FromToml(string toml)
  {
    var content = Toml.ToModel<TranslationDictionaryContent>(
      toml
    );

    return new TranslationDictionaryEntity(content);
  }

  public string ToToml()
  {
    return Toml.FromModel(DictionaryToContent(items, Format.Toml));
  }

  public static TranslationDictionaryEntity FromXml(string xml)
  {
    var serializer = new XmlSerializer(typeof(TranslationDictionaryContent));
    using var reader = new StringReader(xml);
    var content = serializer.Deserialize(reader)
        as TranslationDictionaryContent
      ?? throw new InvalidOperationException("Invalid XML.");
    return new TranslationDictionaryEntity(content);
  }

  public string ToXml()
  {
    var serializer = new XmlSerializer(typeof(TranslationDictionaryContent));
    var stringBuilder = new StringBuilder();
    using var xmlWriter = XmlWriter.Create(stringBuilder, XmlSettings);
    serializer.Serialize(xmlWriter, DictionaryToContent(items, Format.Xml));
    xmlWriter.Flush();
    return stringBuilder.ToString();
  }

  public static TranslationDictionaryEntity FromString(string text, string path)
  {
    if (path.EndsWith(".json"))
    {
      return FromJson(text);
    }

    if (path.EndsWith(".xml"))
    {
      return FromXml(text);
    }

    if (path.EndsWith(".toml"))
    {
      return FromToml(text);
    }

    throw new InvalidOperationException("Invalid file extension.");
  }

  public string ToString(string path)
  {
    if (path.EndsWith(".json"))
    {
      return ToJson();
    }

    if (path.EndsWith(".xml"))
    {
      return ToXml();
    }

    if (path.EndsWith(".toml"))
    {
      return ToToml();
    }

    throw new InvalidOperationException("Invalid file extension.");
  }

  public static TranslationDictionaryEntity FromDictionary(
    Dictionary<string, string> dictionary
  )
  {
    return new TranslationDictionaryEntity(
      new ConcurrentDictionary<string, Item>(
        dictionary.ToDictionary(
          x => x.Key,
          x => new Item(string.Empty, x.Value))));
  }

  public Dictionary<string, string> ToDictionary()
  {
    return items.ToDictionary(
      x => x.Key,
      x => x.Value.Value
    );
  }

  public List<EnumerationItem> ToList()
  {
    return items
      .Select(x => new EnumerationItem(x.Key, x.Value.Metadata, x.Value.Value))
      .ToList();
  }

  public static async Task<TranslationDictionaryEntity> Load(
    string path,
    CancellationToken cancellationToken
  )
  {
    if (!File.Exists(path))
    {
      return new TranslationDictionaryEntity();
    }

    var text = await File.ReadAllTextAsync(path, Encoding, cancellationToken);

    return FromString(text, path);
  }

  public Task Save(
    string path,
    CancellationToken cancellationToken
  )
  {
    var text = ToString(path);

    return File.WriteAllTextAsync(
      path,
      text,
      Encoding,
      cancellationToken
    );
  }

  public string? Get(string key)
  {
    return items.GetValueOrDefault(key)?.Value;
  }

  public bool Contains(string key)
  {
    return items.Any(item => item.Key == key);
  }

  public void Add(string key, string value)
  {
    items.TryAdd(key, new Item(null, value));
  }

  public void Add(string key, string? metadata, string value)
  {
    items.TryAdd(key, new Item(metadata, value));
  }

  public void AddOrUpdate(string key, string value)
  {
    items.AddOrUpdate(
      key,
      _ => new Item(null, value),
      (_, _) => new Item(null, value));
  }

  public void AddOrUpdate(string key, string? metadata, string value)
  {
    items.AddOrUpdate(
      key,
      _ => new Item(metadata, value),
      (_, _) => new Item(metadata, value));
  }

  public string? Remove(string key)
  {
    if (!items.TryRemove(key, out var removed))
    {
      return null;
    }

    return removed.Value;
  }

  private static TranslationDictionaryContent DictionaryToContent(
    ConcurrentDictionary<string, Item> dictionary,
    Format format
  )
  {
    return new TranslationDictionaryContent
    {
      Translations = dictionary
        .OrderBy(item => item.Key)
        .Select(
          item => new TranslationDictionaryItem
          {
            Key = PrettyKeyValue(item.Key, format),
            Metadata = item.Value.Metadata is { } metadata
              ? PrettyMetadata(metadata, format)
              : null,
            Value = PrettyKeyValue(item.Value.Value, format)
          })
        .ToList()
    };
  }

  private static ConcurrentDictionary<string, Item>
    ContentToDictionary(TranslationDictionaryContent content)
  {
    return new ConcurrentDictionary<string, Item>(
      content.Translations
        .Select(item =>
          new KeyValuePair<string, Item>(
            item.Key.TrimWords(),
            new Item(
              item.Metadata?.Trim().Dedent(8, "\n"),
              item.Value.TrimWords())))
        .DistinctBy(item => item.Key)
    );
  }

  private static string PrettyKeyValue(
    string value,
    Format format
  )
  {
    if (format is Format.Json)
    {
      return value.TrimWords();
    }

    if (format is Format.Xml)
    {
      var prettyValue = value.TrimWords().Wrap(80 - 8).Indent(8, "\n");
      return $"\n{prettyValue}\n{new string(' ', 6)}";
    }

    if (format is Format.Toml)
    {
      var prettyValue = value.TrimWords().Wrap();
      return $"\n{prettyValue}\n";
    }

    throw new InvalidOperationException("Invalid file extension.");
  }

  private static string PrettyMetadata(
    string metadata,
    Format format
  )
  {
    if (format is Format.Json)
    {
      return metadata.TrimWords();
    }

    if (format is Format.Xml)
    {
      var prettyValue = metadata.Indent(8, "\n");
      return $"\n{prettyValue}\n{new string(' ', 6)}";
    }

    if (format is Format.Toml)
    {
      return $"\n{metadata}\n";
    }

    throw new InvalidOperationException("Invalid file extension.");
  }

  private enum Format
  {
    Json,
    Xml,
    Toml
  }

  public sealed record EnumerationItem(string Key, string? Metadata, string Value);

  private sealed record Item(string? Metadata, string Value);
}

// NOTE: public because of XML serialization
public sealed class TranslationDictionaryContent
{
  public List<TranslationDictionaryItem> Translations { get; set; } = default!;
}

// NOTE: public because of XML serialization
public sealed class TranslationDictionaryItem
{
  public string Key { get; set; } = default!;

  public string? Metadata { get; set; }

  public string Value { get; set; } = default!;
}
