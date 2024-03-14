using Microsoft.EntityFrameworkCore;

namespace Ozds.Data;

public partial class OzdsDbClient(OzdsDbContext context)
{
  public async Task MigrateAsync()
  {
    await context.Database.MigrateAsync();
  }
}
