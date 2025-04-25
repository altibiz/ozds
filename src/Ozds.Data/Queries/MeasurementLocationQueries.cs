using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MeasurementLocationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<IMeasurementLocationEntity?> ReadMeasurementLocationByMeter(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var measurementLocation = await context
      .MeasurementLocations.Where(
        context.ForeignKeyEquals<MeasurementLocationEntity>(
          nameof(MeasurementLocationEntity.Meter),
          meterId
        )
      )
      .FirstOrDefaultAsync(cancellationToken);
    return measurementLocation;
  }

  public async Task<IMeterEntity?> ReadMeterByMeasurementLocation(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var meter = await context
      .MeasurementLocations.Where(
        context.PrimaryKeyEquals<MeasurementLocationEntity>(
          measurementLocationId
        )
      )
      .Include(x => x.Meter)
      .Select(x => x.Meter)
      .FirstOrDefaultAsync(cancellationToken);
    return meter;
  }

  public async Task<
      List<IMeasurementLocationEntity>>
    ReadMeasurementLocationByNetworkUser(
      string networkUserId,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var measurementLocations = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.ForeignKeyEquals<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser),
          networkUserId
        )
      )
      .Select(x => (IMeasurementLocationEntity)x)
      .ToListAsync(cancellationToken);
    return measurementLocations;
  }

  public async Task<List<IMeasurementLocationEntity>>
    ReadMeasurementLocationByLocation(
      string locationId,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var networkUsers = await context
      .NetworkUsers.OfType<NetworkUserEntity>()
      .Where(
        context.ForeignKeyEquals<NetworkUserEntity>(
          nameof(NetworkUserEntity.Location),
          locationId
        )
      )
      .ToListAsync(cancellationToken);

    var measurementLocations = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.ForeignKeyIn<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser),
          networkUsers.Select(x => x.Id).ToList()
        )
      )
      .Select(x => (IMeasurementLocationEntity)x)
      .ToListAsync(cancellationToken);

    return measurementLocations;
  }

  public async Task<List<AnalysisBasisEntity>>
    ReadAnalysisBasesByLocationAndRepresentative(
      string? locationId,
      RepresentativeEntity? representative,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    if (representative?.Role
      is null
      or RoleEntity.OperatorRepresentative
      or RoleEntity.LocationRepresentative)
    {
      var initialLocations = context.Locations as IQueryable<LocationEntity>;

      if (locationId is not null)
      {
        initialLocations = initialLocations
          .Where(context.PrimaryKeyEquals<LocationEntity>(locationId));
      }

      return await initialLocations
        .Include(x => x.NetworkUsers)
        .ThenInclude(x => x.NetworkUserMeasurementLocations)
        .ThenInclude(x => x.Meter)
        .AsSingleQuery()
        .ToListAsync(cancellationToken)
        .ContinueWith(
          x => x.Result
            .SelectMany(
              x => x.NetworkUsers
                .SelectMany(
                  y => y.NetworkUserMeasurementLocations
                    .Select(
                      z => new AnalysisBasisEntity
                      {
                        Representative = representative,
                        FromDate = fromDate,
                        ToDate = toDate,
                        Location = x,
                        NetworkUser = y,
                        MeasurementLocation = z,
                        Meter = z.Meter,
                        Calculations = new List<CalculationEntity>(),
                        Invoices = new List<InvoiceEntity>(),
                        LastMeasurement = null,
                        MonthlyAggregates = new List<AggregateEntity>()
                      })))
            .ToList());
    }

    var initialNetworkUsersQuery = context.NetworkUserRepresentatives
      .Where(
        context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.Representative),
          representative.Id))
      .Select(x => x.NetworkUser);
    if (locationId is not null)
    {
      initialNetworkUsersQuery = initialNetworkUsersQuery
        .Where(
          context.ForeignKeyEquals<NetworkUserEntity>(
            nameof(NetworkUserEntity.Location),
            locationId));
    }

    var initialNetworkUsers = await initialNetworkUsersQuery
      .ToListAsync(cancellationToken);

    return await context.NetworkUsers
      .Where(
        context.PrimaryKeyIn<NetworkUserEntity>(
          initialNetworkUsers.Select(x => x.Id)))
      .Include(x => x.Location)
      .Include(x => x.NetworkUserMeasurementLocations)
      .ThenInclude(x => x.Meter)
      .AsSingleQuery()
      .ToListAsync(cancellationToken)
      .ContinueWith(
        x => x.Result
          .SelectMany(
            x => x.NetworkUserMeasurementLocations
              .Select(
                y => new AnalysisBasisEntity
                {
                  Representative = representative,
                  FromDate = fromDate,
                  ToDate = toDate,
                  Location = x.Location,
                  NetworkUser = x,
                  MeasurementLocation = y,
                  Meter = y.Meter,
                  Calculations = new List<CalculationEntity>(),
                  Invoices = new List<InvoiceEntity>(),
                  LastMeasurement = null,
                  MonthlyAggregates = new List<AggregateEntity>()
                }))
          .ToList());
  }
}
