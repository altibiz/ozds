using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsInvoiceMutations : IOzdsMutations
{
  private readonly OzdsDbContext _context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter;

  public OzdsInvoiceMutations(
    OzdsDbContext context,
    AgnosticModelEntityConverter modelEntityConverter
  )
  {
    _context = context;
    _modelEntityConverter = modelEntityConverter;
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

  public async Task<string> Create(IInvoice invoice)
  {
    var validationResults = invoice
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var entity = _modelEntityConverter.ToEntity(invoice)
      as IIdentifiableEntity
      ?? throw new InvalidOperationException(
        $"Failed to convert {nameof(invoice)} to entity.");
    _context.Add(entity);
    await _context.SaveChangesAsync();
    return entity.Id;
  }
}
