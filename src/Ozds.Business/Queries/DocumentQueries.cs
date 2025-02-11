using Ozds.Business.Conversion;
using Ozds.Business.Queries.Abstractions;
using Ozds.Document.Entities;
using DocumentDocumentQueries = Ozds.Document.Queries.DocumentQueries;

namespace Ozds.Business.Queries;

public class DocumentQueries(
  DocumentDocumentQueries documentQueries,
  CalculatedInvoiceQueries invoiceQueries,
  ModelDocumentEntityConverter converter
) : IQueries
{
  public async Task<string?> ReadHtmlForNetworkUserInvoice(
    string id,
    CancellationToken cancellationToken
  )
  {
    var model = await invoiceQueries.ReadCalculatedNetworkUserInvoice(
      id,
      cancellationToken
    );
    if (model is null)
    {
      return null;
    }

    var entity = new CalculatedNetworkUserInvoiceEntity
    {
      Calculations = model.Calculations
        .Select(converter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      Invoice = converter.ToEntity<NetworkUserInvoiceEntity>(model.Invoice)
    };

    var html = await documentQueries.ReadHtmlForNetworkUserInvoice(
      entity,
      cancellationToken
    );
    return html;
  }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> ReadPdfForNetworkUserInvoice(
    string id,
    CancellationToken cancellationToken
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    var model = await invoiceQueries.ReadCalculatedNetworkUserInvoice(
      id,
      cancellationToken
    );
    if (model is null)
    {
      return null;
    }

    var entity = new CalculatedNetworkUserInvoiceEntity
    {
      Calculations = model.Calculations
        .Select(converter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      Invoice = converter.ToEntity<NetworkUserInvoiceEntity>(model.Invoice)
    };

    var pdf = await documentQueries.ReadPdfForNetworkUserInvoice(
      entity,
      cancellationToken
    );
    return pdf;
  }
}
