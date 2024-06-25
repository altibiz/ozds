using Ozds.Business.Time;
using Xunit;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetStartOfDayTest
{
  [Theory]
  [InlineData("2023-01-15T12:34:56Z", "2023-01-14T23:00:00Z")]
  [InlineData("2023-02-15T12:34:56Z", "2023-02-14T23:00:00Z")]
  [InlineData("2023-03-15T12:34:56Z", "2023-03-14T23:00:00Z")]
  [InlineData("2023-04-15T12:34:56Z", "2023-04-14T22:00:00Z")]
  [InlineData("2023-05-15T12:34:56Z", "2023-05-14T22:00:00Z")]
  [InlineData("2023-06-15T12:34:56Z", "2023-06-14T22:00:00Z")]
  [InlineData("2023-07-15T12:34:56Z", "2023-07-14T22:00:00Z")]
  [InlineData("2023-08-15T12:34:56Z", "2023-08-14T22:00:00Z")]
  [InlineData("2023-09-15T12:34:56Z", "2023-09-14T22:00:00Z")]
  [InlineData("2023-10-15T12:34:56Z", "2023-10-14T22:00:00Z")]
  [InlineData("2023-11-15T12:34:56Z", "2023-11-14T23:00:00Z")]
  [InlineData("2023-12-15T12:34:56Z", "2023-12-14T23:00:00Z")]
  public void GetStartOfDay_ReturnsExpectedStartOfDay(string inputDateString,
    string expectedDateString)
  {
    var inputDate = DateTimeOffset.Parse(inputDateString);
    var expectedDate = DateTimeOffset.Parse(expectedDateString);

    var result = inputDate.GetStartOfDay();

    Assert.Equal(expectedDate, result);
  }
}
