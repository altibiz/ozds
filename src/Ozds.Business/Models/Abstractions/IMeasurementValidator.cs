using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementValidator : IValidatableObject
{
  public string MeterId { get; }
}
