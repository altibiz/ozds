using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries;

public class OzdsLocationsTableQueries(
  OzdsDbContext context,
  AgnosticModelEntityConverter modelEntityConverter) : IOzdsQueries
{
  protected readonly OzdsDbContext context = context;

  protected readonly AgnosticModelEntityConverter modelEntityConverter =
    modelEntityConverter;

  public async Task<List<LocationsTableViewModel>>
    ViewModelByNetworkUser(
      List<string> networkUserId
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
        context.AbbB2xMeasurements
          .OrderByDescending(x => x.Timestamp)
          .Take(1),
        context.PrimaryKeyOf((ViewModelStruct x) => x.Meter),
        context.ForeignKeyOf<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.Meter)),
        (x, abbB2xMeasurements) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          abbB2xMeasurements
        }
      )
      .SelectMany(x => x.abbB2xMeasurements.DefaultIfEmpty(),
        (x, abbMeasurement) => new ViewModelStruct
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          AbbMeasurement = abbMeasurement
        })
      .GroupJoin(
        context.SchneideriEM3xxxMeasurements
          .OrderByDescending(x => x.Timestamp)
          .Take(1),
        context.PrimaryKeyOf((ViewModelStruct x) => x.Meter),
        context.ForeignKeyOf<SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.Meter)),
        (x, schneideriEM3xxxMeasurements) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.AbbMeasurement,
          schneideriEM3xxxMeasurements
        }
      )
      .SelectMany(x => x.schneideriEM3xxxMeasurements.DefaultIfEmpty(),
        (x, schneiderMeasurement) => new ViewModelStruct
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          AbbMeasurement = x.AbbMeasurement,
          SchneiderMeasurement = schneiderMeasurement
        });
    var result = await query.ToListAsync();
    var grouped = result.GroupBy(x => x.MeasurementLocation.Id);
    return grouped
      .Select(x => new LocationsTableViewModel(
        x.First().Location.ToModel(),
        x.First().NetworkUser.ToModel(),
        x.First().MeasurementLocation.ToModel(),
        x.First().Meter.ToModel(),
        Enumerable.Empty<IMeasurement>()
          .Concat(x.Select(x => x.AbbMeasurement?.ToModel())
            .OfType<IMeasurement>())
          .Concat(x.Select(x => x.SchneiderMeasurement?.ToModel())
            .OfType<IMeasurement>())
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
    public AbbB2xMeasurementEntity? AbbMeasurement { get; init; }

    public SchneideriEM3xxxMeasurementEntity? SchneiderMeasurement
    {
      get;
      init;
    }
  }
}
