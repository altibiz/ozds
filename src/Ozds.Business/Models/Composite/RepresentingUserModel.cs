using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class RepresentingUserModel : MaybeRepresentingUserModel, IComposite
{
  public new RepresentativeModel Representative
  {
    get { return base.Representative!; }
    set { base.Representative = value; }
  }
}
