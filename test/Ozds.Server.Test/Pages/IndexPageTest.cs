using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Ozds.Server.Test.Base;

namespace Ozds.Server.Test.Pages;

public partial class IndexPageTest : OzdsServerTestBase
{
  public static IEnumerable<TestUser> Users()
  {
    return TestUser.Users;
  }

  [Test]
  public async Task IndexPage_Loads()
  {
    var response = await Page.GotoAsync("/");

    response.Should().NotBeNull();
    response!.Status.Should().Be((int)HttpStatusCode.OK);
  }

  [Test]
  [MethodDataSource(nameof(Users))]
  public async Task IndexPage_CanLoginAs(
    TestUser user,
    CancellationToken cancellationToken
  )
  {
    await User.Create(user, cancellationToken);

    await Page.GotoAsync("/");
    await User.LoginOnLoginPage(user, cancellationToken);

    var pageAssertions = Assertions.Expect(Page);
    await pageAssertions.ToHaveURLAsync(IndexRegex());
  }

  [Test]
  [MethodDataSource(nameof(Users))]
  public async Task IndexPage_CanPickLocation(
    TestUser user,
    CancellationToken cancellationToken
  )
  {
    // NOTE: two locations because otherwise it just goes to the first location
    var locationA = await Location.Create(cancellationToken);
    var locationB = await Location.Create(cancellationToken);

    await User.Create(
      user,
      cancellationToken,
      [locationA.Location, locationB.Location]
    );

    await Page.GotoAsync("/");
    await User.LoginOnLoginPage(user, cancellationToken);
    await Location.PickFirstLocationOnLocationPicker(cancellationToken);

    var pageAssertions = Assertions.Expect(Page);
    await pageAssertions.ToHaveURLAsync(IndexRegex());
    var logoLocator = Page.Locator("img[src=\"/logo.svg\"]");
    var logoAssertions = Assertions.Expect(logoLocator);
    await logoAssertions.ToBeVisibleAsync();
  }

  [GeneratedRegex("/app/_/?")]
  private partial Regex IndexRegex();
}
