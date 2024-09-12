using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ozds.Iot.Entities.Abstractions;

// TODO: get rid of ghosts

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
      var meterId = meterIdProp.GetString();

      if (meterId is not null)
      {
        var types = typeof(T).Assembly.GetTypes()
          .Where(t =>
            !t.IsAbstract
            && !t.IsGenericType
            && t.IsAssignableTo(typeof(IPidgeonMeterPushRequestEntity)));

        foreach (var type in types)
        {
          if (type
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(p => p.Name == "MeterIdPrefix")
            ?.GetValue(null) is not string meterIdPrefix
            || !meterId.StartsWith(meterIdPrefix))
          {
            continue;
          }

          return (T?)JsonSerializer.Deserialize(jsonObject, type, options);
        }
      }
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
}
