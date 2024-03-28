using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Data;

// TODO: check representative model user id

namespace Ozds.Business.Mutations;

public class OzdsAuditableMutations : IDisposable, IAsyncDisposable
{
  private readonly OzdsDbContext _context;

  public OzdsAuditableMutations(OzdsDbContext context)
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

  public List<ValidationResult>? Create(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    _context.Add(EntityModelTypeMapper.ToEntity(auditable));
    return null;
  }

  public List<ValidationResult>? Update(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    _context.Update(EntityModelTypeMapper.ToEntity(auditable));
    return null;
  }

  public void Delete(IAuditable auditable)
  {
    _context.Remove(EntityModelTypeMapper.ToEntity(auditable));
  }
}
