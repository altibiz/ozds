using System.Collections;
using System.Text;
using MassTransit.Internals;
using Ozds.Business.Models.Enums;

namespace Ozds.Client.Extensions;

public static class StringExtensions
{
  public static string JoinString(this IEnumerable enumerable, string delimiter = ", ")
  {
    var builder = new StringBuilder();
    foreach (var item in enumerable)
    {
      if (item is ObisModel obisItem)
      {
        builder.Append(obisItem.ToCode());
      }
      else
      {
        builder.Append(item);
      }
      builder.Append(delimiter);
    }
    return builder.ToString();
  }
}
