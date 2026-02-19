using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class NetworkUserWithRepresentativesModel : IComposite
{
  public NetworkUserModel NetworkUser { get; set; } = default!;

  public List<RepresentativeModel> Representatives { get; set; } = default!;
}
