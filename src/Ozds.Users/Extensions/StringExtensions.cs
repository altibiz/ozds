namespace Ozds.Users.Extensions;

public static class StringExtensions
{
  public static string EscapeLdap(this string value)
  {
    return value
      .Replace(@"\", @"\5c")
      .Replace("*", @"\2a")
      .Replace("(", @"\28")
      .Replace(")", @"\29")
      .Replace("\0", @"\00");
  }
}
