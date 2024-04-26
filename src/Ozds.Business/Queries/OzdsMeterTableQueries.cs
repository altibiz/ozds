using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Queries;

public class OzdsMeterTableQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  protected readonly AgnosticModelEntityConverter modelEntityConverter;

  public OzdsMeterTableQueries(OzdsDbContext context,
    AgnosticModelEntityConverter modelEntityConverter)
  {
    this.context = context;
    this.modelEntityConverter = modelEntityConverter;
  }

  public async Task<IMeter?> GetMeterById(string Id)
  {
    return await context.Meters.Where(context.PrimaryKeyEquals<MeterEntity>(Id))
      .Select(m => m.ToModel()).FirstOrDefaultAsync();
  }
  // !!! Fix this
  public async Task<List<NetworkUserModel>?> GetNetworkUsersByRepresentative(RepresentativeModel representative)
  {
    List<NetworkUserModel> netUsers = new();
    switch (representative.Role)
    {
      case RoleModel.NetworkUserRepresentative:
        netUsers = await context.NetworkUsers.Select(x => x.ToModel()).ToListAsync();
        break;
      case RoleModel.LocationRepresentative:
        netUsers = await context.NetworkUsers.Select(x => x.ToModel()).ToListAsync();
        break;
      case RoleModel.OperatorRepresentative:
        netUsers = await context.NetworkUsers.Select(x => x.ToModel()).ToListAsync();
        break;
    }

    return netUsers;
  }
  // !!! Fix this
  public async Task<List<LocationModel>?> GetLocationsByRepresentative(RepresentativeModel representative)
  {
    List<LocationModel> locations = new();
    switch (representative.Role)
    {
      case RoleModel.NetworkUserRepresentative:
        locations = await context.Locations.Select(x => x.ToModel()).ToListAsync();
        break;
      case RoleModel.LocationRepresentative:
        locations = await context.Locations.Select(x => x.ToModel()).ToListAsync();
        break;
      case RoleModel.OperatorRepresentative:
        locations = await context.Locations.Select(x => x.ToModel()).ToListAsync();
        break;
    }

    return locations;
  }
  public async Task<List<MeterTableViewModel>>
    ViewModelByNetworkUser(
      List<string> networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var query = context.NetworkUsers
      .Include(x => x.Location)
      .Include(x => x.Location.RegulatoryCatalogue)
      .Where(context.PrimaryKeyIn<NetworkUserEntity>(networkUserId))
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
      .Join(
        context.Meters,
        context.ForeignKeyOf(
          (ViewModelStruct x) => x.MeasurementLocation,
          nameof(MeasurementLocationEntity.Meter)
        ),
        context.PrimaryKeyOf<MeterEntity>(),
        (x, meter) => new ViewModelStruct
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = meter
        }
      )
      .GroupJoin(
        context.AbbB2xAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp <= toDate)
          .Where(x => x.Interval == IntervalEntity.Month),
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
          .Where(x => x.Interval == IntervalEntity.Month),
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
