using System.Text.RegularExpressions;

namespace Ozds.Server.Test.Extensions;

public static partial class StringExtensions
{
  public static string Escape(this string value)
  {
    return value
      .Replace("\\", "\\\\")
      .Replace("\"", "\\\"")
      .Replace("\n", "\\n")
      .Replace("\r", "\\r")
      .Replace("\t", "\\t");
  }

  public static string NormalizeForHostName(this string value)
  {
    var lower = value.ToLowerInvariant();
    return InvalidHostCharacters().Replace(lower, "-");
  }

  [GeneratedRegex("[^a-z0-9-]")]
  private static partial Regex InvalidHostCharacters();
}
