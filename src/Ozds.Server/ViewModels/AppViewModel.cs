using System.Globalization;

namespace Ozds.Server.ViewModels;

public class AppViewModel
{
  public CultureInfo? Culture { get; set; } = default!;

  public string LogoutToken { get; set; } = default!;
}
