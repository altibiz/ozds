using System.Text.Json.Serialization;
using Ozds.Iot.Converters;

namespace Ozds.Iot.Entities.Abstractions;

[JsonConverter(typeof(MessengerPushRequestEntityConverterFactory))]
public interface IMessengerPushRequestEntity
{
  public DateTimeOffset Timestamp { get; }

  public IEnumerable<IMeterPushRequestEntity> Measurements { get; }
}
