using System.Globalization;
using Ozds.Time.Queries.Implementations;

namespace Ozds.Time.Test.Queries.TimeQueriesTest;

public class GetMonthRangeTest
{
  [Test]
  [Arguments(
    "2023-01-15T12:34:56Z", "2022-12-31T23:00:00Z",
    "2023-01-31T23:00:00Z")]
  [Arguments(
    "2023-02-15T12:34:56Z", "2023-01-31T23:00:00Z",
    "2023-02-28T23:00:00Z")]
  [Arguments(
    "2023-03-15T12:34:56Z", "2023-02-28T23:00:00Z",
    "2023-03-31T22:00:00Z")]
  [Arguments(
    "2023-04-15T12:34:56Z", "2023-03-31T22:00:00Z",
    "2023-04-30T22:00:00Z")]
  [Arguments(
    "2023-05-15T12:34:56Z", "2023-04-30T22:00:00Z",
    "2023-05-31T22:00:00Z")]
  [Arguments(
    "2023-06-15T12:34:56Z", "2023-05-31T22:00:00Z",
    "2023-06-30T22:00:00Z")]
  [Arguments(
    "2023-07-15T12:34:56Z", "2023-06-30T22:00:00Z",
    "2023-07-31T22:00:00Z")]
  [Arguments(
    "2023-08-15T12:34:56Z", "2023-07-31T22:00:00Z",
    "2023-08-31T22:00:00Z")]
  [Arguments(
    "2023-09-15T12:34:56Z", "2023-08-31T22:00:00Z",
    "2023-09-30T22:00:00Z")]
  [Arguments(
    "2023-10-15T12:34:56Z", "2023-09-30T22:00:00Z",
    "2023-10-31T23:00:00Z")]
  [Arguments(
    "2023-11-15T12:34:56Z", "2023-10-31T23:00:00Z",
    "2023-11-30T23:00:00Z")]
  [Arguments(
    "2023-12-15T12:34:56Z", "2023-11-30T23:00:00Z",
    "2023-12-31T23:00:00Z")]
  [Arguments(
    "2024-02-29T12:34:56Z", "2024-01-31T23:00:00Z",
    "2024-02-29T23:00:00Z")]
  [Arguments(
    "2024-11-30T23:38:56Z", "2024-11-30T23:00:00Z",
    "2024-12-31T23:00:00Z")] // Edge time, CET offset is +1
  [Arguments(
    "2024-03-31T23:38:56Z", "2024-03-31T22:00:00Z",
    "2024-04-30T22:00:00Z")] // Edge time, Leap year, CET offset is +1
  public void GetMonthRange_ReturnsExpectedRange(
    string inputDateString,
    string expectedStartString,
    string expectedEndString)
  {
    var timeQueries = new TimeQueries();

    var inputDate = DateTimeOffset.Parse(
      inputDateString, CultureInfo.InvariantCulture);
    var expectedStart = DateTimeOffset.Parse(
      expectedStartString, CultureInfo.InvariantCulture);
    var expectedEnd = DateTimeOffset.Parse(
      expectedEndString, CultureInfo.InvariantCulture);

    var (start, end) = timeQueries.GetMonthRange(inputDate);

    start.Should().BeExactly(expectedStart);
    end.Should().BeExactly(expectedEnd);
  }

  [Test]
  [Arguments(
    2023, 01, "2022-12-31T23:00:00Z",
    "2023-01-31T23:00:00Z")]
  [Arguments(
    2023, 02, "2023-01-31T23:00:00Z",
    "2023-02-28T23:00:00Z")]
  [Arguments(
    2023, 03, "2023-02-28T23:00:00Z",
    "2023-03-31T22:00:00Z")]
  [Arguments(
    2023, 04, "2023-03-31T22:00:00Z",
    "2023-04-30T22:00:00Z")]
  [Arguments(
    2023, 05, "2023-04-30T22:00:00Z",
    "2023-05-31T22:00:00Z")]
  [Arguments(
    2023, 06, "2023-05-31T22:00:00Z",
    "2023-06-30T22:00:00Z")]
  [Arguments(
    2023, 07, "2023-06-30T22:00:00Z",
    "2023-07-31T22:00:00Z")]
  [Arguments(
    2023, 08, "2023-07-31T22:00:00Z",
    "2023-08-31T22:00:00Z")]
  [Arguments(
    2023, 09, "2023-08-31T22:00:00Z",
    "2023-09-30T22:00:00Z")]
  [Arguments(
    2023, 10, "2023-09-30T22:00:00Z",
    "2023-10-31T23:00:00Z")]
  [Arguments(
    2023, 11, "2023-10-31T23:00:00Z",
    "2023-11-30T23:00:00Z")]
  [Arguments(
    2023, 12, "2023-11-30T23:00:00Z",
    "2023-12-31T23:00:00Z")]
  [Arguments(
    2024, 02, "2024-01-31T23:00:00Z",
    "2024-02-29T23:00:00Z")]
  [Arguments(
    2024, 12, "2024-11-30T23:00:00Z",
    "2024-12-31T23:00:00Z")] // Edge time, CET offset is +1
  [Arguments(
    2024, 04, "2024-03-31T22:00:00Z",
    "2024-04-30T22:00:00Z")] // Edge time, Leap year, CET offset is +1
  public void GetMonthRange_ReturnsExpectedRangeForYearMonth(
    int year,
    int month,
    string expectedStartString,
    string expectedEndString
  )
  {
    var timeQueries = new TimeQueries();

    var (start, end) = timeQueries.GetMonthRange(
      year,
      month
    );

    var expectedStart = DateTimeOffset.Parse(
      expectedStartString, CultureInfo.InvariantCulture);
    var expectedEnd = DateTimeOffset.Parse(
      expectedEndString, CultureInfo.InvariantCulture);

    start.Should().BeExactly(expectedStart);
    end.Should().BeExactly(expectedEnd);
  }
}
