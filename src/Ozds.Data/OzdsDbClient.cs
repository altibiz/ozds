using Microsoft.EntityFrameworkCore;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  private readonly OzdsDbContext _context;

  public OzdsDbClient(OzdsDbContext context)
  {
    _context = context;
  }

  public async Task MigrateAsync()
  {
    await _context.Database.MigrateAsync();
  }
}
