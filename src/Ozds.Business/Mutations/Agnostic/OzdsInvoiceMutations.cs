using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsInvoiceMutations(
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IMutations
{
  private readonly DataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public Task SaveChangesAsync()
  {
    return _context.SaveChangesAsync();
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
  }

  public void Create(IInvoice invoice)
  {
    var validationResults = invoice
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    _context.Add(_modelEntityConverter.ToEntity(invoice));
  }

  public async Task<string> CreateId(IInvoice invoice)
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
