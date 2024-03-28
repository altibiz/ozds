using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RepresentativeAuditEventEntity : AuditEventEntity
{
  public string RepresentativeId { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;
}

public class
  RepresentativeAuditEventEntityConfiguration : EntityTypeConfiguration<
  RepresentativeAuditEventEntity>
{
  public override void Configure(
    EntityTypeBuilder<RepresentativeAuditEventEntity> builder)
  {
    builder
      .HasOne(nameof(RepresentativeAuditEventEntity.Representative))
      .WithMany(nameof(RepresentativeEntity.AuditEvents))
      .HasForeignKey(nameof(RepresentativeAuditEventEntity.RepresentativeId));
  }
}
