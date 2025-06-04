using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Server.Controllers;

public class DataController(
  MeasurementQueries queries
) : Controller
{
  [HttpGet]
  public async Task<IActionResult> MeasurementsByMeter(
    string meterId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeterId(
      meterId,
      null,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> MeasurementsByMeasurementLocation(
    string measurementLocationId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeasurementLocationId(
      measurementLocationId,
      null,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> QuarterHourlyAggregatesByMeter(
    string meterId,
    string fromDate,
    string toDate,
    int pageNumber,
    int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeterId(
      meterId,
      IntervalModel.QuarterHour,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> QuarterHourlyAggregatesByMeasurementLocation(
    string measurementLocationId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeasurementLocationId(
      measurementLocationId,
      IntervalModel.QuarterHour,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> DailyAggregatesByMeter(
    string meterId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeterId(
      meterId,
      IntervalModel.Day,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> DailyAggregatesByMeasurementLocation(
    string measurementLocationId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeasurementLocationId(
      measurementLocationId,
      IntervalModel.Day,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> MonthlyAggregatesByMeter(
    string meterId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeterId(
      meterId,
      IntervalModel.Month,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  [HttpGet]
  public async Task<IActionResult> MonthlyAggregatesByMeasurementLocation(
    string measurementLocationId,
    string fromDate,
    string toDate,
    int pageNumber,
    [FromQuery] int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var models = await ReadByMeasurementLocationId(
      measurementLocationId,
      IntervalModel.Month,
      fromDate,
      toDate,
      pageNumber,
      pageCount,
      cancellationToken
    );

    return Json(models);
  }

  private async Task<PaginatedList<IMeasurement>> ReadByMeterId(
    string meterId,
    IntervalModel? interval,
    string fromDate,
    string toDate,
    int pageNumber,
    int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var fromDateParsed = DateTimeOffset
      .Parse(fromDate, CultureInfo.InvariantCulture);
    var toDateParsed = DateTimeOffset
      .Parse(toDate, CultureInfo.InvariantCulture);

    var models = pageCount is { } count
      ? await queries.ReadByMeterIds(
        new[] { meterId },
        interval,
        fromDateParsed,
        toDateParsed,
        pageNumber,
        cancellationToken,
        count
      )
      : await queries.ReadByMeterIds(
        new[] { meterId },
        interval,
        fromDateParsed,
        toDateParsed,
        pageNumber,
        cancellationToken
      );

    return models;
  }

  private async Task<PaginatedList<IMeasurement>> ReadByMeasurementLocationId(
    string measurementLocationId,
    IntervalModel? interval,
    string fromDate,
    string toDate,
    int pageNumber,
    int? pageCount,
    CancellationToken cancellationToken
  )
  {
    var fromDateParsed = DateTimeOffset
      .Parse(fromDate, CultureInfo.InvariantCulture);
    var toDateParsed = DateTimeOffset
      .Parse(toDate, CultureInfo.InvariantCulture);

    var models = pageCount is { } count
      ? await queries.ReadByMeasurementLocationIds(
        new[] { measurementLocationId },
        interval,
        fromDateParsed,
        toDateParsed,
        pageNumber,
        cancellationToken,
        count
      )
      : await queries.ReadByMeasurementLocationIds(
        new[] { measurementLocationId },
        interval,
        fromDateParsed,
        toDateParsed,
        pageNumber,
        cancellationToken
      );

    return models;
  }
}
