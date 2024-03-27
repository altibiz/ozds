using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record MeasurementValidatorModel<T>(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string MeterId
) : AuditableModel(
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById
), IMeasurementValidator<T>
  where T : IMeasurement
{
  public virtual string? Validate(T measurement, string property)
  {
    return null;
  }
}
