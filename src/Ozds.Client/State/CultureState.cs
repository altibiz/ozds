using System.Globalization;

namespace Ozds.Client.State;

public record CultureState(
  CultureInfo Culture,
  Action<CultureInfo> SetCulture
);
