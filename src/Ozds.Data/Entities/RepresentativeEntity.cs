namespace Ozds.Data.Entities;

public class RepresentativeEntity : IdEntity
{
  public string UserId { get; set; } = default!;
  public bool IsOperatorRepresentative { get; set; } = default!;
  public List<TenantEntity> Tenants { get; set; } = new();
  public List<SubnetEntity> Subnets { get; set; } = new();
  public string Name { get; set; } = default!;
  public string SocialSecurityNumber { get; set; } = default!;
  public string Address { get; set; } = default!;
  public string City { get; set; } = default!;
  public string PostalCode { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string PhoneNumber { get; set; } = default!;
}
