namespace Ozds.Data.Models;

public record RepresentingUserModel(
  UserModel User,
  RepresentativeModel Representative
);
