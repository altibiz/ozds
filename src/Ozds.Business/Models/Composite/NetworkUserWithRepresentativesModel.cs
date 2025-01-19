namespace Ozds.Business.Models.Composite;

public class NetworkUserWithRepresentativesModel
{
  public NetworkUserModel NetworkUser { get; set; } = default!;

  public List<RepresentativeModel> Representatives { get; set; } =
    default!;
}
