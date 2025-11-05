using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class MaybeRepresentingUserModel : IComposite
{
  public UserModel User { get; set; } = default!;

  public RepresentativeModel? Representative { get; set; } = default!;
}
