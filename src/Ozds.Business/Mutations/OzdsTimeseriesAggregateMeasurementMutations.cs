using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models;

namespace Ozds.Business.Mutations;

public partial class OzdsTimeseriesMutations
{
  public async Task CreateAbbB2xMeasurement(
    AbbB2xMeasurementModel model
  )
  {
    var validationResults = model.Validate(new ValidationContext(this));
  }
}
