using Ozds.Business.Models;

namespace Ozds.Business.Mutations;

public partial class OzdsRelationalMutations
{
  public async Task CreateRepresentative(RepresentativeModel model)
  {
    await _context.Representatives.AddAsync(model.ToEntity());
  }

  public async Task UpdateRepresentative(RepresentativeModel model)
  {
    await _context.Representatives.SingleUpdateAsync(model.ToEntity());
  }

  public async Task DeleteRepresentative(string id)
  {
    await _context.Representatives.DeleteByKeyAsync(id);
  }
}
