using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Mutations;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public record TestUser(
  string Id,
  string Name,
  string Email,
  string PhoneNumber,
  RoleModel Role,
  string Password
)
{
  public static readonly TestUser Operator = new(
    "operator",
    "operator",
    "operator@ozds.com",
    "123-456-7890",
    RoleModel.OperatorRepresentative,
    "Operator123!"
  );

  public static readonly TestUser Location = new(
    "location",
    "location",
    "location@ozds.com",
    "123-456-7890",
    RoleModel.LocationRepresentative,
    "Location123!"
  );

  public static readonly TestUser NetworkUser = new(
    "network-user",
    "network-user",
    "user@ozds.com",
    "123-456-7890",
    RoleModel.NetworkUserRepresentative,
    "NetworkUser123!"
  );
}

public class TestUserFixture(
  ServiceComposition composition
)
{
  public async Task Create(
    TestUser testUser,
    CancellationToken cancellationToken,
    List<LocationModel>? locations = null,
    List<NetworkUserModel>? networkUsers = null
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();

    var activator = scope.ServiceProvider
      .GetRequiredService<ModelActivator>();

    var user = activator.Activate<UserModel>();
    user.Id = testUser.Id;
    user.Name = testUser.Name;
    user.Email = testUser.Email;

    var userWithPassword = new UserWithPasswordModel
    {
      User = user,
      OldPassword = testUser.Password,
      NewPassword = testUser.Password
    };

    var representative = activator.Activate<RepresentativeModel>();
    representative.Id = testUser.Id;
    representative.Title = testUser.Name;
    representative.PhysicalPerson = new PhysicalPersonModel
    {
      Name = testUser.Name,
      Email = testUser.Email,
      PhoneNumber = testUser.PhoneNumber
    };
    representative.Role = testUser.Role;
    representative.Topics = [];

    var userMutations = scope.ServiceProvider
      .GetRequiredService<UserMutations>();
    await userMutations.CreateUser(
      userWithPassword,
      cancellationToken
    );

    var auditableMutations = scope.ServiceProvider
      .GetRequiredService<AuditableMutations>();
    await auditableMutations.Create(
      representative,
      cancellationToken
    );

    var joinMutations = scope.ServiceProvider
      .GetRequiredService<JoinMutations>();
    if (locations is not null)
    {
      foreach (var location in locations)
      {
        var locationRepresentative = new LocationRepresentativeModel
        {
          RepresentativeId = representative.Id,
          LocationId = location.Id
        };
        await joinMutations.Create(
          locationRepresentative,
          cancellationToken
        );
      }
    }

    if (networkUsers is not null)
    {
      foreach (var networkUser in networkUsers)
      {
        var networkUserRepresentative = new NetworkUserRepresentativeModel
        {
          RepresentativeId = representative.Id,
          NetworkUserId = networkUser.Id
        };
        await joinMutations.Create(
          networkUserRepresentative,
          cancellationToken
        );
      }
    }
  }

  public async Task LoginOnLoginPage(
    TestUser @as,
    CancellationToken _
  )
  {
    await composition.Playwright.Page
      .FillAsync("input[id=username-textfield]", @as.Name);
    await composition.Playwright.Page
      .FillAsync("input[id=password-textfield]", @as.Password);
    await composition.Playwright.Page
      .ClickAsync("button[id=sign-in-button]");
    await composition.Playwright.Page
      .ClickAsync("button[id=openid-consent-accept]");
  }
}
