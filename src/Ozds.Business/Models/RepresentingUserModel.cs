namespace Ozds.Business.Models;

public record RepresentingUserModel(
  UserModel User,
  RepresentativeModel Representative
);
