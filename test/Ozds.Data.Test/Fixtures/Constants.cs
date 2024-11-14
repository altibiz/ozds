using System.Diagnostics;

namespace Ozds.Data.Test.Fixtures;

public static class Constants
{
  public static int DefaultRepeatCount => Debugger.IsAttached ? 1 : 10;

  public const int DefaultFuzzCount = 3;
}
