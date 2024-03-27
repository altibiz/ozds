using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

// TODO: settings for the messenger

namespace Ozds.Data.Entities;

public class MessengerEntity : AuditableEntity
{
  [ForeignKey(nameof(Location))]
  [Column(TypeName = "bigint")]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;

  public virtual ICollection<MessengerEventEntity> Events { get; set; } =
    default!;
}

public class MessengerEntityTypeConfiguration : EntityTypeConfiguration<MessengerEntity>
{
  public override void Configure(EntityTypeBuilder<MessengerEntity> builder)
  {
    builder.HasMany(x => x.Meters)
      .WithOne(x => x.Messenger);
  }
}
