using System.Text;

namespace Ozds.Assets.Extensions;

public static class StringExtensions
{
  public static string Indent(this string value, int indent, string newline)
  {
    return string.Join(
      newline,
      value
        .Split(newline)
        .Select(line => new string(' ', indent) + line));
  }

  public static string Dedent(this string value, int indent, string newline)
  {
    return string.Join(
      newline,
      value
        .Split(newline)
        .Select(
          line => line.StartsWith(new string(' ', indent))
            ? line[indent..]
            : line.TrimStart()));
  }

  public static string TrimWords(
    this string value
  )
  {
    return string.Join(
      " ",
      value
        .ReplaceLineEndings(" ")
        .Split(" ")
        .Select(word => word.Trim())
        .Where(word => word.Length > 0));
  }

  public static string Wrap(
    this string value,
    int at = 80
  )
  {
    var words = value.Split(' ');

    var result = new StringBuilder();
    var currentLineLength = 0;

    foreach (var word in words)
    {
      currentLineLength += word.Length + 1;
      if (currentLineLength >= at)
      {
        result.AppendLine();
        currentLineLength = word.Length + 1;
      }

      result.Append(word);
      result.Append(' ');
    }

    result.Remove(result.Length - 1, 1);

    return result.ToString();
  }
}
