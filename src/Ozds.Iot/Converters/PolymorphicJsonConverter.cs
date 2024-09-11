using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ozds.Iot.Converters;

public class PolymorphicJsonConverter<T>
  : JsonConverter<T>
{
  public override bool CanConvert(Type typeToConvert)
  {
    return typeof(T).IsAssignableFrom(typeToConvert);
  }

  public override T? Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var concrete = typeof(T).Assembly
      .GetTypes()
      .Where(type =>
        type.IsClass &&
        !type.IsAbstract &&
        !type.IsGenericType &&
        type.IsAssignableFrom(typeof(T)));

    foreach (var type in concrete)
    {
      try
      {
        return (T?)JsonSerializer.Deserialize(ref reader, type);
      }
      catch (JsonException)
      {
      }
    }

    throw new JsonException();
  }


  public override void Write(
    Utf8JsonWriter writer,
    T value,
    JsonSerializerOptions options
  )
  {
  }
}
