using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Attributes;

// TODO: figure out how to slap on all the necessary entities without
// ef core automatically making relationships

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(IssuedOn))]
[Table("invoices")]
public class InvoiceEntity : ReadonlyEntity
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
