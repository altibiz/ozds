using System.Globalization;
using Ozds.Business.Time;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetThisYearMonthStartsTest
{
  [Theory]
  [InlineData(
    "2023-06-15T12:34:56Z",
    new[]
    {
      "2022-12-31T23:00:00Z", // January 1, 2023, CET offset +1
      "2023-01-31T23:00:00Z", // February 1, 2023, CET offset +1
      "2023-02-28T23:00:00Z", // March 1, 2023, CET offset +1
      "2023-03-31T22:00:00Z", // April 1, 2023, CEST offset +2
      "2023-04-30T22:00:00Z", // May 1, 2023, CEST offset +2
      "2023-05-31T22:00:00Z", // June 1, 2023, CEST offset +2
      "2023-06-30T22:00:00Z", // July 1, 2023, CEST offset +2
      "2023-07-31T22:00:00Z", // August 1, 2023, CEST offset +2
      "2023-08-31T22:00:00Z", // September 1, 2023, CEST offset +2
      "2023-09-30T22:00:00Z", // October 1, 2023, CEST offset +2
      "2023-10-31T23:00:00Z", // November 1, 2023, CET offset +1
      "2023-11-30T23:00:00Z" // December 1, 2023, CET offset +1
    })]
  [InlineData(
    "2024-03-15T12:34:56Z",
    new[]
    {
      "2023-12-31T23:00:00Z", // January 1, 2024, CET offset +1
      "2024-01-31T23:00:00Z", // February 1, 2024, CET offset +1
      "2024-02-29T23:00:00Z", // March 1, 2024, CET offset +1 (leap year)
      "2024-03-31T22:00:00Z", // April 1, 2024, CEST offset +2
      "2024-04-30T22:00:00Z", // May 1, 2024, CEST offset +2
      "2024-05-31T22:00:00Z", // June 1, 2024, CEST offset +2
      "2024-06-30T22:00:00Z", // July 1, 2024, CEST offset +2
      "2024-07-31T22:00:00Z", // August 1, 2024, CEST offset +2
      "2024-08-31T22:00:00Z", // September 1, 2024, CEST offset +2
      "2024-09-30T22:00:00Z", // October 1, 2024, CEST offset +2
      "2024-10-31T23:00:00Z", // November 1, 2024, CET offset +1
      "2024-11-30T23:00:00Z" // December 1, 2024, CET offset +1
    })]
  [InlineData(
    "2024-12-31T23:38:56Z",
    new[]
    {
      "2024-12-31T23:00:00Z", // January 1, 2025, CET offset +1
      "2025-01-31T23:00:00Z", // February 1, 2025, CET offset +1
      "2025-02-28T23:00:00Z", // March 1, 2025, CET offset +1
      "2025-03-31T22:00:00Z", // April 1, 2025, CEST offset +2
      "2025-04-30T22:00:00Z", // May 1, 2025, CEST offset +2
      "2025-05-31T22:00:00Z", // June 1, 2025, CEST offset +2
      "2025-06-30T22:00:00Z", // July 1, 2025, CEST offset +2
      "2025-07-31T22:00:00Z", // August 1, 2025, CEST offset +2
      "2025-08-31T22:00:00Z", // September 1, 2025, CEST offset +2
      "2025-09-30T22:00:00Z", // October 1, 2025, CEST offset +2
      "2025-10-31T23:00:00Z", // November 1, 2025, CET offset +1
      "2025-11-30T23:00:00Z" // December 1, 2025, CET offset +1
    })]
  public void GetThisYearMonthStarts_ReturnsExpectedMonthStarts(
    string inputDateString,
    string[] expectedDateStrings)
  {
    var inputDate = DateTimeOffset.Parse(
      inputDateString, CultureInfo.InvariantCulture);
    var expectedDates = expectedDateStrings.Select(
      s => DateTimeOffset.Parse(s, CultureInfo.InvariantCulture)).ToList();

    var resultDates = inputDate.GetThisYearMonthStarts().ToList();

    Assert.Equal(expectedDates.Count, resultDates.Count);
    for (var i = 0; i < expectedDates.Count; i++)
    {
      Assert.Equal(expectedDates[i], resultDates[i]);
    }
  }
}
