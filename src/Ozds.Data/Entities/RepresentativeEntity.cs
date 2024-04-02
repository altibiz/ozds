using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities;

public class RepresentativeEntity : AuditableEntity
{
  private readonly string _stringId = default!;

  public override string Id { get => _stringId; init => _stringId = value; }

  public RoleEntity Role { get; set; }

  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } =
    default!;

  public virtual ICollection<LocationEntity> Locations { get; set; } = default!;

  public virtual ICollection<RepresentativeEventEntity> Events { get; set; } =
    default!;

  public virtual ICollection<RepresentativeAuditEventEntity> AuditEvents
  {
    get;
    set;
  } =
    default!;

  public string Name { get; set; } = default!;

  public string SocialSecurityNumber { get; set; } = default!;

  public string Address { get; set; } = default!;

  public string PostalCode { get; set; } = default!;

  public string City { get; set; } = default!;

  public string Email { get; set; } = default!;

  public string PhoneNumber { get; set; } = default!;
}
