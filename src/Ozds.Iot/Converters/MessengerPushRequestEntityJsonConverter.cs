using System.Text.Json;
using System.Text.Json.Serialization;
using Ozds.Iot.Entities;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Converters;

public class MessengerPushRequestEntityJsonConverter<T>
  : JsonConverter<T>
  where T : IMessengerPushRequestEntity
{
  public override T? Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    return (T?)JsonSerializer.Deserialize(
      ref reader,
      typeof(PidgeonPushRequestEntity),
      options
    );
  }

  public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
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
