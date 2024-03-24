using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations;

public partial class OzdsInvoiceMutations
{
  private readonly OzdsDbContext _context;

  public OzdsInvoiceMutations(OzdsDbContext context)
  {
    _context = context;
  }

  public async Task SaveChanges()
  {
    await _context.SaveChangesAsync();
  }

  public List<ValidationResult>? Create(IInvoice invoice)
  {
    var validationResults = invoice
      .Validate(new ValidationContext(invoice))
      .ToList();
    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    _context.Add(invoice.ToDbEntity());
    return null;
  }
}
