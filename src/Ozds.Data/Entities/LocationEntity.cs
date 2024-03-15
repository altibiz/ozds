using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationEntity : IdEntity
{
  public List<RepresentativeEntity> Representatives { get; set; } = new();
}
