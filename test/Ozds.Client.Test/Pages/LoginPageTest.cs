using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace Ozds.Client.Test.Pages;

public partial class LoginPageTest(
  IPage page,
  LoginNavigator loginNavigator
)
{
  public static readonly
    TheoryData<Login>
    Logins = new(
      Login.Operator,
      Login.Location,
      Login.NetworkUser);

  [Fact]
  public async Task LoginPage_Loads()
  {
    var response = await page.GotoAsync("/login");

    response.Should().NotBeNull();
    response!.Status.Should().Be((int)HttpStatusCode.OK);
  }

  [Theory]
  [MemberData(nameof(Logins))]
  public async Task LoginPage_CanLoginAs(Login login)
  {
    await page.GotoAsync("/login");
    await loginNavigator.LoginOnLoginPage(login);

    var pageAssertions = Assertions.Expect(page);
    await pageAssertions.ToHaveURLAsync(IndexRegex());
  }

  [Theory]
  [MemberData(nameof(Logins))]
  public async Task LoginPage_CanPickLocation(Login login)
  {
    await page.GotoAsync("/login");
    await loginNavigator.LoginOnLoginPage(login);
    await loginNavigator.PickFirstLocationOnLocationPicker();

    var pageAssertions = Assertions.Expect(page);
    await pageAssertions.ToHaveURLAsync(IndexRegex());
    var logoLocator = page.Locator("img[src=\"/logo.svg\"]");
    var logoAssertions = Assertions.Expect(logoLocator);
    await logoAssertions.ToBeVisibleAsync();
  }

  [GeneratedRegex("/app/_/?")]
  private partial Regex IndexRegex();
}
