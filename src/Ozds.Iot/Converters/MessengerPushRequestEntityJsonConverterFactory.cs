using System.Text.Json;
using System.Text.Json.Serialization;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Converters;

public class MessengerPushRequestEntityConverterFactory : JsonConverterFactory
{
  public override bool CanConvert(Type typeToConvert)
  {
    return typeof(IMessengerPushRequestEntity).IsAssignableFrom(typeToConvert);
  }

  public override JsonConverter? CreateConverter(
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    return (JsonConverter?)Activator.CreateInstance(
      typeof(MessengerPushRequestEntityJsonConverter<>)
        .MakeGenericType(typeToConvert));
  }
}
