using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class NetworkUserMeasurementLocationEntity : MeasurementLocationEntity
{
  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;
}
