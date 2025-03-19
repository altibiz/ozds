using System.Text;
using GeoAPI.DataStructures;
using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Export;

namespace Ozds.Server.Controllers;

public class DownloadController(
  CalculatedInvoiceQueries calculatedInvoiceQueries,
  MeasurementQueries measurementQueries,
  MeasurementLocationQueries measurementLocationQueries,
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
    var invoice =
      await calculatedInvoiceQueries.ReadCalculatedNetworkUserInvoice(
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

    return File(pdf, "application/pdf", $"{invoice.Invoice.Title}.pdf");
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

    var pdf = await documentQueries.ReadPdfForNetworkUserInvoice(
      invoice,
      cancellationToken
    );
    if (pdf is null)
    {
      return NotFound();
    }

    return File(pdf, "application/pdf", $"{invoice.Invoice.Title}.pdf");
  }

  [HttpGet]
  public async Task<IActionResult> NetworkUserMonthlyAggregates(
    string networkUserId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);

    var measurementLocations =
      await measurementLocationQueries.ReadMeasurementLocationByNetworkUser(
        networkUserId,
        cancellationToken
      );

    if (measurementLocations.Count == 0)
    {
      return NotFound();
    }

    var measures = await measurementQueries.ReadByMeasurementLocationIds(
      measurementLocations.Select(x => x.Id).ToList(),
      IntervalModel.Month,
      start,
      end,
      0,
      cancellationToken
    );
    var orderedMeasures = measures.Items.OrderBy(x => x.Timestamp).ToList();
    var measurements = orderedMeasures.Select(x => (IAggregate)x).ToList();

    return GenerateCsv(
      measurements,
      false,
      networkUserId + "-monthly-aggregate-" + end.ToString("MM-yyyy") + ".csv"
    );
  }

  [HttpGet]
  public async Task<IActionResult> LocationMonthlyAggregates(
    string locationId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);
    var measurementResolution = 30;

    var measurementLocations =
      await measurementLocationQueries.ReadMeasurementLocationByLocation(
        locationId,
        cancellationToken
      );

    if (measurementLocations.Count == 0)
    {
      return NotFound();
    }

    var measures = await measurementQueries.ReadByMeasurementLocationIds(
      measurementLocations.Select(x => x.Id).ToList(),
      IntervalModel.Month,
      start,
      end,
      0,
      cancellationToken
    );
    var orderedMeasures = measures.Items.OrderBy(x => x.Timestamp).ToList();
    var measurements = orderedMeasures.Select(x => (IAggregate)x).ToList();

    return GenerateCsv(
      measurements,
      false,
      locationId + "-monthly-aggregate-" + end.ToString("MM-yyyy") + ".csv"
    );
  }

  [HttpGet]
  public async Task<IActionResult> MeterQuarterHourlyAggregatesForMonth(
    string meterId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);
    var countInPage = 5000;

    var measures = await measurementQueries.ReadByMeterIds(
      new[] { meterId },
      IntervalModel.QuarterHour,
      start,
      end,
      0,
      cancellationToken,
      countInPage
    );
    var measurements = measures.Items.OrderBy(x => x.Timestamp).ToList();

    return GenerateCsv(
      measurements,
      true,
      meterId + "-" + end.ToString("MM-yyyy") + ".csv"
    );
  }

  [HttpGet]
  public async Task<IActionResult> MeasurementLocationQuarterHourlyAggregatesForMonth(
    string measurementLocationId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = DateTimeOffsetExtensions.GetMonthRange(year, month);
    var countInPage = 5000;

    var measures = await measurementQueries.ReadByMeasurementLocationIds(
      new[] { measurementLocationId },
      IntervalModel.QuarterHour,
      start,
      end,
      0,
      cancellationToken,
      countInPage
    );
    var orderedMeasures = measures.Items.OrderBy(x => x.Timestamp).ToList();
    var measurements = orderedMeasures.Select(x => (IAggregate)x).ToList();

    return GenerateCsv(
      measurements,
      false,
      measurementLocationId + "-" + end.ToString("MM-yyyy") + ".csv"
    );
  }

  public FileContentResult GenerateCsv(
    IEnumerable<object> models,
    bool isGeneric = true,
    string fileName = "export.csv"
  )
  {
    var exporter =
      HttpContext.RequestServices.GetRequiredService<CsvExporter>();
    string csv;
    if (models is List<IAggregate> aggregates)
    {
      var newList = aggregates.Select(x => exporter.ToCalculationBasis(x));
      csv = exporter.ExportGeneric(newList);
    }
    else if (!isGeneric)
    {
      csv = exporter.Export(models);
    }
    else
    {
      csv = exporter.ExportGeneric(models);
    }

    var bytes = Encoding.UTF8.GetBytes(csv);
    return File(bytes, "text/csv", fileName);
  }
}
