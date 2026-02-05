using System.Globalization;
using Ozds.Time.Queries.Implementations;

namespace Ozds.Time.Test.Queries.TimeQueriesTest;

public class GetStartOfYearTest
{
  [Test]
  [Arguments("2023-01-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-02-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-03-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-04-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-05-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-06-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-07-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-08-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-09-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-10-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-11-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2023-12-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [Arguments("2024-02-29T12:34:56Z", "2023-12-31T23:00:00Z")]
  [Arguments("2024-12-31T23:38:56Z", "2024-12-31T23:00:00Z")] // Edge time, CET offset is +1
  public void GetStartOfYear_ReturnsExpectedStartOfYear(
    string inputDateString,
    string expectedDateString
  )
  {
    var timeQueries = new TimeQueries();

    var inputDate = DateTimeOffset.Parse(
      inputDateString,
      CultureInfo.InvariantCulture
    );
    var expectedDate = DateTimeOffset.Parse(
      expectedDateString,
      CultureInfo.InvariantCulture
    );

    var result = timeQueries.GetStartOfYear(inputDate);

    result.Should().BeExactly(expectedDate);
  }
}
