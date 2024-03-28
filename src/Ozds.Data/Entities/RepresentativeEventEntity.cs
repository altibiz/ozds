using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RepresentativeEventEntity : EventEntity
{
  public string RepresentativeId { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;
}

public class RepresentativeEventEntityConfiguration : EntityTypeConfiguration<RepresentativeEventEntity>
{
  public override void Configure(EntityTypeBuilder<RepresentativeEventEntity> builder)
  {
    builder
      .HasOne(nameof(RepresentativeEventEntity.Representative))
      .WithMany(nameof(RepresentativeEntity.Events))
      .HasForeignKey(nameof(RepresentativeEventEntity.RepresentativeId));
  }
}
