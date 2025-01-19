namespace Ozds.Business.Models.Composite;

public class RepresentingUserModel : MaybeRepresentingUserModel
{
  public new RepresentativeModel Representative
  {
    get => base.Representative!;
    set => base.Representative = value;
  }
}
