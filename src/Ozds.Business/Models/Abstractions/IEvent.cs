using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IEvent : IValidatableObject
{
  public string Id { get; }

  public DateTimeOffset Timestamp { get; }

  public LevelModel Level { get; }

  public string Description { get; }
}
