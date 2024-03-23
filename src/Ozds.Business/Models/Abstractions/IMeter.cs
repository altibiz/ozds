using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IValidatableObject
{
  public string Id { get; }

  public string? NetworkUserMeasurementLocationId { get; }

  public string? LocationMeasurementLocationId { get; }

  public ICapabilities Capabilities { get; }
}
