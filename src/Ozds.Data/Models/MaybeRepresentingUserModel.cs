namespace Ozds.Data.Models;

public record MaybeRepresentingUserModel(
  UserModel User,
  RepresentativeModel? Representative
);
