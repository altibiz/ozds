using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Ozds.Client.Test.Base;

namespace Ozds.Client.Test.Pages;

public partial class LoginPageTest : OzdsClientTestBase
{
  public static IEnumerable<TestUser> Users()
  {
    return new List<TestUser>
    {
      TestUser.Operator,
      TestUser.Location,
      TestUser.NetworkUser
    };
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
  public async Task LoginPage_CanLoginAs(
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
  public async Task LoginPage_CanPickLocation(
    TestUser user,
    CancellationToken cancellationToken
  )
  {
    // NOTE: otherwise it just goes to the first location
    var locationACatalogueSet = await Location
      .CreateWithCatalogues(cancellationToken);
    var locationBCatalogueSet = await Location
      .CreateWithCatalogues(cancellationToken);

    await User.Create(
      user,
      cancellationToken,
      [
        locationACatalogueSet.Location,
        locationBCatalogueSet.Location
      ]
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
