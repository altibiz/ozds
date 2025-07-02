namespace Ozds.Business.Analysis;

public record Expenses(
  DateTimeOffset Timestamp,
  decimal Total_EUR
);
