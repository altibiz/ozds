using System.ComponentModel.DataAnnotations;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class RepresentativeEntity : AuditableEntity
{
  [Required]
  public bool IsOperatorRepresentative { get; set; } = default!;

  public ICollection<NetworkUserEntity> NetworkUsers { get; set; } = default!;

  public ICollection<LocationEntity> Locations { get; set; } = default!;

  public ICollection<RepresentativeEventEntity> Events { get; set; } = default!;

  public ICollection<RepresentativeAuditEventEntity> AuditEvents { get; set; } = default!;

  [Required]
  public string Name { get; set; } = default!;

  [Required]
  public string SocialSecurityNumber { get; set; } = default!;

  [Required]
  public string Address { get; set; } = default!;

  [Required]
  public string City { get; set; } = default!;

  [Required]
  public string PostalCode { get; set; } = default!;

  [Required]
  public string Email { get; set; } = default!;

  [Required]
  public string PhoneNumber { get; set; } = default!;
}
