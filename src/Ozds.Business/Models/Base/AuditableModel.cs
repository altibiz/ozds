using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record AuditableModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById
) : IdentifiableModel(
  Id,
  Title
), IAuditable;
