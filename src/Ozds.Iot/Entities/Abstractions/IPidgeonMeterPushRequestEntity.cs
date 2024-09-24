using System.Text.Json.Serialization;
using Ozds.Iot.Converters;

namespace Ozds.Iot.Entities.Abstractions;

[JsonConverter(typeof(PidgeonMeterPushRequestEntityConverterFactory))]
public interface IPidgeonMeterPushRequestEntity : IMeterPushRequestEntity
{
}
