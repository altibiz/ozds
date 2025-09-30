using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Base;

public abstract class AuditableJoinEntity : JoinEntity, IAuditableEntity
{
  public const string AuditingIdSeparator = ":";

  public string AuditingId
  {
    get { return LeftId + AuditingIdSeparator + RightId; }
  }

  public string AuditingTitle
  {
    get { return $"{GetType().Name} {AuditingId}"; }
  }

  public DateTimeOffset CreatedOn { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public string? CreatedById { get; set; }

  public virtual RepresentativeEntity? CreatedBy { get; set; }

  public string? AuditingRepresentativeId { get; set; }
}

public class AuditableJoinEntityConfiguration
  : EntityTypeHierarchyConfiguration<AuditableJoinEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.Ignore(nameof(AuditableJoinEntity.AuditingId));
    builder.Ignore(nameof(AuditableJoinEntity.AuditingTitle));

    builder
      .HasOne(nameof(AuditableJoinEntity.CreatedBy))
      .WithMany()
      .HasForeignKey(nameof(AuditableJoinEntity.CreatedById));

    builder.Ignore(nameof(AuditableJoinEntity.AuditingRepresentativeId));
  }
}
