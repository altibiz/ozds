using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
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

  public static readonly IReadOnlyCollection<TestUser> Users =
    new List<TestUser>
    {
      Operator,
      Location,
      NetworkUser
    };
}

public class TestUserFixture(
  ServiceComposition composition
)
{
  public async Task Create(
    TestUser testUser,
    CancellationToken cancellationToken,
    IEnumerable<LocationModel>? locations = null,
    IEnumerable<NetworkUserModel>? networkUsers = null
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();

    var activator = scope.ServiceProvider
      .GetRequiredService<ModelActivator>();

    var user = activator.Activate<UserModel>();
    TestUserToUserModel(testUser, user);

    var password = activator.Activate<PasswordModel>();
    TestUserToPasswordModel(testUser, password);

    var representative = activator.Activate<RepresentativeModel>();
    TestUserToRepresentativeModel(testUser, representative);

    var userMutations = scope.ServiceProvider
      .GetRequiredService<UserMutations>();
    await userMutations.Create(
      user,
      cancellationToken
    );

    var passwordMutations = scope.ServiceProvider
      .GetRequiredService<PasswordMutations>();
    await passwordMutations.Update(
      password,
      cancellationToken
    );

    var trackableMutations = scope.ServiceProvider
      .GetRequiredService<TrackableMutations>();
    await trackableMutations.Create(
      representative,
      cancellationToken
    );

    var modelMutations = scope.ServiceProvider
      .GetRequiredService<ModelMutations>();
    if (locations is not null)
    {
      foreach (var location in locations)
      {
        var locationRepresentative = activator
          .Activate<LocationRepresentativeModel>();
        locationRepresentative.RepresentativeId = representative.Id;
        locationRepresentative.LocationId = location.Id;
        await modelMutations.Create(
          locationRepresentative,
          cancellationToken
        );
      }
    }

    if (networkUsers is not null)
    {
      foreach (var networkUser in networkUsers)
      {
        var networkUserRepresentative = activator
          .Activate<NetworkUserRepresentativeModel>();
        networkUserRepresentative.RepresentativeId = representative.Id;
        networkUserRepresentative.NetworkUserId = networkUser.Id;
        await modelMutations.Create(
          networkUserRepresentative,
          cancellationToken
        );
      }
    }
  }

  public UserModel TestUserToUserModel(
    TestUser testUser,
    UserModel? user = null
  )
  {
    if (user is null)
    {
      var activator = composition.Ozds.Services
        .GetRequiredService<ModelActivator>();
      user = activator.Activate<UserModel>();
    }

    user.Id = testUser.Id;
    user.Name = testUser.Name;
    user.Email = testUser.Email;
    return user;
  }

  public PasswordModel TestUserToPasswordModel(
    TestUser testUser,
    PasswordModel? password = null
  )
  {
    if (password is null)
    {
      var activator = composition.Ozds.Services
        .GetRequiredService<ModelActivator>();
      password = activator.Activate<PasswordModel>();
    }

    password.UserId = testUser.Id;
    password.OldPassword = testUser.Password;
    password.NewPassword = testUser.Password;
    password.ConfirmNewPassword = testUser.Password;
    return password;
  }

  public RepresentativeModel TestUserToRepresentativeModel(
    TestUser testUser,
    RepresentativeModel? representative = null
  )
  {
    if (representative is null)
    {
      var activator = composition.Ozds.Services
        .GetRequiredService<ModelActivator>();
      representative = activator.Activate<RepresentativeModel>();
    }

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
    return representative;
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
