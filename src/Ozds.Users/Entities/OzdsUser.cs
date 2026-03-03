using Microsoft.AspNetCore.Identity;

namespace Ozds.Users.Entities;

public class OzdsUser : IdentityUser
{
  // IdentityUser.Id is string by default.
  // Set to existing LDAP uid during migration to preserve
  // RepresentativeEntity FK references.

  public string DisplayName { get; set; } = string.Empty;
}
