using Ozds.Business.Models.Abstractions;
using Ozds.Data;

namespace Ozds.Business.Mutations;

public partial class OzdsEventMutations
{
  private readonly OzdsDbContext _context;

  public OzdsEventMutations(OzdsDbContext context)
  {
    _context = context;
  }

  public async Task SaveChanges()
  {
    await _context.SaveChangesAsync();
  }

  public void Create(IEvent @event)
  {
    _context.Add(@event.ToDbEntity());
  }
}
