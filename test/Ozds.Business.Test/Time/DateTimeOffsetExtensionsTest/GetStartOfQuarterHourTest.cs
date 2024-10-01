using System.Globalization;
using Ozds.Business.Time;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetStartOfQuarterHourTest
{
  [Theory]
  // Regular times
  [InlineData("2023-01-15T12:05:56Z", "2023-01-15T12:00:00Z")]
  [InlineData("2023-02-15T12:10:56Z", "2023-02-15T12:00:00Z")]
  [InlineData("2023-03-15T12:25:56Z", "2023-03-15T12:15:00Z")]
  [InlineData("2023-04-15T12:35:56Z", "2023-04-15T12:30:00Z")]
  [InlineData("2023-05-15T12:50:56Z", "2023-05-15T12:45:00Z")]
  [InlineData("2023-06-15T12:55:56Z", "2023-06-15T12:45:00Z")]
  // Daylight Saving Time starts on March 31, 2024, at 2:00 AM local time
  [InlineData("2024-03-31T00:30:00Z", "2024-03-31T00:30:00Z")] // Before DST starts
  [InlineData("2024-03-31T01:30:00Z", "2024-03-31T01:30:00Z")] // During DST transition
  [InlineData("2024-03-31T02:30:00Z", "2024-03-31T02:30:00Z")] // After DST starts
  // Daylight Saving Time ends on October 27, 2024, at 3:00 AM local time
  [InlineData("2024-10-27T00:30:00Z", "2024-10-27T01:30:00Z")] // Before DST ends
  [InlineData("2024-10-27T01:30:00Z", "2024-10-27T01:30:00Z")] // During DST transition
  [InlineData("2024-10-27T02:30:00Z", "2024-10-27T02:30:00Z")] // After DST ends
  // Edge cases around the DST transition
  [InlineData("2024-03-31T00:59:59Z", "2024-03-31T00:45:00Z")] // Right before DST starts
  [InlineData("2024-03-31T01:00:00Z", "2024-03-31T01:00:00Z")] // Exact moment of DST start
  [InlineData("2024-10-27T01:59:59Z", "2024-10-27T01:45:00Z")] // Right before DST ends
  [InlineData("2024-10-27T02:00:00Z", "2024-10-27T02:00:00Z")] // Exact moment of DST end
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
