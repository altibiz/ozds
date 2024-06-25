using Ozds.Business.Time;
using Xunit;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetMonthRangeTest
{
  [Theory]
  [InlineData("2023-01-15T12:34:56Z", "2022-12-31T23:00:00Z",
    "2023-01-31T23:00:00Z")]
  [InlineData("2023-02-15T12:34:56Z", "2023-01-31T23:00:00Z",
    "2023-02-28T23:00:00Z")]
  [InlineData("2023-03-15T12:34:56Z", "2023-02-28T23:00:00Z",
    "2023-03-31T22:00:00Z")]
  [InlineData("2023-04-15T12:34:56Z", "2023-03-31T22:00:00Z",
    "2023-04-30T22:00:00Z")]
  [InlineData("2023-05-15T12:34:56Z", "2023-04-30T22:00:00Z",
    "2023-05-31T22:00:00Z")]
  [InlineData("2023-06-15T12:34:56Z", "2023-05-31T22:00:00Z",
    "2023-06-30T22:00:00Z")]
  [InlineData("2023-07-15T12:34:56Z", "2023-06-30T22:00:00Z",
    "2023-07-31T22:00:00Z")]
  [InlineData("2023-08-15T12:34:56Z", "2023-07-31T22:00:00Z",
    "2023-08-31T22:00:00Z")]
  [InlineData("2023-09-15T12:34:56Z", "2023-08-31T22:00:00Z",
    "2023-09-30T22:00:00Z")]
  [InlineData("2023-10-15T12:34:56Z", "2023-09-30T22:00:00Z",
    "2023-10-31T23:00:00Z")]
  [InlineData("2023-11-15T12:34:56Z", "2023-10-31T23:00:00Z",
    "2023-11-30T23:00:00Z")]
  [InlineData("2023-12-15T12:34:56Z", "2023-11-30T23:00:00Z",
    "2023-12-31T23:00:00Z")]
  [InlineData("2024-02-29T12:34:56Z", "2024-01-31T23:00:00Z",
    "2024-02-29T23:00:00Z")]
  public void GetMonthRange_ReturnsExpectedRange(string inputDateString,
    string expectedStartString, string expectedEndString)
  {
    var inputDate = DateTimeOffset.Parse(inputDateString);
    var expectedStart = DateTimeOffset.Parse(expectedStartString);
    var expectedEnd = DateTimeOffset.Parse(expectedEndString);

    var (start, end) = inputDate.GetMonthRange();

    Assert.Equal(expectedStart, start);
    Assert.Equal(expectedEnd, end);
  }
}
