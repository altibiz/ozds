using System.Text.Json;
using System.Text.Json.Serialization;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Converters;

public class
  PidgeonMeterPushRequestEntityConverterFactory : JsonConverterFactory
{
  public override bool CanConvert(Type typeToConvert)
  {
    return typeof(IPidgeonMeterPushRequestEntity).IsAssignableFrom(
      typeToConvert);
  }

  public override JsonConverter? CreateConverter(
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    return (JsonConverter?)Activator.CreateInstance(
      typeof(PidgeonMeterPushRequestEntityJsonConverter<>)
        .MakeGenericType(typeToConvert));
  }
}
