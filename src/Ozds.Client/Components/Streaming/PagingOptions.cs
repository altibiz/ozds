namespace Ozds.Client.Components.Streaming;

public record PaginationOptions(
  int PageCount,
  int PageNumber,
  int TotalCount,
  Func<int, Task> SetPageNumber
);
