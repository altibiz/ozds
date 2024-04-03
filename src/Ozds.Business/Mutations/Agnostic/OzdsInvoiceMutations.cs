using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsInvoiceMutations : IOzdsMutations
{
  private readonly OzdsDbContext _context;

  private readonly IServiceProvider _serviceProvider;

  public OzdsInvoiceMutations(
    OzdsDbContext context,
    IServiceProvider serviceProvider
  )
  {
    _context = context;
    _serviceProvider = serviceProvider;
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

  public void Create(IInvoice invoice)
  {
    var validationResults = invoice
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var modelEntityConverter = _serviceProvider
                                 .GetServices<IModelEntityConverter>()
                                 .FirstOrDefault(converter => converter
                                   .CanConvertToEntity(invoice.GetType())) ??
                               throw new InvalidOperationException(
                                 $"No model entity converter found for {invoice.GetType()}");
    _context.Add(modelEntityConverter.ToEntity(invoice));
  }
}
