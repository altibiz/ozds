using System.Globalization;

namespace Ozds.Client.State;

public record CultureState(
  CultureInfo Culture,
  TimeZoneInfo TimeZoneInfo,
  Func<CultureInfo, Task> SetCulture
);
