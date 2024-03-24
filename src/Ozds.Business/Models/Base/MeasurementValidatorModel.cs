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
  Id: Id,
  Title: Title,
  CreatedOn: CreatedOn,
  CreatedById: CreatedById,
  LastUpdatedOn: LastUpdatedOn,
  LastUpdatedById: LastUpdatedById,
  IsDeleted: IsDeleted,
  DeletedOn: DeletedOn,
  DeletedById: DeletedById
), IMeasurementValidator<T>
  where T : IMeasurement
{
  public abstract string? Validate(T measurement, string property);
}
