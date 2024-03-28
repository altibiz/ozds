using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations;

public class OzdsMeasurementMutations : IDisposable, IAsyncDisposable
{
  private readonly OzdsDbContext _context;

  public OzdsMeasurementMutations(OzdsDbContext context)
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
