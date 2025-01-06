using System.Text.Json;
using System.Text.Json.Serialization;
using Ozds.Iot.Attributes;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Converters;

public class PidgeonMeterPushRequestEntityJsonConverter<T>
  : JsonConverter<T>
  where T : IPidgeonMeterPushRequestEntity
{
  public override T? Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    using var jsonDocument = JsonDocument.ParseValue(ref reader);

    var jsonObject = jsonDocument.RootElement;

    if (jsonObject.TryGetProperty("MeterId", out var meterIdProp) ||
      jsonObject.TryGetProperty("meterId", out meterIdProp))
    {
      var meterId = meterIdProp.GetString()
        ?? throw new JsonException(
          "Invalid JSON: MeterId not found or unrecognized prefix.");

      var type = GetTypeForMeterId(meterId)
        ?? throw new JsonException(
          "Invalid JSON: MeterId not found or unrecognized prefix.");

      return (T?)jsonObject.Deserialize(type, options);
    }

    throw new JsonException(
      "Invalid JSON: MeterId not found or unrecognized prefix.");
  }

  public override void Write(
    Utf8JsonWriter writer,
    T value,
    JsonSerializerOptions options
  )
  {
    var type = value?.GetType();
    if (type == null)
    {
      writer.WriteNullValue();
      return;
    }

    JsonSerializer.Serialize(writer, value, type, options);
  }

  private static Type? GetTypeForMeterId(string meterId)
  {
    var prefix = string.Join('-', meterId.Split('-').SkipLast(1));
    if (MeterIdPrefixAttribute.TypesByMeterIdPrefix
      .TryGetValue(prefix, out var existingType))
    {
      return existingType;
    }

    return null;
  }
}
