using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

// TODO: check representative model user id

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsAuditableMutations : IOzdsMutations
{
  private readonly OzdsDbContext _context;

  private readonly IServiceProvider _serviceProvider;

  public OzdsAuditableMutations(
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

  public void Create(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var modelEntityConverter = _serviceProvider
                                 .GetServices<IModelEntityConverter>()
                                 .FirstOrDefault(converter => converter
                                   .CanConvertToEntity(auditable.GetType())) ??
                               throw new InvalidOperationException(
                                 $"No model entity converter found for {auditable.GetType()}");
    _context.Entry(modelEntityConverter.ToEntity(auditable)).State = EntityState.Added;
  }

  public void Update(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var modelEntityConverter = _serviceProvider
                                 .GetServices<IModelEntityConverter>()
                                 .FirstOrDefault(converter => converter
                                   .CanConvertToEntity(auditable.GetType())) ??
                               throw new InvalidOperationException(
                                 $"No model entity converter found for {auditable.GetType()}");
    _context.Update(modelEntityConverter.ToEntity(auditable));
  }

  public void Delete(IAuditable auditable)
  {
    var modelEntityConverter = _serviceProvider
                                 .GetServices<IModelEntityConverter>()
                                 .FirstOrDefault(converter => converter
                                   .CanConvertToEntity(auditable.GetType())) ??
                               throw new InvalidOperationException(
                                 $"No model entity converter found for {auditable.GetType()}");
    _context.Remove(modelEntityConverter.ToEntity(auditable));
  }
}
