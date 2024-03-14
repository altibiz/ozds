namespace Ozds.Data.Entities;

public class SubnetEntity : IdEntity
{
  public List<RepresentativeEntity> Representatives { get; set; } = new();
}
