using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations;

public partial class OzdsMeasurementMutations
{
  private readonly OzdsDbContext _context;

  public OzdsMeasurementMutations(OzdsDbContext context)
  {
    _context = context;
  }

  public async Task SaveChanges()
  {
    await _context.SaveChangesAsync();
  }

  public List<ValidationResult>? Create(IMeasurement measurement)
  {
    var validationResults = measurement
      .Validate(new ValidationContext(measurement))
      .ToList();
    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    _context.Add(EntityModelTypeMapper.ToEntity(measurement));
    return null;
  }
}
