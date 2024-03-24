using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public record EventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description
) : IEvent;
