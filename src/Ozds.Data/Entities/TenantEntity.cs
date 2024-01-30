namespace Ozds.Data.Entities;

public class TenantEntity : IdEntity
{
  public List<RepresentativeEntity> Representatives { get; set; } = [];
}
