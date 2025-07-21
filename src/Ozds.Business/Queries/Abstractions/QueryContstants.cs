namespace Ozds.Business.Queries.Abstractions;

public static class QueryConstants
{
  public const int StartingPage = 1;

  public const int DefaultPageCount = 10;

  public const int DefaultLargePageCount = 10000;

  public const int DefaultFinancialPageCount = 1000;

  // Set at 5000 because otherwise reduces aggregate resolution too early
  public const int DefaultMeasurementPageCount = 5000;
}
