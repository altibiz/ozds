using System.Text;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Asn1;
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
  private const string PasswordModifyExtendedOperation =
    "1.3.6.1.4.1.4203.1.11.1";

  public async Task CreateUser(
    UserWithPasswordEntity entity,
    CancellationToken cancellationToken
  )
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection = scope.ServiceProvider
      .GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={entity.User.Id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes =
      {
        options.Value.Ldap.UserIdAttribute,
        options.Value.Ldap.UserNameAttribute,
        options.Value.Ldap.UserEmailAttribute
      };

      var searchResults = await Task.Run(
        () => ldapConnection.Search(
          options.Value.Ldap.BaseDn,
          LdapConnection.ScopeSub,
          filter,
          attributes,
          false
        ), cancellationToken);

      if (searchResults.HasMore())
      {
        throw new InvalidOperationException(
          $"User with id '{entity.User.Id}' already exists");
      }

      var userDn =
        $"{options.Value.Ldap.UserIdAttribute}={entity.User.Id}"
        + $",ou={options.Value.Ldap.UserOrganizationalUnit}"
        + $",{options.Value.Ldap.BaseDn}";

      var entry = new LdapAttributeSet();

      var objectClassAttr = new LdapAttribute(
        "objectClass",
        options.Value.Ldap.UserObjectClasses.ToArray()
      );
      entry.Add(objectClassAttr);

      entry.Add(
        new LdapAttribute(options.Value.Ldap.UserIdAttribute, entity.User.Id));

      entry.Add(
        new LdapAttribute(
          options.Value.Ldap.UserNameAttribute, entity.User.Name));

      entry.Add(
        new LdapAttribute(
          options.Value.Ldap.UserEmailAttribute, entity.User.Email));

      var newEntry = new LdapEntry(userDn, entry);
      await Task.Run(() => ldapConnection.Add(newEntry), cancellationToken);

      var sequence = new Asn1Sequence();
      var idTag = new Asn1Tagged(
        new Asn1Identifier(Asn1Identifier.Context, false, 0),
        new Asn1OctetString(Encoding.UTF8.GetBytes(userDn)),
        false
      );
      sequence.Add(idTag);
      var newPasswordTag = new Asn1Tagged(
        new Asn1Identifier(Asn1Identifier.Context, false, 2),
        new Asn1OctetString(Encoding.UTF8.GetBytes(entity.NewPassword)),
        false
      );
      sequence.Add(newPasswordTag);
      var value = sequence.GetEncoding(new LberEncoder());
      var passwordOperation = new LdapExtendedOperation(
        PasswordModifyExtendedOperation,
        value
      );

      await Task.Run(
        () => ldapConnection.ExtendedOperation(passwordOperation),
        cancellationToken);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error during user creation");
    }
  }

  public async Task UpdateUser(
    UserWithPasswordEntity entity,
    CancellationToken cancellationToken
  )
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection = scope.ServiceProvider
      .GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={entity.User.Id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes =
      {
        options.Value.Ldap.UserIdAttribute,
        options.Value.Ldap.UserNameAttribute,
        options.Value.Ldap.UserEmailAttribute
      };

      var searchResults = await Task.Run(
        () => ldapConnection.Search(
          options.Value.Ldap.BaseDn,
          LdapConnection.ScopeSub,
          filter,
          attributes,
          false
        ), cancellationToken);

      if (!searchResults.HasMore())
      {
        throw new InvalidOperationException(
          $"User with id '{entity.User.Id}' not found");
      }

      var entry = searchResults.Next();
      var userDn = entry.Dn;

      var modifications = new List<LdapModification>();

      var emailAttribute = new LdapAttribute(
        options.Value.Ldap.UserEmailAttribute,
        entity.User.Email
      );
      var emailModification = new LdapModification(
        LdapModification.Replace,
        emailAttribute
      );
      modifications.Add(emailModification);

      var nameAttribute = new LdapAttribute(
        options.Value.Ldap.UserNameAttribute,
        entity.User.Name
      );
      var nameModification = new LdapModification(
        LdapModification.Replace,
        nameAttribute
      );
      modifications.Add(nameModification);

      await Task.Run(
        () =>
          ldapConnection.Modify(userDn, modifications.ToArray()),
        cancellationToken);

      var sequence = new Asn1Sequence();
      var idTag = new Asn1Tagged(
        new Asn1Identifier(Asn1Identifier.Context, false, 0),
        new Asn1OctetString(Encoding.UTF8.GetBytes(userDn)),
        false
      );
      sequence.Add(idTag);
      var oldPasswordTag = new Asn1Tagged(
        new Asn1Identifier(Asn1Identifier.Context, false, 1),
        new Asn1OctetString(Encoding.UTF8.GetBytes(entity.OldPassword)),
        false
      );
      sequence.Add(oldPasswordTag);
      var newPasswordTag = new Asn1Tagged(
        new Asn1Identifier(Asn1Identifier.Context, false, 2),
        new Asn1OctetString(Encoding.UTF8.GetBytes(entity.NewPassword)),
        false
      );
      sequence.Add(newPasswordTag);
      var value = sequence.GetEncoding(new LberEncoder());
      var passwordOperation = new LdapExtendedOperation(
        PasswordModifyExtendedOperation,
        value
      );

      await Task.Run(
        () => ldapConnection.ExtendedOperation(passwordOperation),
        cancellationToken);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error during user update");
    }
  }

  public async Task DeleteUser(string id, CancellationToken cancellationToken)
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection = scope.ServiceProvider
      .GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes = { options.Value.Ldap.UserIdAttribute };

      var searchResults = await Task.Run(
        () => ldapConnection.Search(
          options.Value.Ldap.BaseDn,
          LdapConnection.ScopeSub,
          filter,
          attributes,
          false
        ), cancellationToken);

      if (!searchResults.HasMore())
      {
        throw new InvalidOperationException(
          $"User with id '{id}' not found");
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
