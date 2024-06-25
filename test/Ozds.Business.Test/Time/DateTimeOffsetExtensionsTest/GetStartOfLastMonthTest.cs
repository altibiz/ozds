using System;
using Xunit;
using Ozds.Business.Time;

namespace Ozds.Business.Test.Time.DateTimeOffsetExtensionsTest;

public class GetStartOfLastMonthTest
{
  [Theory]
  [InlineData("2023-01-15T12:34:56Z", "2022-11-30T23:00:00Z")] // CET offset is +1
  [InlineData("2023-02-15T12:34:56Z", "2022-12-31T23:00:00Z")] // CET offset is +1
  [InlineData("2023-03-15T12:34:56Z", "2023-01-31T23:00:00Z")] // CET offset is +1
  [InlineData("2023-04-15T12:34:56Z", "2023-02-28T23:00:00Z")] // CET offset is +1
  [InlineData("2023-05-15T12:34:56Z", "2023-03-31T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-06-15T12:34:56Z", "2023-04-30T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-07-15T12:34:56Z", "2023-05-31T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-08-15T12:34:56Z", "2023-06-30T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-09-15T12:34:56Z", "2023-07-31T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-10-15T12:34:56Z", "2023-08-31T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-11-15T12:34:56Z", "2023-09-30T22:00:00Z")] // CEST offset is +2
  [InlineData("2023-12-15T12:34:56Z", "2023-10-31T23:00:00Z")] // CET offset is +1
  [InlineData("2024-02-29T12:34:56Z", "2023-12-31T23:00:00Z")] // CET offset is +1
  public void GetStartOfLastMonth_ReturnsExpectedStartOfLastMonth(string inputDateString, string expectedDateString)
  {
    var inputDate = DateTimeOffset.Parse(inputDateString);
    var expectedDate = DateTimeOffset.Parse(expectedDateString);

    var result = inputDate.GetStartOfLastMonth();

    Assert.Equal(expectedDate, result);
  }
}
