namespace Ozds.Business.Models.Composite;

public record RepresentingUserModel(
  UserModel User,
  RepresentativeModel Representative
) : MaybeRepresentingUserModel(User, Representative);
