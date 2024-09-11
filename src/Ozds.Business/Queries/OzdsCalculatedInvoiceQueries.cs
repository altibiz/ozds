using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsCalculatedInvoiceQueries(
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IOzdsQueries
{
  private readonly DataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<CalculatedNetworkUserInvoiceModel?>
    ReadCalculatedNetworkUserInvoice(
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
