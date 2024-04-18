namespace Ozds.Business.Models.Composite;

public record NetworkUserWithRepresentativesModel(
  NetworkUserModel NetworkUser,
  List<RepresentativeModel> Representatives
);
