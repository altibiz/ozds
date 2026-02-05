using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Base;

public abstract class TrackableEntity
  : IdentifiableEntity,
    ITrackableIdentifiableEntity
{
  public virtual RepresentativeEntity? DeletedBy { get; set; }

  public string AuditingId
  {
    get { return Id; }
  }

  public string AuditingTitle
  {
    get { return Title; }
  }

  public DateTimeOffset CreatedOn { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public string? CreatedById { get; set; }

  public virtual RepresentativeEntity? CreatedBy { get; set; }

  public DateTimeOffset? LastUpdatedOn { get; set; }

  public string? LastUpdatedById { get; set; }

  public virtual RepresentativeEntity? LastUpdatedBy { get; set; }

  public bool IsDeleted { get; set; }

  public DateTimeOffset? DeletedOn { get; set; }

  public string? DeletedById { get; set; }

  public bool Restore { get; set; } = false;

  public bool Forget { get; set; } = false;

  public string? AuditingRepresentativeId { get; set; }
}

public class TrackableEntityConfiguration
  : EntityTypeHierarchyConfiguration<TrackableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .HasOne(nameof(TrackableEntity.CreatedBy))
      .WithMany()
      .HasForeignKey(nameof(TrackableEntity.CreatedById));

    builder
      .HasOne(nameof(TrackableEntity.LastUpdatedBy))
      .WithMany()
      .HasForeignKey(nameof(TrackableEntity.LastUpdatedById));

    builder
      .HasOne(nameof(TrackableEntity.DeletedBy))
      .WithMany()
      .HasForeignKey(nameof(TrackableEntity.DeletedById));

    builder.Ignore(nameof(TrackableEntity.Restore));
    builder.Ignore(nameof(TrackableEntity.Forget));
    builder.Ignore(nameof(TrackableEntity.AuditingRepresentativeId));
  }
}
