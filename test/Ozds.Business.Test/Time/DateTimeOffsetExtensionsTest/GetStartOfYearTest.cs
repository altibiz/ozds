using Ozds.Business.Time;
using Xunit;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetStartOfYearTest
{
  [Theory]
  [InlineData("2023-01-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-02-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-03-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-04-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-05-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-06-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-07-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-08-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-09-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-10-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-11-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2023-12-15T12:34:56Z", "2022-12-31T23:00:00Z")]
  [InlineData("2024-02-29T12:34:56Z", "2023-12-31T23:00:00Z")]
  public void GetStartOfYear_ReturnsExpectedStartOfYear(string inputDateString,
    string expectedDateString)
  {
    var inputDate = DateTimeOffset.Parse(inputDateString);
    var expectedDate = DateTimeOffset.Parse(expectedDateString);

    var result = inputDate.GetStartOfYear();

    Assert.Equal(expectedDate, result);
  }
}
