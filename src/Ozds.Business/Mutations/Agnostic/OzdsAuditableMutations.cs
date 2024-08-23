using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;
using Ozds.Data.Extensions;

// TODO: check representative model user id

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsAuditableMutations(
  OzdsDataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IOzdsMutations
{
  private readonly OzdsDataDbContext _context = context;

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

  public void Create(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    _context.AddTracked(_modelEntityConverter.ToEntity(auditable));
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

    _context.UpdateTracked(_modelEntityConverter.ToEntity(auditable));
  }

  public void Delete(IAuditable auditable)
  {
    _context.DeleteTracked(_modelEntityConverter.ToEntity(auditable));
  }
}
