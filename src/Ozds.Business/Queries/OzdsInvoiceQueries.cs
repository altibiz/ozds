using Ozds.Data;

namespace Ozds.Business.Queries;

public partial class OzdsInvoiceQueries
{
  private readonly OzdsDbContext _context;

  public OzdsInvoiceQueries(OzdsDbContext context)
  {
    _context = context;
  }
}

