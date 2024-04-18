using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsMeterTableQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsMeterTableQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  // public async Task<List<MeterTableViewModel>>
  //   ViewModelByNetworkUser(
  //     string networkUserId,
  //     DateTimeOffset fromDate,
  //     DateTimeOffset toDate
  //   )
  // {
  //   return (await context.NetworkUsers
  //       .WithId(networkUserId)
  //       .Join(
  //         context.MeasurementLocations
  //           .OfType<NetworkUserMeasurementLocationEntity>()
  //           .Include(x => x.NetworkUserCatalogue)
  //           .Include(x => x.NetworkUser)
  //           .Include(x => x.NetworkUser.Location)
  //           .Include(x => x.NetworkUser.Location.RegulatoryCatalogue),
  //         networkUser => networkUser.Id,
  //         measurementLocation => measurementLocation.NetworkUserId,
  //         (networkUser, measurementLocation) => new
  //         {
  //           Location = measurementLocation.NetworkUser.Location.ToModel(),
  //           NetworkUser = measurementLocation.NetworkUser.ToModel(),
  //           MeasurementLocation = measurementLocation.ToModel()
  //         }
  //       )
  //       .Join(
  //         context.Meters,
  //         x => x.MeasurementLocation.MeterId,
  //         meter => meter.Id,
  //         (x, meter) => new
  //         {
  //           x.Location,
  //           x.NetworkUser,
  //           x.MeasurementLocation,
  //           Meter = meter.ToModel()
  //         }
  //       )
  //       .GroupJoin(
  //         context.AbbB2xAggregates
  //           .Where(x => x.Timestamp >= fromDate)
  //           .Where(x => x.Timestamp <= toDate)
  //           .Where(x =>
  //             x.Interval == IntervalEntity.QuarterHour ||
  //             x.Interval == IntervalEntity.Month),
  //         x => x.Meter.Id,
  //         aggregate => aggregate.MeterId,
  //         (x, abbB2xAggregates) => new
  //         {
  //           x.Location,
  //           x.NetworkUser,
  //           x.Meter,
  //           x.MeasurementLocation,
  //           AbbB2xAggregates = abbB2xAggregates
  //             .Select(abbB2xAggregate => abbB2xAggregate
  //               .ToModel())
  //         }
  //       )
  //       .GroupJoin(
  //         context.SchneideriEM3xxxAggregates
  //           .Where(x => x.Timestamp >= fromDate)
  //           .Where(x => x.Timestamp <= toDate)
  //           .Where(x =>
  //             x.Interval == IntervalEntity.QuarterHour ||
  //             x.Interval == IntervalEntity.Month),
  //         x => x.Meter.Id,
  //         aggregate => aggregate.MeterId,
  //         (x, schneideriEM3xxxAggregates) => new
  //         {
  //           x.Location,
  //           x.NetworkUser,
  //           x.Meter,
  //           x.MeasurementLocation,
  //           x.AbbB2xAggregates,
  //           SchneideriEM3xxxAggregates = schneideriEM3xxxAggregates
  //             .Select(schneideriEM3xxxAggregate => schneideriEM3xxxAggregate
  //               .ToModel())
  //         }
  //       )
  //       .ToListAsync())
  //     .Select(x => new MeterTableViewModel(
  //       x.Location,
  //       x.NetworkUser,
  //       x.Meter,
  //       Enumerable.Empty<AggregateModel>()
  //         .Concat(x.AbbB2xAggregates)
  //         .Concat(x.SchneideriEM3xxxAggregates)
  //         .ToList()
  //     ))
  //     .ToList();
  // }

  private readonly struct NetworkUserMeasurementLocationJoin
  {
    public LocationEntity Location { get; init; }
    public NetworkUserEntity NetworkUser { get; init; }
    public NetworkUserMeasurementLocationEntity MeasurementLocation { get; init; }
    public NetworkUserCatalogueEntity NetworkUserCatalogue { get; init; }
    public RegulatoryCatalogueEntity RegulatoryCatalogue { get; init; }
  };

  public async Task<List<MeterTableViewModel>>
    ViewModelByNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var query = context.NetworkUsers
        .Include(x => x.Location)
        .Include(x => x.Location.RegulatoryCatalogue)
        .Where(context.PrimaryKeyEquals<NetworkUserEntity>(networkUserId))
        .Join(
          context.MeasurementLocations
            .OfType<NetworkUserMeasurementLocationEntity>()
            .Include(x => x.NetworkUserCatalogue),
          context.PrimaryKeyOf<NetworkUserEntity>(),
          context.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(typeof(NetworkUserEntity)),
          (networkUser, measurementLocation) => new NetworkUserMeasurementLocationJoin
          {
            Location = networkUser.Location,
            NetworkUser = networkUser,
            MeasurementLocation = measurementLocation,
            NetworkUserCatalogue = measurementLocation.NetworkUserCatalogue,
            RegulatoryCatalogue = networkUser.Location.RegulatoryCatalogue
          }
        )
        .Join(
          context.Meters,
          context.ForeignKeyOf(
            (NetworkUserMeasurementLocationJoin x) => x.MeasurementLocation,
            typeof(MeterEntity)
          ),
          context.PrimaryKeyOf<MeterEntity>(),
          (x, meter) => new
          {
            x.Location,
            x.NetworkUser,
            x.MeasurementLocation,
            x.NetworkUserCatalogue,
            x.RegulatoryCatalogue,
            Meter = meter
          }
        );
    return (await query.ToListAsync())
      .Select(x => new MeterTableViewModel(
        x.Location.ToModel(),
        x.NetworkUser.ToModel(),
        x.MeasurementLocation.ToModel(),
        x.Meter.ToModel(),
        Enumerable.Empty<AggregateModel>().ToList()
      ))
      .ToList();
  }

}
