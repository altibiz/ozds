using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;

namespace Ozds.Server.Controllers;

public class DocumentController(
  CalculatedInvoiceQueries calculatedInvoiceQueries,
  NetworkUserInvoiceIssuer networkUserInvoiceIssuer,
  DocumentMutations documentMutations,
  LocalizationQueries localizationQueries,
  TimeQueries time
) : Controller
{
  [HttpGet]
  public async Task<IActionResult> NetworkUserInvoice(
    string id,
    CancellationToken cancellationToken
  )
  {
    var invoice =
      await calculatedInvoiceQueries.ReadCalculatedNetworkUserInvoice(
        id,
        cancellationToken
      );
    if (invoice is null)
    {
      return NotFound();
    }

    var pdf = await documentMutations.CreatePdfForNetworkUserInvoice(
      invoice,
      cancellationToken
    );
    if (pdf is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(
        localizationQueries.CroatianCulture, "network-user-")
      + invoice.Invoice.NetworkUserId
      + localizationQueries.Translate(
        localizationQueries.CroatianCulture, "-invoice-for-")
      + invoice.Invoice.ToDate.ToString("MM-yyyy") + ".pdf";

    return File(pdf, "application/pdf", fileName);
  }

  [HttpGet]
  public async Task<IActionResult> NetworkUserInvoicePreview(
    string networkUserId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var invoice = await networkUserInvoiceIssuer.PreviewNetworkUserInvoiceAsync(
      networkUserId,
      start,
      end,
      cancellationToken
    );

    var pdf = await documentMutations.CreatePdfForNetworkUserInvoice(
      invoice,
      cancellationToken
    );
    if (pdf is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(
        localizationQueries.CroatianCulture, "network-user-")
      + invoice.Invoice.NetworkUserId
      + localizationQueries.Translate(
        localizationQueries.CroatianCulture, "-invoice-preview-for-")
      + invoice.Invoice.ToDate.ToString("MM-yyyy") + ".pdf";

    return File(pdf, "application/pdf", fileName);
  }
}
