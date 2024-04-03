using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsInvoiceMutations : IOzdsMutations
{
  private readonly OzdsDbContext _context;

  public OzdsInvoiceMutations(OzdsDbContext context)
  {
    _context = context;
  }

  public async ValueTask DisposeAsync()
  {
    await _context.SaveChangesAsync();
    GC.SuppressFinalize(this);
  }

  public void Dispose()
  {
    _context.SaveChanges();
    GC.SuppressFinalize(this);
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
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

    _context.Add(EntityModelTypeMapper.ToEntity(invoice));
    return null;
  }
}
