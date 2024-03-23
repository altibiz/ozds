namespace Ozds.Business.Models.Abstractions;

public abstract record AuditableModel(
  string Id,
  DateTimeOffset CreationDate,
  string CreatedById,
  DateTimeOffset? LastUpdateDate,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletionDate,
  string? DeletedBy
) : IdModel(Id);
