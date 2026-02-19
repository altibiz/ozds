namespace Ozds.Migration.Extensions;

public static class StringExtensions
{
  public static string Indent(this string value, int indent, string newline)
  {
    return string.Join(
      newline,
      value.Split(newline).Select(line => new string(' ', indent) + line)
    );
  }

  public static string Dedent(this string value, int indent, string newline)
  {
    return string.Join(
      newline,
      value
        .Split(newline)
        .Select(line =>
          line.StartsWith(new string(' ', indent))
            ? line[indent..]
            : line.TrimStart()
        )
    );
  }
}
