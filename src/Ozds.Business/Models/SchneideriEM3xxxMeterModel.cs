using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities.Implementations;
using Ozds.Business.Capabilities.Implementations.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeterModel : MeterModel
{
  [Required]
  public required string MeasurementValidatorId { get; set; }

  public override ICapabilities Capabilities
  {
    get { return new SchneideriEM3xxxCapabilities(); }
  }
}
