using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Capabilities.Implementations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class AbbB2xMeterModel : MeterModel
{
  [Required]
  public required string MeasurementValidatorId { get; set; }

  public override ICapabilities Capabilities
  {
    get { return new AbbB2xCapabilities(); }
  }
}
