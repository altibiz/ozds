using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

// TODO: figure out how to slap on all the necessary entities without
// ef core automatically making relationships
// TODO: use complex properties

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(IssuedOn))]
public abstract class InvoiceEntity : ReadonlyEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Key] public string Id { get; set; } = default!;

  [Required]
  public DateTimeOffset IssuedOn
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }

  [ForeignKey(nameof(IssuedBy))] public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;
}

public class InvoiceEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<InvoiceEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.UseTpcMappingStrategy();
  }
}
