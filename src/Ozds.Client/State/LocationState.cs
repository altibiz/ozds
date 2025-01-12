using Ozds.Business.Models;

namespace Ozds.Client.State;

public record LocationState(
  LocationModel? Location,
  Func<Task> UnsetLocation
);
