namespace Ozds.Data.Extensions;

public static class StringExtensions
{
  public static string Abbreviation(this string name)
  {
    return string.Concat(
      !name.Any(char.IsUpper) ? name : name.Where(char.IsUpper)).ToLower();
  }

  public static string ToSnakeCase(this string name)
  {
    return string.Concat(
      name.Select(
        (x, i) =>
          i > 0 && char.IsUpper(x) && !char.IsUpper(name[i - 1])
            ? "_" + x.ToString().ToLower()
            : x.ToString().ToLower()
      )
    );
  }
}
