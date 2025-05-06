using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Business.Time;

namespace Ozds.Server.Controllers;

public class DownloadController(
  CalculatedInvoiceQueries calculatedInvoiceQueries,
  NetworkUserInvoiceIssuer networkUserInvoiceIssuer,
  DocumentMutations documentMutations,
  ReportMutations reportMutations,
  ReportQueries reportQueries,
  LocalizationQueries localizationQueries
) : Controller
{
  [HttpGet]
  public async Task<IActionResult> LocationEnergyCard(
    CultureInfo culture,
    string locationId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var energyCards = await reportQueries
      .ReadEnergyCardReportsByLocation(
        culture,
        locationId,
        start,
        end,
        cancellationToken
      );
    if (energyCards is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(culture, "location-")
      + locationId
      + localizationQueries.Translate(culture, "-energy-card-for-")
      + end.ToString("MM-yyyy") + ".csv";

    var csv = await reportMutations.Export(
      fileName,
      culture,
      energyCards,
      cancellationToken
    );

    var bytes = Encoding.UTF8.GetBytes(csv);

    return File(bytes, "text/csv", fileName);
  }

  [HttpGet]
  public async Task<IActionResult> NetworkUserEnergyCard(
    CultureInfo culture,
    string networkUserId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var energyCards = await reportQueries
      .ReadEnergyCardReportsByNetworkUser(
        culture,
        networkUserId,
        start,
        end,
        cancellationToken
      );
    if (energyCards is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(culture, "network-user-")
      + networkUserId
      + localizationQueries.Translate(culture, "-energy-card-for-")
      + end.ToString("MM-yyyy") + ".csv";
    var csv = await reportMutations.Export(
      fileName,
      culture,
      energyCards,
      cancellationToken
    );

    var bytes = Encoding.UTF8.GetBytes(csv);

    return File(bytes, "text/csv", fileName);
  }

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
      + invoice.Invoice.ToDate.ToString("MM-yyyy") + ".csv";

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
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

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
      + invoice.Invoice.ToDate.ToString("MM-yyyy") + ".csv";

    return File(pdf, "application/pdf", fileName);
  }

  [HttpGet]
  public async Task<IActionResult> MeasurementLocationAccountingPeriod(
    CultureInfo culture,
    string measurementLocationId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var accountingPeriod = await reportQueries
      .ReadAccountingPeriodReports(
        culture,
        measurementLocationId,
        start,
        end,
        cancellationToken
      );
    if (accountingPeriod is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(culture, "measurement-location-")
      + measurementLocationId
      + localizationQueries.Translate(culture, "-accounting-period-for-")
      + end.ToString("MM-yyyy") + ".csv";

    var csv = await reportMutations.Export(
      fileName,
      culture,
      accountingPeriod,
      cancellationToken
    );

    var bytes = Encoding.UTF8.GetBytes(csv);

    return File(bytes, "text/csv", fileName);
  }

  [HttpGet]
  public async Task<IActionResult> MeasurementLocationLoadCurve(
    CultureInfo culture,
    string measurementLocationId,
    string obisString,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var obis = obisString.ToObis();

    var loadCurves = await reportQueries
      .ReadLoadCurveReports(
        culture,
        measurementLocationId,
        obis,
        start,
        end,
        cancellationToken
      );
    if (loadCurves is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(culture, "measurement-location-")
      + measurementLocationId
      + localizationQueries.Translate(culture, "-load-curve-for-")
      + end.ToString("MM-yyyy") + ".csv";

    var csv = await reportMutations.Export(
      fileName,
      culture,
      loadCurves,
      cancellationToken
    );

    var bytes = Encoding.UTF8.GetBytes(csv);

    return File(bytes, "text/csv", fileName);
  }

  [HttpGet]
  public async Task<IActionResult> MeterAccountingPeriod(
    CultureInfo culture,
    string meterId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var accountingPeriod = await reportQueries
      .ReadAccountingPeriodReportsByMeter(
        culture,
        meterId,
        start,
        end,
        cancellationToken
      );
    if (accountingPeriod is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(culture, "meter-")
      + meterId
      + localizationQueries.Translate(culture, "-accounting-period-for-")
      + end.ToString("MM-yyyy") + ".csv";

    var csv = await reportMutations.Export(
      fileName,
      culture,
      accountingPeriod,
      cancellationToken
    );

    var bytes = Encoding.UTF8.GetBytes(csv);

    return File(bytes, "text/csv", fileName);
  }

  [HttpGet]
  public async Task<IActionResult> MeterLoadCurve(
    CultureInfo culture,
    string meterId,
    string obisString,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var obis = obisString.ToObis();

    var loadCurves = await reportQueries
      .ReadLoadCurveReportsByMeter(
        culture,
        meterId,
        obis,
        start,
        end,
        cancellationToken
      );
    if (loadCurves is null)
    {
      return NotFound();
    }

    var fileName =
      localizationQueries.Translate(culture, "meter-")
      + meterId
      + localizationQueries.Translate(culture, "-load-curve-for-")
      + end.ToString("MM-yyyy") + ".csv";

    var csv = await reportMutations.Export(
      fileName,
      culture,
      loadCurves,
      cancellationToken
    );

    var bytes = Encoding.UTF8.GetBytes(csv);

    return File(bytes, "text/csv", fileName);
  }
}
