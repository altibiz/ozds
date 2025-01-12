using System.Globalization;

namespace Ozds.Client.State;

public record CultureState(
  CultureInfo Culture,
  Func<CultureInfo, Task> SetCulture
);
