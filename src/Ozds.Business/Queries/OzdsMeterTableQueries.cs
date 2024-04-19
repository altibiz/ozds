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
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsMeterTableQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  protected readonly AgnosticModelEntityConverter modelEntityConverter;

  public OzdsMeterTableQueries(OzdsDbContext context, AgnosticModelEntityConverter modelEntityConverter)
  {
    this.context = context;
    this.modelEntityConverter = modelEntityConverter;
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

  private readonly struct ViewModelStruct
  {
    public LocationEntity Location { get; init; }
    public NetworkUserEntity NetworkUser { get; init; }
    public NetworkUserMeasurementLocationEntity MeasurementLocation { get; init; }
    public MeterEntity Meter { get; init; }
    public AbbB2xAggregateEntity? AbbAggregate { get; init; }
    public SchneideriEM3xxxAggregateEntity? SchneiderAggregate { get; init; }
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
          context.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(nameof(NetworkUserMeasurementLocationEntity.NetworkUser)),
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
            .Where(x =>
              x.Interval == IntervalEntity.QuarterHour ||
              x.Interval == IntervalEntity.Month),
          context.PrimaryKeyOf((ViewModelStruct x) => x.Meter),
          context.ForeignKeyOf<AbbB2xAggregateEntity>(nameof(AbbB2xAggregateEntity.Meter)),
          (x, abbB2xAggregates) => new
          {
            x.Location,
            x.NetworkUser,
            x.MeasurementLocation,
            x.Meter,
            AbbAggregates = abbB2xAggregates
          }
        )
        .SelectMany(x => x.AbbAggregates.DefaultIfEmpty(),
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
            .Where(x =>
              x.Interval == IntervalEntity.QuarterHour ||
              x.Interval == IntervalEntity.Month),
          context.PrimaryKeyOf((ViewModelStruct x) => x.Meter),
          context.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(nameof(SchneideriEM3xxxAggregateEntity.Meter)),
          (x, schneideriEM3xxxAggregate) => new
          {
            x.Location,
            x.NetworkUser,
            x.MeasurementLocation,
            x.Meter,
            x.AbbAggregate,
            SchneiderAggregate = schneideriEM3xxxAggregate
          }
        )
        .SelectMany(x => x.SchneiderAggregate.DefaultIfEmpty(), (x, schneiderAggregate) => new ViewModelStruct
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
        x.FirstOrDefault()!.Location.ToModel(),
        x.FirstOrDefault()!.NetworkUser.ToModel(),
        x.FirstOrDefault()!.MeasurementLocation.ToModel(),
        x.FirstOrDefault()!.Meter.ToModel(),
        Enumerable.Empty<AggregateModel>()
        .Concat(x.Select(x => x.AbbAggregate?.ToModel()).Where(x => x is not null).OfType<AggregateModel>())
        .Concat(x.Select(x => x.SchneiderAggregate?.ToModel()).Where(x => x is not null).OfType<AggregateModel>())
        .ToList()
      ))
      .ToList();
  }

}
