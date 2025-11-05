using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Authorization;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Reflection;

namespace Ozds.Server.Controllers.Api.V1;

[Route("api/v1/measurements")]
public class ApiV1MeasurementsController(
  ModelReflector modelReflector,
  MeasurementLocationQueries measurementLocationQueries,
  MeasurementQueries measurementQueries
) : ApiV1ControllerBase
{
  [HttpGet]
  [Route("quarter-hourly-aggregates-by-location/{locationId}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(
    typeof(QuarterHourlyAggregatesByLocationResponse),
    StatusCodes.Status200OK,
    "application/json")]
  public async Task<IActionResult> QuarterHourlyAggregatesByLocation(
    [FromRoute] string locationId,
    [FromQuery] DateTimeOffset? dateFrom,
    [FromQuery] DateTimeOffset? dateTo,
    [FromQuery] int? page,
    CancellationToken cancellationToken
  )
  {
    var auth = HttpContext.Items[ApiKeyAuthAttribute.ApiKeyAuthItemKey]
      as ApiKeyAuthModel;
    if (auth is null)
    {
      return Unauthorized();
    }

    var scope = auth.Scopes
      .FirstOrDefault(
        scope =>
          scope.ScopeModelType == modelReflector
            .ResolveModelName(typeof(LocationModel))
          && scope.ScopeModelId == locationId);
    if (scope is null)
    {
      return Unauthorized();
    }

    if (!auth.Registers.TryGetValue(scope.Id, out var registers))
    {
      return Unauthorized();
    }

    var measurementLocations = await measurementLocationQueries
      .ReadByLocationId(
        locationId,
        cancellationToken
      );
    if (measurementLocations.Count == 0)
    {
      return Json(
        new QuarterHourlyAggregatesByLocationResponse(
          dateFrom,
          dateTo,
          page,
          QueryConstants.DefaultMeasurementPageCount,
          0,
          locationId,
          new List<QuarterHourlyAggregatesByLocationResponseMeasurement>()
        ));
    }

    if (dateFrom is null || dateTo is null || page is null)
    {
      var lastMeasurements = await measurementQueries
        .ReadByMeasurementLocationIdsLast(
          measurementLocations.Select(x => x.Id),
          cancellationToken,
          IntervalModel.QuarterHour
        );

      var lastMeasurementsJson =
        new List<QuarterHourlyAggregatesByLocationResponseMeasurement>();
      foreach (var measurement in lastMeasurements)
      {
        var measurementJson =
          new QuarterHourlyAggregatesByLocationResponseMeasurement(
            measurement.Timestamp,
            measurement.MeterId,
            measurement.MeasurementLocationId,
            new Dictionary<string, string>()
          );
        foreach (var register in registers)
        {
          var registerValue = measurement.RegisterValue(register);
          var registerName = register.Name;
          measurementJson.Registers.Add(registerName, registerValue.ToString());
        }

        lastMeasurementsJson.Add(measurementJson);
      }

      return Json(
        new QuarterHourlyAggregatesByLocationResponse(
          dateFrom,
          dateTo,
          page,
          QueryConstants.DefaultMeasurementPageCount,
          lastMeasurements.Count,
          locationId,
          lastMeasurementsJson
        ));
    }

    var pageMeasurements = await measurementQueries
      .ReadByMeasurementLocationIds(
        measurementLocations.Select(x => x.Id),
        IntervalModel.QuarterHour,
        dateFrom.Value,
        dateTo.Value,
        page.Value,
        cancellationToken
      );

    var pagedMeasurementsJson =
      new List<QuarterHourlyAggregatesByLocationResponseMeasurement>();
    foreach (var measurement in pageMeasurements.Items)
    {
      var measurementJson =
        new QuarterHourlyAggregatesByLocationResponseMeasurement(
          measurement.Timestamp,
          measurement.MeterId,
          measurement.MeasurementLocationId,
          new Dictionary<string, string>()
        );
      foreach (var register in registers)
      {
        var registerValue = measurement.RegisterValue(register);
        var registerName = register.Name;
        measurementJson.Registers.Add(registerName, registerValue.ToString());
      }

      pagedMeasurementsJson.Add(measurementJson);
    }

    return Json(
      new QuarterHourlyAggregatesByLocationResponse(
        dateFrom,
        dateTo,
        page,
        QueryConstants.DefaultMeasurementPageCount,
        pageMeasurements.TotalCount,
        locationId,
        pagedMeasurementsJson
      ));
  }

  private sealed record QuarterHourlyAggregatesByLocationResponse(
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    int? Page,
    int? PageSize,
    int? TotalCount,
    string LocationId,
    IList<QuarterHourlyAggregatesByLocationResponseMeasurement> Measurements
  );

  private sealed record QuarterHourlyAggregatesByLocationResponseMeasurement(
    DateTimeOffset Timestamp,
    string MeterId,
    string MeasurementLocationId,
    IDictionary<string, string> Registers
  );

  [HttpGet]
  [Route("quarter-hourly-aggregates-by-network-user/{networkUserId}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(
    typeof(QuarterHourlyAggregatesByNetworkUserResponse),
    StatusCodes.Status200OK,
    "application/json")]
  public async Task<IActionResult> QuarterHourlyAggregatesByNetworkUser(
    [FromRoute] string networkUserId,
    [FromQuery] DateTimeOffset? dateFrom,
    [FromQuery] DateTimeOffset? dateTo,
    [FromQuery] int? page,
    CancellationToken cancellationToken
  )
  {
    var auth = HttpContext.Items[ApiKeyAuthAttribute.ApiKeyAuthItemKey]
      as ApiKeyAuthModel;
    if (auth is null)
    {
      return Unauthorized();
    }

    var scope = auth.Scopes
      .FirstOrDefault(
        scope =>
          scope.ScopeModelType == modelReflector
            .ResolveModelName(typeof(NetworkUserModel))
          && scope.ScopeModelId == networkUserId);
    if (scope is null)
    {
      return Unauthorized();
    }

    if (!auth.Registers.TryGetValue(scope.Id, out var registers))
    {
      return Unauthorized();
    }

    var measurementLocations = await measurementLocationQueries
      .ReadByNetworkUserId(
        networkUserId,
        cancellationToken
      );
    if (measurementLocations.Count == 0)
    {
      return Json(
        new QuarterHourlyAggregatesByNetworkUserResponse(
          dateFrom,
          dateTo,
          page,
          QueryConstants.DefaultMeasurementPageCount,
          0,
          networkUserId,
          new List<QuarterHourlyAggregatesByNetworkUserResponseMeasurement>()
        ));
    }

    if (dateFrom is null || dateTo is null || page is null)
    {
      var lastMeasurements = await measurementQueries
        .ReadByMeasurementLocationIdsLast(
          measurementLocations.Select(x => x.Id),
          cancellationToken,
          IntervalModel.QuarterHour
        );

      var lastMeasurementsJson =
        new List<QuarterHourlyAggregatesByNetworkUserResponseMeasurement>();
      foreach (var measurement in lastMeasurements)
      {
        var measurementJson =
          new QuarterHourlyAggregatesByNetworkUserResponseMeasurement(
            measurement.Timestamp,
            measurement.MeterId,
            measurement.MeasurementLocationId,
            new Dictionary<string, string>()
          );
        foreach (var register in registers)
        {
          var registerValue = measurement.RegisterValue(register);
          var registerName = register.Name;
          measurementJson.Registers.Add(registerName, registerValue.ToString());
        }

        lastMeasurementsJson.Add(measurementJson);
      }

      return Json(
        new QuarterHourlyAggregatesByNetworkUserResponse(
          dateFrom,
          dateTo,
          page,
          QueryConstants.DefaultMeasurementPageCount,
          lastMeasurements.Count,
          networkUserId,
          lastMeasurementsJson
        ));
    }

    var pageMeasurements = await measurementQueries
      .ReadByMeasurementLocationIds(
        measurementLocations.Select(x => x.Id),
        IntervalModel.QuarterHour,
        dateFrom.Value,
        dateTo.Value,
        page.Value,
        cancellationToken
      );

    var pagedMeasurementsJson =
      new List<QuarterHourlyAggregatesByNetworkUserResponseMeasurement>();
    foreach (var measurement in pageMeasurements.Items)
    {
      var measurementJson =
        new QuarterHourlyAggregatesByNetworkUserResponseMeasurement(
          measurement.Timestamp,
          measurement.MeterId,
          measurement.MeasurementLocationId,
          new Dictionary<string, string>()
        );
      foreach (var register in registers)
      {
        var registerValue = measurement.RegisterValue(register);
        var registerName = register.Name;
        measurementJson.Registers.Add(registerName, registerValue.ToString());
      }

      pagedMeasurementsJson.Add(measurementJson);
    }

    return Json(
      new QuarterHourlyAggregatesByNetworkUserResponse(
        dateFrom,
        dateTo,
        page,
        QueryConstants.DefaultMeasurementPageCount,
        pageMeasurements.TotalCount,
        networkUserId,
        pagedMeasurementsJson
      ));
  }

  private sealed record QuarterHourlyAggregatesByNetworkUserResponse(
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    int? Page,
    int? PageSize,
    int? TotalCount,
    string NetworkUserId,
    IList<QuarterHourlyAggregatesByNetworkUserResponseMeasurement> Measurements
  );

  private sealed record QuarterHourlyAggregatesByNetworkUserResponseMeasurement(
    DateTimeOffset Timestamp,
    string MeterId,
    string MeasurementLocationId,
    IDictionary<string, string> Registers
  );

  [HttpGet]
  [Route("quarter-hourly-aggregates-by-measurement-location/{measurementLocationId}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(
    typeof(QuarterHourlyAggregatesByMeasurementLocationResponse),
    StatusCodes.Status200OK,
    "application/json")]
  public async Task<IActionResult> QuarterHourlyAggregatesByMeasurementLocation(
    [FromRoute] string measurementLocationId,
    [FromQuery] DateTimeOffset? dateFrom,
    [FromQuery] DateTimeOffset? dateTo,
    [FromQuery] int? page,
    CancellationToken cancellationToken
  )
  {
    var auth = HttpContext.Items[ApiKeyAuthAttribute.ApiKeyAuthItemKey]
      as ApiKeyAuthModel;
    if (auth is null)
    {
      return Unauthorized();
    }

    var scope = auth.Scopes
      .FirstOrDefault(
        scope =>
          scope.ScopeModelType == modelReflector
            .ResolveModelName(typeof(NetworkUserMeasurementLocationModel))
          && scope.ScopeModelId == measurementLocationId);
    if (scope is null)
    {
      return Unauthorized();
    }

    if (!auth.Registers.TryGetValue(scope.Id, out var registers))
    {
      return Unauthorized();
    }

    if (dateFrom is null || dateTo is null || page is null)
    {
      var lastMeasurements = await measurementQueries
        .ReadByMeasurementLocationIdsLast(
          [measurementLocationId],
          cancellationToken,
          IntervalModel.QuarterHour
        );

      var lastMeasurementsJson =
        new List<QuarterHourlyAggregatesByMeasurementLocationResponseMeasurement>();
      foreach (var measurement in lastMeasurements)
      {
        var measurementJson =
          new QuarterHourlyAggregatesByMeasurementLocationResponseMeasurement(
            measurement.Timestamp,
            measurement.MeterId,
            measurement.MeasurementLocationId,
            new Dictionary<string, string>()
          );
        foreach (var register in registers)
        {
          var registerValue = measurement.RegisterValue(register);
          var registerName = register.Name;
          measurementJson.Registers.Add(registerName, registerValue.ToString());
        }

        lastMeasurementsJson.Add(measurementJson);
      }

      return Json(
        new QuarterHourlyAggregatesByMeasurementLocationResponse(
          dateFrom,
          dateTo,
          page,
          QueryConstants.DefaultMeasurementPageCount,
          lastMeasurements.Count,
          measurementLocationId,
          lastMeasurementsJson
        ));
    }

    var pageMeasurements = await measurementQueries
      .ReadByMeasurementLocationIds(
        [measurementLocationId],
        IntervalModel.QuarterHour,
        dateFrom.Value,
        dateTo.Value,
        page.Value,
        cancellationToken
      );

    var pagedMeasurementsJson =
      new List<QuarterHourlyAggregatesByMeasurementLocationResponseMeasurement>();
    foreach (var measurement in pageMeasurements.Items)
    {
      var measurementJson =
        new QuarterHourlyAggregatesByMeasurementLocationResponseMeasurement(
          measurement.Timestamp,
          measurement.MeterId,
          measurement.MeasurementLocationId,
          new Dictionary<string, string>()
        );
      foreach (var register in registers)
      {
        var registerValue = measurement.RegisterValue(register);
        var registerName = register.Name;
        measurementJson.Registers.Add(registerName, registerValue.ToString());
      }

      pagedMeasurementsJson.Add(measurementJson);
    }

    return Json(
      new QuarterHourlyAggregatesByMeasurementLocationResponse(
        dateFrom,
        dateTo,
        page,
        QueryConstants.DefaultMeasurementPageCount,
        pageMeasurements.TotalCount,
        measurementLocationId,
        pagedMeasurementsJson
      ));
  }

  private sealed record QuarterHourlyAggregatesByMeasurementLocationResponse(
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    int? Page,
    int? PageSize,
    int? TotalCount,
    string MeasurementLocationId,
    IList<QuarterHourlyAggregatesByMeasurementLocationResponseMeasurement> Measurements
  );

  private sealed record QuarterHourlyAggregatesByMeasurementLocationResponseMeasurement(
    DateTimeOffset Timestamp,
    string MeterId,
    string MeasurementLocationId,
    IDictionary<string, string> Registers
  );
}
