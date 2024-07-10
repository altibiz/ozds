using System.Globalization;
using Ozds.Business.Time;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetStartOfQuarterHourTest
{
  [Theory]
  [InlineData("2023-01-15T12:05:56Z", "2023-01-15T12:00:00Z")]
  [InlineData("2023-02-15T12:10:56Z", "2023-02-15T12:00:00Z")]
  [InlineData("2023-03-15T12:25:56Z", "2023-03-15T12:15:00Z")]
  [InlineData("2023-04-15T12:35:56Z", "2023-04-15T12:30:00Z")]
  [InlineData("2023-05-15T12:50:56Z", "2023-05-15T12:45:00Z")]
  [InlineData("2023-06-15T12:55:56Z", "2023-06-15T12:45:00Z")]
  public void GetStartOfQuarterHour_ReturnsExpectedStartOfQuarterHour(
    string inputDateString,
    string expectedDateString)
  {
    var inputDate = DateTimeOffset.Parse(
      inputDateString, CultureInfo.InvariantCulture);
    var expectedDate = DateTimeOffset.Parse(
      expectedDateString, CultureInfo.InvariantCulture);

    var result = inputDate.GetStartOfQuarterHour();

    Assert.Equal(expectedDate, result);
  }
}
