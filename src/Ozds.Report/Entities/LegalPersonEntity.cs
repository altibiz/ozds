using Ozds.Report.Entities.Abstractions;

namespace Ozds.Report.Entities;

public class LegalPersonEntity : IEntity
{
  public string Name { get; set; } = default!;

  public string SocialSecurityNumber { get; set; } = default!;

  public string Address { get; set; } = default!;

  public string PostalCode { get; set; } = default!;

  public string City { get; set; } = default!;

  public string Email { get; set; } = default!;

  public string PhoneNumber { get; set; } = default!;
}
