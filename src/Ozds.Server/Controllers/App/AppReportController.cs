using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;

namespace Ozds.Server.Controllers.App;

[Route("app/report")]
[Authorize]
public class AppReportController(
  ReportMutations reportMutations,
  ReportQueries reportQueries,
  LocalizationQueries localizationQueries,
  TimeQueries time
) : Controller
{
  [HttpGet]
  [Route("location-energy-card/{culture}/{locationId}/{year:int}/{month:int}")]
  public async Task<IActionResult> LocationEnergyCard(
    CultureInfo culture,
    string locationId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var energyCards = await reportQueries.ReadShortenedEnergyCardsByLocation(
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
      + end.ToString("MM-yyyy")
      + ".csv";

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
  [Route(
    "network-user-energy-card/{culture}/{networkUserId}/{year:int}/{month:int}"
  )]
  public async Task<IActionResult> NetworkUserEnergyCard(
    CultureInfo culture,
    string networkUserId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var energyCards = await reportQueries.ReadShortenedEnergyCardsByNetworkUser(
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
      + end.ToString("MM-yyyy")
      + ".csv";
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
  [Route(
    "measurement-location-accounting-period/{culture}/{measurementLocationId}/{year:int}/{month:int}"
  )]
  public async Task<IActionResult> MeasurementLocationAccountingPeriod(
    CultureInfo culture,
    string measurementLocationId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var accountingPeriod = await reportQueries.ReadAccountingPeriodReports(
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
      + end.ToString("MM-yyyy")
      + ".csv";

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
  [Route(
    "measurement-location-load-curve/{culture}/{measurementLocationId}/{obisString}/{year:int}/{month:int}"
  )]
  public async Task<IActionResult> MeasurementLocationLoadCurve(
    CultureInfo culture,
    string measurementLocationId,
    string obisString,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var obis = obisString.ToObis();

    var loadCurves = await reportQueries.ReadLoadCurveReports(
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
      + end.ToString("MM-yyyy")
      + ".csv";

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
  [Route("meter-accounting-period/{culture}/{meterId}/{year:int}/{month:int}")]
  public async Task<IActionResult> MeterAccountingPeriod(
    CultureInfo culture,
    string meterId,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var accountingPeriod =
      await reportQueries.ReadAccountingPeriodReportsByMeter(
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
      + end.ToString("MM-yyyy")
      + ".csv";

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
  [Route(
    "meter-load-curve/{culture}/{meterId}/{obisString}/{year:int}/{month:int}"
  )]
  public async Task<IActionResult> MeterLoadCurve(
    CultureInfo culture,
    string meterId,
    string obisString,
    int year,
    int month,
    CancellationToken cancellationToken
  )
  {
    var (start, end) = time.GetMonthRange(year, month);

    var obis = obisString.ToObis();

    var loadCurves = await reportQueries.ReadLoadCurveReportsByMeter(
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
      + end.ToString("MM-yyyy")
      + ".csv";

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
