using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models;
using Ozds.Data;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsNetworkUserQueries
{
  protected readonly OzdsDbContext context;

  public OzdsNetworkUserQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<NetworkUserModel?>
    NetworkUserById(string id)
  {
    var networkUser =
      await context.NetworkUsers
        .WithId(id)
        .FirstOrDefaultAsync();
    if (networkUser is not null)
    {
      return networkUser.ToModel();
    }
    else
    {
      return null;
    }
  }
}
