namespace Ozds.Migration.Extensions;

public static class StringExtensions
{
  public static string Indent(this string value, int indent)
  {
    return string.Join(
      Environment.NewLine,
      value
        .Split(Environment.NewLine)
        .Select(line => new string(' ', indent) + line));
  }

  public static string Dedent(this string value, int indent)
  {
    return string.Join(
      Environment.NewLine,
      value
        .Split(Environment.NewLine)
        .Select(
          line => line.StartsWith(new string(' ', indent))
            ? line[indent..]
            : line.TrimStart()));
  }
}
