using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users;
using Ozds.Data;

namespace Ozds.Business.Mutations;

public partial class OzdsRelationalMutations
{
  private readonly OzdsDbContext _context;

  private readonly UserManager<IUser> _userManager;

  private readonly YesSql.ISession _session;

  public OzdsRelationalMutations(OzdsDbContext context, UserManager<IUser> userManager, YesSql.ISession session)
  {
    _context = context;
    _userManager = userManager;
    _session = session;

    _context.Database.Migrate();
  }
}
