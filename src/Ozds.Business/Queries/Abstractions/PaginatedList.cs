using System.Collections;

namespace Ozds.Business.Queries.Abstractions;

public record PaginatedList(Type Type, IList ObjectItems, int TotalCount);

public record PaginatedList<T>(List<T> Items, int TotalCount)
  : PaginatedList(typeof(T), Items, TotalCount)
{
  public static readonly PaginatedList<T> Empty = new(new List<T>(), 0);
}

public static class PaginatedListExtensions
{
  public static PaginatedList<T> ToPaginated<T>(
    this List<T> data,
    int totalCount
  )
  {
    return new PaginatedList<T>(data, totalCount);
  }

  public static PaginatedList<T> ToPaginatedList<T>(
    this IEnumerable<T> data,
    int? totalCount = null
  )
  {
    var list = data.ToList();
    totalCount ??= list.Count;
    return new PaginatedList<T>(data.ToList(), totalCount.Value);
  }
}
