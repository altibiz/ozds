using System.Globalization;
using Ozds.Time.Queries.Implementations;

namespace Ozds.Time.Test.Queries.TimeQueriesTest;

public class GetYearRangeTest
{
  [Test]
  [Arguments(
    "2023-01-15T12:34:56Z",
    "2022-12-31T23:00:00Z",
    "2023-12-31T23:00:00Z"
  )] // Regular year, winter time
  [Arguments(
    "2023-06-15T12:34:56Z",
    "2022-12-31T23:00:00Z",
    "2023-12-31T23:00:00Z"
  )] // Same year, different month - should return same range
  [Arguments(
    "2024-02-29T12:34:56Z",
    "2023-12-31T23:00:00Z",
    "2024-12-31T23:00:00Z"
  )] // Leap year
  [Arguments(
    "2023-03-26T01:30:00Z",
    "2022-12-31T23:00:00Z",
    "2023-12-31T23:00:00Z"
  )] // DST transition day (spring)
  [Arguments(
    "2023-10-29T01:30:00Z",
    "2022-12-31T23:00:00Z",
    "2023-12-31T23:00:00Z"
  )] // DST transition day (fall)
  public void GetYearRange_ReturnsExpectedRange(
    string inputDateString,
    string expectedStartString,
    string expectedEndString
  )
  {
    var timeQueries = new TimeQueries();

    var inputDate = DateTimeOffset.Parse(
      inputDateString,
      CultureInfo.InvariantCulture
    );
    var expectedStart = DateTimeOffset.Parse(
      expectedStartString,
      CultureInfo.InvariantCulture
    );
    var expectedEnd = DateTimeOffset.Parse(
      expectedEndString,
      CultureInfo.InvariantCulture
    );

    var (start, end) = timeQueries.GetYearRange(inputDate);

    start.Should().BeExactly(expectedStart);
    end.Should().BeExactly(expectedEnd);
  }

  [Test]
  [Arguments(2023, "2022-12-31T23:00:00Z", "2023-12-31T23:00:00Z")] // Regular year
  [Arguments(2024, "2023-12-31T23:00:00Z", "2024-12-31T23:00:00Z")] // Leap year
  [Arguments(2022, "2021-12-31T23:00:00Z", "2022-12-31T23:00:00Z")] // Another regular year
  public void GetYearRange_ReturnsExpectedRangeForYearMonth(
    int year,
    string expectedStartString,
    string expectedEndString
  )
  {
    var timeQueries = new TimeQueries();

    var (start, end) = timeQueries.GetYearRange(year);

    var expectedStart = DateTimeOffset.Parse(
      expectedStartString,
      CultureInfo.InvariantCulture
    );
    var expectedEnd = DateTimeOffset.Parse(
      expectedEndString,
      CultureInfo.InvariantCulture
    );

    start.Should().BeExactly(expectedStart);
    end.Should().BeExactly(expectedEnd);
  }
}
