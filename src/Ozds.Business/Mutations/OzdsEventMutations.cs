using Ozds.Business.Models.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations;

public class OzdsEventMutations : IDisposable, IAsyncDisposable
{
  private readonly OzdsDbContext _context;

  public OzdsEventMutations(OzdsDbContext context)
  {
    _context = context;
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
  }

  public void Dispose()
  {
    _context.SaveChanges();
    GC.SuppressFinalize(this);
  }

  public async ValueTask DisposeAsync()
  {
    await _context.SaveChangesAsync();
    GC.SuppressFinalize(this);
  }

  public void Create(IEvent @event)
  {
    _context.Add(EntityModelTypeMapper.ToEntity(@event));
  }
}
