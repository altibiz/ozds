using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class NetworkUserEntity : IdEntity
{
  public List<RepresentativeEntity> Representatives { get; set; } = new();
}
