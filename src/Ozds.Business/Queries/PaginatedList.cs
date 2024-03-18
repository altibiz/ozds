namespace Ozds.Business.Queries;

public record PaginatedList<T>(
  List<T> Items,
  int TotalCount
);

public static class PaginatedList
{
  public static PaginatedList<T> ToPaginated<T>(
    this List<T> data,
    int totalCount
  ) => new(data, totalCount);

  public static PaginatedList<T> ToPaginatedList<T>(
    this IEnumerable<T> data,
    int totalCount
  ) => new(data.ToList(), totalCount);
}

