using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsMeasurementMutations(
  OzdsDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IOzdsMutations
{
  private readonly OzdsDbContext _context = context;

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

  public void Create(IMeasurement measurement)
  {
    var validationResults = measurement
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    _context
      .Upsert(_modelEntityConverter.ToEntity(measurement))
      .NoUpdate();
  }
}
