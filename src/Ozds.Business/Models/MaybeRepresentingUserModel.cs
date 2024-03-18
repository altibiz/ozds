namespace Ozds.Business.Models;

public record MaybeRepresentingUserModel(
  UserModel User,
  RepresentativeModel? Representative
);
