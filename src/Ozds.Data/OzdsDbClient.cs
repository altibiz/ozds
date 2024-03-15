using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  private const int TakeLimit = 1000;

  private readonly OzdsDbContext _context;

  private readonly UserManager<IUser> _userManager;

  public OzdsDbClient(OzdsDbContext context, UserManager<IUser> userManager)
  {
    _context = context;
    _userManager = userManager;

    _context.Database.Migrate();
  }
}
