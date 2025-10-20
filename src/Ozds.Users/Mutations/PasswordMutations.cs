using System.Text;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Asn1;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;
using Ozds.Users.Options;

namespace Ozds.Users.Mutations;

public class PasswordMutations(
  IServiceProvider serviceProvider,
  ILogger<PasswordMutations> logger,
  IOptions<OzdsUsersOptions> options
) : IMutations
{
  private const string PasswordModifyExtendedOperation =
    "1.3.6.1.4.1.4203.1.11.1";

  public async Task Update(
    PasswordEntity entity,
    CancellationToken cancellationToken
  )
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection = scope.ServiceProvider
      .GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={entity.UserId}";
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
          $"User with id '{entity.UserId}' not found");
      }

      var entry = searchResults.Next();
      var userDn = entry.Dn;

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
      logger.LogError(ex, "LDAP error during password update");
    }
  }
}
