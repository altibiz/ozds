using System.Collections.Concurrent;
using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming;

public class MessengerNamingConvention(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<string, IMessengerNamingConvention>
    idCache =
      new();

  private readonly ConcurrentDictionary<Type, IMessengerNamingConvention>
    typeCache =
      new();

  public Type MessengerTypeForMessengerId(string meterId)
  {
    return GetMessengerNamingConvention(meterId).MessengerType;
  }

  public string IdPrefixForMessengerType(Type meterType)
  {
    return GetMessengerNamingConvention(meterType).IdPrefix;
  }

  private IMessengerNamingConvention GetMessengerNamingConvention(
    string meterId
  )
  {
    var meterIdPrefix = string.Join('-', meterId.Split('-').SkipLast(1));

    if (idCache.TryGetValue(meterIdPrefix, out var meterNamingConvention))
    {
      return meterNamingConvention;
    }

    meterNamingConvention = serviceProvider
        .GetServices<IMessengerNamingConvention>()
        .FirstOrDefault(service => meterIdPrefix == service.IdPrefix)
      ?? throw new InvalidOperationException(
        $"No MessengerNamingConvention found for {meterId}");

    idCache.TryAdd(meterId, meterNamingConvention);

    return meterNamingConvention;
  }

  private IMessengerNamingConvention GetMessengerNamingConvention(
    Type meterType
  )
  {
    if (typeCache.TryGetValue(meterType, out var meterNamingConvention))
    {
      return meterNamingConvention;
    }

    meterNamingConvention = serviceProvider
        .GetServices<IMessengerNamingConvention>()
        .FirstOrDefault(service => meterType == service.MessengerType)
      ?? throw new InvalidOperationException(
        $"No MessengerNamingConvention found for {meterType}");

    typeCache.TryAdd(meterType, meterNamingConvention);

    return meterNamingConvention;
  }
}
