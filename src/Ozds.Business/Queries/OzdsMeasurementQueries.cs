using Ozds.Data;

namespace Ozds.Business.Queries;

public class OzdsMeasurementQueries
{
  private readonly OzdsDbContext _context;

  public OzdsMeasurementQueries(OzdsDbContext context)
  {
    _context = context;
  }
}
