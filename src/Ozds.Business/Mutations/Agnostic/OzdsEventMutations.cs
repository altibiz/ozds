using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsEventMutations : IOzdsMutations
{
  private readonly OzdsDbContext _context;

  public OzdsEventMutations(OzdsDbContext context)
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

  public void Create(IEvent @event)
  {
    _context.Add(EntityModelTypeMapper.ToEntity(@event));
  }
}
