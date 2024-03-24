using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record AuditableModel(
  string Id,
  string Title,
  DateTimeOffset CreationDate,
  string? CreatedById,
  DateTimeOffset? LastUpdateDate,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletionDate,
  string? DeletedById
) : IdentifiableModel(Id, Title), IAuditable;
