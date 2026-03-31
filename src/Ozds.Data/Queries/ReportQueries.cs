using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Queries;

public class ReportQueries(
  IDbContextFactory<DataDbContext> factory,
  MeasurementQueries measurementQueries,
  AggregateWindowQueries aggregateWindowQueries,
  EntityReflector reflector
) : IQueries
{
  public async Task<List<EnergyCardReportBasisEntity>?> ReadEnergyCardReportBasis(
    IEnumerable<string> measurementLocationIds,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBases(
      measurementLocationIds,
      cancellationToken
    );
    if (initial is null)
    {
      return null;
    }

    var boundaries =
      await aggregateWindowQueries.ReadAggregateWindowBoundaryBasesByMeasurementLocation(
        GroupByAggregateType(initial),
        fromDate,
        toDate,
        cancellationToken
      );

    return MapEnergyCardBases(initial, boundaries);
  }

  public async Task<List<EnergyCardReportBasisEntity>?> ReadEnergyCardReportBasisByNetworkUser(
    string networkUserId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBasesByNetworkUser(
      networkUserId,
      cancellationToken
    );
    if (initial is null)
    {
      return null;
    }

    var boundaries =
      await aggregateWindowQueries.ReadAggregateWindowBoundaryBasesByMeasurementLocation(
        GroupByAggregateType(initial),
        fromDate,
        toDate,
        cancellationToken
      );

    return MapEnergyCardBases(initial, boundaries);
  }

  public async Task<List<EnergyCardReportBasisEntity>?> ReadEnergyCardReportBasisByLocation(
    string locationId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBasesByLocation(
      locationId,
      cancellationToken
    );
    if (initial is null)
    {
      return null;
    }

    var boundaries =
      await aggregateWindowQueries.ReadAggregateWindowBoundaryBasesByMeasurementLocation(
        GroupByAggregateType(initial),
        fromDate,
        toDate,
        cancellationToken
      );

    return MapEnergyCardBases(initial, boundaries);
  }

  public async Task<AccountingPeriodReportBasisEntity?> ReadAccountingPeriodReportBasis(
    string measurementLocationId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBasis(
      measurementLocationId,
      cancellationToken
    );
    if (initial is null)
    {
      return null;
    }

    var aggregates = await measurementQueries.ReadByMeasurementLocationIds(
      [measurementLocationId],
      IntervalEntity.Month,
      fromDate,
      toDate,
      0,
      cancellationToken
    );

    var minAggregate = aggregates
      .Items.OfType<AggregateEntity>()
      .FirstOrDefault();
    var maxAggregate = aggregates
      .Items.OfType<AggregateEntity>()
      .LastOrDefault();
    if (minAggregate is null || maxAggregate is null)
    {
      return null;
    }

    return new AccountingPeriodReportBasisEntity
    {
      Location = initial.Location,
      NetworkUser = initial.NetworkUser,
      Catalogue = initial.Catalogue,
      MeasurementLocation = initial.MeasurementLocation,
      Meter = initial.Meter,
      MinAggregate = minAggregate,
      MaxAggregate = maxAggregate,
    };
  }

  public async Task<AccountingPeriodReportBasisEntity?> ReadAccountingPeriodReportBasisByMeter(
    Type aggregateEntityType,
    string meterId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBasisByMeter(meterId, cancellationToken);
    if (initial is null)
    {
      return null;
    }

    var aggregates = await measurementQueries.ReadByMeterIds(
      [
        new KeyValuePair<Type, IEnumerable<string>>(
          aggregateEntityType,
          [meterId]
        ),
      ],
      IntervalEntity.Month,
      fromDate,
      toDate,
      0,
      cancellationToken
    );

    var minAggregate = aggregates
      .Items.OfType<AggregateEntity>()
      .FirstOrDefault();
    var maxAggregate = aggregates
      .Items.OfType<AggregateEntity>()
      .LastOrDefault();
    if (minAggregate is null || maxAggregate is null)
    {
      return null;
    }

    return new AccountingPeriodReportBasisEntity
    {
      Location = initial.Location,
      NetworkUser = initial.NetworkUser,
      Catalogue = initial.Catalogue,
      MeasurementLocation = initial.MeasurementLocation,
      Meter = initial.Meter,
      MinAggregate = minAggregate,
      MaxAggregate = maxAggregate,
    };
  }

  public async Task<LoadCurveReportBasisEntity?> ReadLoadCurveReportBasis(
    string measurementLocationId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBasis(
      measurementLocationId,
      cancellationToken
    );
    if (initial is null)
    {
      return null;
    }

    var aggregateType = reflector.ResolveMeterMeasurementType(
      initial.Meter.GetType(),
      aggregate: true
    );

    var curves =
      await aggregateWindowQueries.ReadAggregateWindowLoadCurveBasesByMeasurementLocation(
        [
          new KeyValuePair<Type, IReadOnlyList<string>>(
            aggregateType,
            [measurementLocationId]
          ),
        ],
        fromDate,
        toDate,
        cancellationToken
      );

    var curve = curves.FirstOrDefault();
    if (curve is null)
    {
      return null;
    }

    return new LoadCurveReportBasisEntity
    {
      Location = initial.Location,
      NetworkUser = initial.NetworkUser,
      Catalogue = initial.Catalogue,
      MeasurementLocation = initial.MeasurementLocation,
      Meter = initial.Meter,
      Aggregates = curve.InWindowAggregates,
    };
  }

  public async Task<LoadCurveReportBasisEntity?> ReadLoadCurveReportBasisByMeter(
    Type aggregateEntityType,
    string meterId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var initial = await ReadReportBasisByMeter(meterId, cancellationToken);
    if (initial is null)
    {
      return null;
    }

    var curves =
      await aggregateWindowQueries.ReadAggregateWindowLoadCurveBasesByMeasurementLocation(
        [
          new KeyValuePair<Type, IReadOnlyList<string>>(
            aggregateEntityType,
            [initial.MeasurementLocation.Id]
          ),
        ],
        fromDate,
        toDate,
        cancellationToken
      );

    var curve = curves.FirstOrDefault();
    if (curve is null)
    {
      return null;
    }

    return new LoadCurveReportBasisEntity
    {
      Location = initial.Location,
      NetworkUser = initial.NetworkUser,
      Catalogue = initial.Catalogue,
      MeasurementLocation = initial.MeasurementLocation,
      Meter = initial.Meter,
      Aggregates = curve.InWindowAggregates,
    };
  }

  private IEnumerable<
    KeyValuePair<Type, IReadOnlyList<string>>
  > GroupByAggregateType(List<ReportBasisEntity> bases)
  {
    return bases
      .GroupBy(b =>
        reflector.ResolveMeterMeasurementType(
          b.Meter.GetType()
            .Assembly.FullName?.StartsWith("DynamicProxyGenAssembly2") != true // TODO: temp fix for proxy types until better solution is implemented
            ? b.Meter.GetType()
            : b.Meter.GetType().BaseType!,
          aggregate: true
        )
      )
      .Select(g => new KeyValuePair<Type, IReadOnlyList<string>>(
        g.Key,
        g.Select(b => b.MeasurementLocation.Id).ToList()
      ));
  }

  private static List<EnergyCardReportBasisEntity> MapEnergyCardBases(
    List<ReportBasisEntity> bases,
    List<AggregateWindowBoundaryBasisEntity> boundaries
  )
  {
    return bases
      .Select(basis =>
      {
        var boundary = boundaries.FirstOrDefault(b =>
          b.MeasurementLocationId == basis.MeasurementLocation.Id
        );
        if (
          boundary?.StartAggregate is null
          || boundary?.EndAggregate is null
          || boundary.StartAggregate == boundary.EndAggregate
        )
        {
          return null;
        }

        return new EnergyCardReportBasisEntity
        {
          Location = basis.Location,
          NetworkUser = basis.NetworkUser,
          Catalogue = basis.Catalogue,
          MeasurementLocation = basis.MeasurementLocation,
          Meter = basis.Meter,
          MinAggregate = boundary.StartAggregate,
          MaxAggregate = boundary.EndAggregate,
        };
      })
      .OfType<EnergyCardReportBasisEntity>()
      .ToList();
  }

  private async Task<List<ReportBasisEntity>?> ReadReportBases(
    IEnumerable<string> measurementLocationIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var entities = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.PrimaryKeyIn<NetworkUserMeasurementLocationEntity>(
          measurementLocationIds
        )
      )
      .Include(x => x.Meter)
      .Include(x => x.NetworkUserCatalogue)
      .Include(x => x.NetworkUser)
        .ThenInclude(x => x.Location)
      .ToListAsync(cancellationToken);
    if (entities is null)
    {
      return null;
    }

    return entities
      .Select(entity => new ReportBasisEntity
      {
        Location = entity.NetworkUser.Location,
        NetworkUser = entity.NetworkUser,
        Catalogue = entity.NetworkUserCatalogue,
        MeasurementLocation = entity,
        Meter = entity.Meter,
      })
      .ToList();
  }

  private async Task<List<ReportBasisEntity>?> ReadReportBasesByLocation(
    string locationId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    // TODO: optimize - ??

    // NOTE: for now viable without split queries
    var networkUsers = await context
      .NetworkUsers.Where(
        context.ForeignKeyEquals<NetworkUserEntity>(
          nameof(NetworkUserEntity.Location),
          locationId
        )
      )
      .Include(x => x.Location)
      .Include(x => x.NetworkUserMeasurementLocations)
        .ThenInclude(x => x.NetworkUserCatalogue)
      .Include(x => x.NetworkUserMeasurementLocations)
        .ThenInclude(x => x.Meter)
      .ToListAsync(cancellationToken);

    return networkUsers
      .SelectMany(nu =>
        nu.NetworkUserMeasurementLocations.Select(ml => new ReportBasisEntity
        {
          Location = nu.Location,
          NetworkUser = nu,
          Catalogue = ml.NetworkUserCatalogue,
          MeasurementLocation = ml,
          Meter = ml.Meter,
        })
      )
      .ToList();
  }

  private async Task<List<ReportBasisEntity>?> ReadReportBasesByNetworkUser(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var entities = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.ForeignKeyEquals<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser),
          networkUserId
        )
      )
      .Include(x => x.Meter)
      .Include(x => x.NetworkUserCatalogue)
      .Include(x => x.NetworkUser)
        .ThenInclude(x => x.Location)
      .ToListAsync(cancellationToken);
    if (entities is null)
    {
      return null;
    }

    return entities
      .Select(entity => new ReportBasisEntity
      {
        Location = entity.NetworkUser.Location,
        NetworkUser = entity.NetworkUser,
        Catalogue = entity.NetworkUserCatalogue,
        MeasurementLocation = entity,
        Meter = entity.Meter,
      })
      .ToList();
  }

  private async Task<ReportBasisEntity?> ReadReportBasis(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var entity = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.PrimaryKeyEquals<NetworkUserMeasurementLocationEntity>(
          measurementLocationId
        )
      )
      .Include(x => x.Meter)
      .Include(x => x.NetworkUserCatalogue)
      .Include(x => x.NetworkUser)
        .ThenInclude(x => x.Location)
      .FirstOrDefaultAsync(cancellationToken);
    if (entity is null)
    {
      return null;
    }

    return new ReportBasisEntity
    {
      Location = entity.NetworkUser.Location,
      NetworkUser = entity.NetworkUser,
      Catalogue = entity.NetworkUserCatalogue,
      Meter = entity.Meter,
      MeasurementLocation = entity,
    };
  }

  private async Task<ReportBasisEntity?> ReadReportBasisByMeter(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var entity = await context
      .Meters.Where(context.PrimaryKeyEquals<MeterEntity>(meterId))
      .GroupJoin(
        context
          .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
          .Include(x => x.NetworkUserCatalogue)
          .Include(x => x.NetworkUser)
            .ThenInclude(x => x.Location),
        context.PrimaryKeyOf<MeterEntity>(),
        context.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.Meter)
        ),
        (meter, measurementLocations) => new { meter, measurementLocations }
      )
      .SelectMany(
        joined => joined.measurementLocations.DefaultIfEmpty(),
        (meter, measurementLocation) =>
          measurementLocation == null
            ? new ReportBasisEntity { Meter = meter.meter }
            : new ReportBasisEntity
            {
              Location = measurementLocation.NetworkUser.Location,
              NetworkUser = measurementLocation.NetworkUser,
              Catalogue = measurementLocation.NetworkUserCatalogue,
              Meter = meter.meter,
              MeasurementLocation = measurementLocation,
            }
      )
      .FirstOrDefaultAsync(cancellationToken);
    if (entity is null || entity.MeasurementLocation is null)
    {
      return null;
    }

    return entity;
  }
}
