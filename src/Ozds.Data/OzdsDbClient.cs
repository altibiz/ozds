using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Entities;

namespace Ozds.Data;

public partial class OzdsDbClient(OzdsDbContext context)
{
  public async Task MigrateAsync()
{
  await context.Database.MigrateAsync();
}
}
