using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Time;

namespace Ozds.Server.Controllers;

public class DownloadController(
  CalculatedInvoiceQueries calculatedInvoiceQueries,
  INetworkUserInvoiceIssuer networkUserInvoiceIssuer,
  DocumentQueries documentQueries
) : Controller
{
  [HttpGet]
  public async Task<IActionResult> NetworkUserInvoice(
    string id,
    CancellationToken cancellationToken
  )
  {
    var invoice = await calculatedInvoiceQueries
      .ReadCalculatedNetworkUserInvoice(
        id,
        cancellationToken
      );
    if (invoice is null)
    {
      return NotFound();
    }

    var pdf = await documentQueries.ReadPdfForNetworkUserInvoice(
      invoice,
      cancellationToken
    );
    if (pdf is null)
    {
      return NotFound();
    }

    return File(
      pdf,
      "application/pdf",
      $"{invoice.Invoice.Title}.pdf"
    );
  }

  [HttpGet]
  public async Task<IActionResult> NetworkUserInvoicePreview(
    string networkUserId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var invoice = await networkUserInvoiceIssuer
      .PreviewNetworkUserInvoiceAsync(
        networkUserId,
        start,
        end,
        cancellationToken
      );

    var pdf = await documentQueries.ReadPdfForNetworkUserInvoice(
      invoice,
      cancellationToken
    );
    if (pdf is null)
    {
      return NotFound();
    }

    return File(
      pdf,
      "application/pdf",
      $"{invoice.Invoice.Title}.pdf"
    );
  }
}
