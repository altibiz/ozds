using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("measurement_validators")]
public abstract class MeasurementValidatorEntity : SoftDeletableEntity
{
}
