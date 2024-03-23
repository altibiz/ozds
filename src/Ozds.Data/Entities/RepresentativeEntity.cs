using System.ComponentModel.DataAnnotations;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class RepresentativeEntity : AuditableEntity
{
  [Required]
  public bool IsOperatorRepresentative { get; set; } = default!;

  public List<NetworkUserEntity> NetworkUsers { get; set; } = new();

  public List<LocationEntity> Locations { get; set; } = new();

  public List<RepresentativeEventEntity> Events { get; set; } = new();

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
