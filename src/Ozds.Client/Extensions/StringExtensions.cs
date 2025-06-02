using System.Collections;
using System.Text;

namespace Ozds.Client.Extensions;

public static class StringExtensions
{
  public static string JoinString(this IEnumerable enumerable, string delimiter = ",")
  {
    var builder = new StringBuilder();
    foreach (var item in enumerable)
    {
      builder.Append(item);
      builder.Append(delimiter);
    }
    return builder.ToString();
  }
}
