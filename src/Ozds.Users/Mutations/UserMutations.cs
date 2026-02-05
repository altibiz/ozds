using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;
using Ozds.Users.Options;

namespace Ozds.Users.Mutations;

public class UserMutations(
  IServiceProvider serviceProvider,
  ILogger<UserMutations> logger,
  IOptions<OzdsUsersOptions> options
) : IMutations
{
  public async Task Create(
    UserEntity entity,
    CancellationToken cancellationToken
  )
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection =
      scope.ServiceProvider.GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={entity.Id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes =
      {
        options.Value.Ldap.UserIdAttribute,
        options.Value.Ldap.UserNameAttribute,
        options.Value.Ldap.UserEmailAttribute,
      };

      var searchResults = await Task.Run(
        () =>
          ldapConnection.Search(
            options.Value.Ldap.BaseDn,
            LdapConnection.ScopeSub,
            filter,
            attributes,
            false
          ),
        cancellationToken
      );

      if (searchResults.HasMore())
      {
        throw new InvalidOperationException(
          $"User with id '{entity.Id}' already exists"
        );
      }

      var userDn =
        $"{options.Value.Ldap.UserIdAttribute}={entity.Id}"
        + $",ou={options.Value.Ldap.UserOrganizationalUnit}"
        + $",{options.Value.Ldap.BaseDn}";

      var entry = new LdapAttributeSet();

      var objectClassAttr = new LdapAttribute(
        "objectClass",
        options.Value.Ldap.UserObjectClasses.ToArray()
      );
      entry.Add(objectClassAttr);

      entry.Add(
        new LdapAttribute(options.Value.Ldap.UserIdAttribute, entity.Id)
      );

      entry.Add(
        new LdapAttribute(options.Value.Ldap.UserNameAttribute, entity.Name)
      );

      entry.Add(
        new LdapAttribute(options.Value.Ldap.UserEmailAttribute, entity.Email)
      );

      var newEntry = new LdapEntry(userDn, entry);
      await Task.Run(() => ldapConnection.Add(newEntry), cancellationToken);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error during user creation");
    }
  }

  public async Task Update(
    UserEntity entity,
    CancellationToken cancellationToken
  )
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection =
      scope.ServiceProvider.GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={entity.Id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes =
      {
        options.Value.Ldap.UserIdAttribute,
        options.Value.Ldap.UserNameAttribute,
        options.Value.Ldap.UserEmailAttribute,
      };

      var searchResults = await Task.Run(
        () =>
          ldapConnection.Search(
            options.Value.Ldap.BaseDn,
            LdapConnection.ScopeSub,
            filter,
            attributes,
            false
          ),
        cancellationToken
      );

      if (!searchResults.HasMore())
      {
        throw new InvalidOperationException(
          $"User with id '{entity.Id}' not found"
        );
      }

      var entry = searchResults.Next();
      var userDn = entry.Dn;

      var modifications = new List<LdapModification>();

      var emailAttribute = new LdapAttribute(
        options.Value.Ldap.UserEmailAttribute,
        entity.Email
      );
      var emailModification = new LdapModification(
        LdapModification.Replace,
        emailAttribute
      );
      modifications.Add(emailModification);

      var nameAttribute = new LdapAttribute(
        options.Value.Ldap.UserNameAttribute,
        entity.Name
      );
      var nameModification = new LdapModification(
        LdapModification.Replace,
        nameAttribute
      );
      modifications.Add(nameModification);

      await Task.Run(
        () => ldapConnection.Modify(userDn, modifications.ToArray()),
        cancellationToken
      );
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error during user update");
    }
  }

  public async Task Delete(string id, CancellationToken cancellationToken)
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection =
      scope.ServiceProvider.GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes = { options.Value.Ldap.UserIdAttribute };

      var searchResults = await Task.Run(
        () =>
          ldapConnection.Search(
            options.Value.Ldap.BaseDn,
            LdapConnection.ScopeSub,
            filter,
            attributes,
            false
          ),
        cancellationToken
      );

      if (!searchResults.HasMore())
      {
        throw new InvalidOperationException($"User with id '{id}' not found");
      }

      var entry = searchResults.Next();
      var userDn = entry.Dn;

      await Task.Run(() => ldapConnection.Delete(userDn), cancellationToken);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error during user deletion");
    }
  }
}
