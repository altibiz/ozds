using Microsoft.Playwright;
using IFixture = Ozds.Client.Test.Fixtures.Abstractions.IFixture;

namespace Ozds.Client.Test.Fixtures;

public record Login(
  string Username,
  string Password
)
{
  public static readonly Login Operator = new(
    "operator",
    "Operator123!"
  );

  public static readonly Login Location = new(
    "location",
    "Location123!"
  );

  public static readonly Login NetworkUser = new(
    "network-user",
    "NetworkUser123!"
  );
}

public class LoginNavigator(
  IPage page
) : IFixture
{
  public async Task LoginOnLoginPage(Login @as)
  {
    await page.FillAsync("input[id=UserName]", @as.Username);
    await page.FillAsync("input[id=Password]", @as.Password);
    await page.ClickAsync("button[type=submit]");
  }

  public async Task PickFirstLocationOnLocationPicker()
  {
    await page.ClickAsync("button[type=button]");
  }
}
