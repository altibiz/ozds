using Ozds.Business.Models;

namespace Ozds.Client.State;

public record LocationState(
  LocationModel? Location,
  Action? UnsetLocation
);
