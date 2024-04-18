namespace Ozds.Business.Models.Composite;

public record LocationWithRepresentativesModel(
  LocationModel Location,
  List<RepresentativeModel> Representatives
);
