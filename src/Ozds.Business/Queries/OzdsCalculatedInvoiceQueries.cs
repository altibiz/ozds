using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsCalculatedInvoiceQueries : IOzdsQueries
{
  private readonly OzdsDbContext _context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter;

  public OzdsCalculatedInvoiceQueries(
      OzdsDbContext context,
      AgnosticModelEntityConverter modelEntityConverter
  )
  {
    _context = context;
    _modelEntityConverter = modelEntityConverter;
  }

  public async Task<CalculatedNetworkUserInvoiceModel?> ReadCalculatedNetworkUserInvoice(
    string id
  )
  {
    var invoice = await _context.NetworkUserInvoices
      .Where(_context.PrimaryKeyEquals<NetworkUserInvoiceEntity>(id))
      .Include(invoice => invoice.NetworkUserCalculations)
      .FirstOrDefaultAsync();
    if (invoice is null)
    {
      return null;
    }

    return new CalculatedNetworkUserInvoiceModel(
      invoice.NetworkUserCalculations
        .Select(_modelEntityConverter.ToModel<INetworkUserCalculation>)
        .ToList(),
      _modelEntityConverter.ToModel<NetworkUserInvoiceModel>(invoice)
    );
  }
}
