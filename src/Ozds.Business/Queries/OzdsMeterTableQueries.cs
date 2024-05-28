using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Queries;

public class OzdsMeterTableQueries(
  OzdsDbContext context,
  AgnosticModelEntityConverter modelEntityConverter) : IOzdsQueries
{
  protected readonly OzdsDbContext context = context;

  protected readonly AgnosticModelEntityConverter modelEntityConverter =
    modelEntityConverter;

  public async Task<IMeter?> GetMeterById(string Id)
  {
    return await context.Meters.Where(context.PrimaryKeyEquals<MeterEntity>(Id))
      .Select(m => m.ToModel()).FirstOrDefaultAsync();
  }

  public async Task<List<NetworkUserModel>?> GetNetworkUsersByRepresentative(
    RepresentativeModel representative)
  {
    List<NetworkUserEntity> netUsers = [];
    switch (representative.Role)
    {
      case RoleModel.NetworkUserRepresentative:
        netUsers = await context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representative
            .Id))
          .Include(x => x.NetworkUsers)
          .SelectMany(x => x.NetworkUsers)
          .ToListAsync();
        break;
      case RoleModel.LocationRepresentative:
        netUsers = await context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representative
            .Id))
          .Include(x => x.Locations)
          .ThenInclude(x => x.NetworkUsers)
          .SelectMany(x => x.Locations.SelectMany(x => x.NetworkUsers))
          .ToListAsync();
        break;
      case RoleModel.OperatorRepresentative:
        netUsers = await context.NetworkUsers
          .ToListAsync();
        break;
    }

    return netUsers.Select(x => x.ToModel()).ToList();
  }

  // !!! Fix this
  public async Task<List<LocationModel>?> GetLocationsByRepresentative(
    RepresentativeModel representative)
  {
    List<LocationModel> locations = [];
    switch (representative.Role)
    {
      case RoleModel.NetworkUserRepresentative:
        locations =
          await context.Locations.Select(x => x.ToModel()).ToListAsync();
        break;
      case RoleModel.LocationRepresentative:
        locations =
          await context.Locations.Select(x => x.ToModel()).ToListAsync();
        break;
      case RoleModel.OperatorRepresentative:
        locations =
          await context.Locations.Select(x => x.ToModel()).ToListAsync();
        break;
    }

    return locations;
  }


  public async Task<List<NetworkUserInvoiceModel>>
    GetNetworkUserInvoicesByRepresentative(
      RepresentativeModel representative,
      DateTimeOffset fromDate,
      DateTimeOffset toDate)
  {
    List<NetworkUserInvoiceEntity> results = [];
    switch (representative.Role)
    {
      case RoleModel.NetworkUserRepresentative:
        results = await context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representative
            .Id))
          .Include(x => x.NetworkUsers)
          .ThenInclude(x => x.Invoices)
          .SelectMany(x => x.NetworkUsers.SelectMany(x => x.Invoices))
          .Where(x => x.ToDate >= fromDate)
          .Where(x => x.ToDate < toDate)
          .ToListAsync();
        break;
      case RoleModel.LocationRepresentative:
        results = await context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representative
            .Id))
          .Include(x => x.Locations)
          .ThenInclude(x => x.NetworkUsers)
          .ThenInclude(x => x.Invoices)
          .SelectMany(x =>
            x.Locations.SelectMany(x =>
              x.NetworkUsers.SelectMany(x => x.Invoices)))
          .Where(x => x.ToDate >= fromDate)
          .Where(x => x.ToDate < toDate)
          .ToListAsync();
        break;
      case RoleModel.OperatorRepresentative:
        results = await context.NetworkUsers
          .Include(x => x.Invoices)
          .SelectMany(x => x.Invoices)
          .Where(x => x.ToDate >= fromDate)
          .Where(x => x.ToDate < toDate)
          .ToListAsync();
        break;
    }

    return results.Select(x => x.ToModel()).ToList();
  }

  public async Task<List<INetworkUserCalculation>>
    GetCalculationByMeasurementLocation(
      List<IMeasurementLocation> measurementLocations,
      DateTimeOffset fromDate,
      DateTimeOffset toDate)
  {
    List<NetworkUserCalculationEntity> results = [];

    results = await context.MeasurementLocations
      .Where(context.PrimaryKeyIn<MeasurementLocationEntity>(
        measurementLocations.Select(x => x.Id).ToList()))
      .Join(
        context.NetworkUserCalculations,
        context.PrimaryKeyOf<MeasurementLocationEntity>(),
        context.ForeignKeyOf<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation)
        ),
        (x, calc) => calc
      )
      .Where(x => x.ToDate >= fromDate)
      .Where(x => x.ToDate < toDate)
      .ToListAsync();

    return results.Select(x => x.ToModel()).ToList();
  }

  public async Task<List<MeterTableViewModel>>
    ViewModelByNetworkUser(
      List<string> networkUserIds,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      IntervalEntity interval = IntervalEntity.Month
    )
  {
    var query = context.NetworkUsers
      .Include(x => x.Location)
      .Include(x => x.Location.RegulatoryCatalogue)
      .Where(context.PrimaryKeyIn<NetworkUserEntity>(networkUserIds))
      .Join(
        context.MeasurementLocations
          .OfType<NetworkUserMeasurementLocationEntity>()
          .Include(x => x.NetworkUserCatalogue),
        context.PrimaryKeyOf<NetworkUserEntity>(),
        context.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser)),
        (networkUser, measurementLocation) => new ViewModelStruct
        {
          Location = networkUser.Location,
          NetworkUser = networkUser,
          MeasurementLocation = measurementLocation
        }
      )
      .GroupJoin(
        context.Meters,
        context.ForeignKeyOf(
          (ViewModelStruct x) => x.MeasurementLocation,
          nameof(MeasurementLocationEntity.Meter)
        ),
        context.PrimaryKeyOf<MeterEntity>(),
        (x, meter) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          meter
        }
      )
      .SelectMany(x => x.meter,
        (x, meter) => new ViewModelStruct
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = meter
        })
      .GroupJoin(
        context.AbbB2xAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp <= toDate)
          .Where(x => x.Interval == interval),
        context.PrimaryKeyOf((ViewModelStruct x) => x.Meter),
        context.ForeignKeyOf<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Meter)),
        (x, abbB2xAggregates) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          abbB2xAggregates
        }
      )
      .SelectMany(x => x.abbB2xAggregates.DefaultIfEmpty(),
        (x, abbAggregate) => new ViewModelStruct
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          AbbAggregate = abbAggregate
        })
      .GroupJoin(
        context.SchneideriEM3xxxAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp <= toDate)
          .Where(x => x.Interval == interval),
        context.PrimaryKeyOf((ViewModelStruct x) => x.Meter),
        context.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(
          nameof(SchneideriEM3xxxAggregateEntity.Meter)),
        (x, schneideriEM3xxxAggregates) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.AbbAggregate,
          schneideriEM3xxxAggregates
        }
      )
      .SelectMany(x => x.schneideriEM3xxxAggregates.DefaultIfEmpty(),
        (x, schneiderAggregate) => new ViewModelStruct
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          AbbAggregate = x.AbbAggregate,
          SchneiderAggregate = schneiderAggregate
        });
    var result = await query.ToListAsync();
    var grouped = result.GroupBy(x => x.MeasurementLocation.Id);
    return grouped
      .Select(x => new MeterTableViewModel(
        x.First().Location.ToModel(),
        x.First().NetworkUser.ToModel(),
        x.First().MeasurementLocation.ToModel(),
        x.First().Meter.ToModel(),
        Enumerable.Empty<AggregateModel>()
          .Concat(x.Select(x => x.AbbAggregate?.ToModel())
            .Where(x => x is not null).OfType<AggregateModel>())
          .Concat(x.Select(x => x.SchneiderAggregate?.ToModel())
            .Where(x => x is not null).OfType<AggregateModel>())
          .ToList()
      ))
      .ToList();
  }

  private readonly struct ViewModelStruct
  {
    public LocationEntity Location { get; init; }
    public NetworkUserEntity NetworkUser { get; init; }

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    }

    public MeterEntity Meter { get; init; }
    public AbbB2xAggregateEntity? AbbAggregate { get; init; }
    public SchneideriEM3xxxAggregateEntity? SchneiderAggregate { get; init; }
  }
}
