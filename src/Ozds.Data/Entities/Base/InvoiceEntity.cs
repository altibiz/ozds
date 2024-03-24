using System.ComponentModel.DataAnnotations.Schema;

// TODO: figure out how to slap on all the necessary entities without
// ef core automatically making relationships

namespace Ozds.Data.Entities.Base;

[Table("invoices")]
public class InvoiceEntity : ReadonlyEntity
{
}
