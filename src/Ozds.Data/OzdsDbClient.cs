using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users.Models;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  private const int TakeLimit = 1000;

  private readonly OzdsDbContext _context;

  private readonly UserManager<User> _userManager;

  public OzdsDbClient(OzdsDbContext context, UserManager<User> userManager)
  {
    _context = context;
    _userManager = userManager;
  }

  public async Task MigrateAsync()
  {
    await _context.Database.MigrateAsync();
  }
}
