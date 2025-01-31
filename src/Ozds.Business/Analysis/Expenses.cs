using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Analysis;

public record Expenses(
  DateTimeOffset Timestamp,
  decimal Total_EUR
);

public static class ExpensesExtensions
{
  public static List<Expenses> Expenses(
    this IEnumerable<ICalculation> models
  )
  {
    return models
      .GroupBy(x => x.FromDate)
      .Select(
        x => new Expenses(
          x.Key,
          x
            .Select(x => x.Total_EUR)
            .DefaultIfEmpty(0)
            .Sum())
      )
      .OrderByDescending(x => x.Timestamp)
      .ToList();
  }

  public static List<Expenses> Expenses(
    this IEnumerable<IInvoice> models
  )
  {
    return models
      .GroupBy(x => x.FromDate)
      .Select(
        x => new Expenses(
          x.Key,
          x
            .Select(x => x.TotalWithTax_EUR)
            .DefaultIfEmpty(0)
            .Sum()
        ))
      .OrderByDescending(x => x.Timestamp)
      .ToList();
  }

  public static Expenses Aggregate(
    this IEnumerable<Expenses> models)
  {
    return new Expenses(
      models
        .Select(x => x.Timestamp)
        .DefaultIfEmpty(DateTimeOffset.MinValue)
        .Min(),
      models
        .Select(x => x.Total_EUR)
        .DefaultIfEmpty(0)
        .Sum()
    );
  }
}
