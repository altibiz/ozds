using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users;
using Ozds.Data;
using ISession = YesSql.ISession;

namespace Ozds.Business.Queries;

public class OzdsTimeseriesQueries
{
  private readonly OzdsDbContext _context;

  private readonly ISession _session;

  private readonly UserManager<IUser> _userManager;

  public OzdsTimeseriesQueries(OzdsDbContext context,
    UserManager<IUser> userManager, ISession session)
  {
    _context = context;
    _userManager = userManager;
    _session = session;

    _context.Database.Migrate();
  }
}
