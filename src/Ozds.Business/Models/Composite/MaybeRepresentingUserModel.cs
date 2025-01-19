namespace Ozds.Business.Models.Composite;

public class MaybeRepresentingUserModel
{
  public UserModel User { get; set; } = default!;

  public RepresentativeModel? Representative { get; set; } = default!;
}
