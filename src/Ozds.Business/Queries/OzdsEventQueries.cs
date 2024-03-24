using Ozds.Data;

namespace Ozds.Business.Queries;

public partial class OzdsEventQueries
{
  private readonly OzdsDbContext _context;

  public OzdsEventQueries(OzdsDbContext context)
  {
    _context = context;
  }
}

