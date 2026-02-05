using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class AnalysisQueries(IDbContextFactory<DataDbContext> factory)
  : IQueries
{
  public async Task<
    List<AnalysisBasisEntity>
  > ReadByLocationIdAndRepresentative(
    string? locationId,
    RepresentativeEntity? representative,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    if (
      representative?.Role
      is null
        or RoleEntity.OperatorRepresentative
        or RoleEntity.LocationRepresentative
    )
    {
      var initialLocations = context.Locations as IQueryable<LocationEntity>;

      if (locationId is not null)
      {
        initialLocations = initialLocations
          .Where(context.PrimaryKeyEquals<LocationEntity>(locationId))
          .Where(x => !x.IsDeleted);
      }

      return await initialLocations
        .Include(x => x.NetworkUsers.Where(x => !x.IsDeleted))
          .ThenInclude(x =>
            x.NetworkUserMeasurementLocations.Where(x => !x.IsDeleted)
          )
            .ThenInclude(x => x.Meter)
        .AsSingleQuery()
        .ToListAsync(cancellationToken)
        .ContinueWith(x =>
          x.Result.SelectMany(x =>
              x.NetworkUsers.SelectMany(y =>
                y.NetworkUserMeasurementLocations.Select(
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
                    MonthlyAggregates = new List<AggregateEntity>(),
                  }
                )
              )
            )
            .ToList()
        );
    }

    var initialNetworkUsersQuery = context
      .NetworkUserRepresentatives.Where(
        context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.Representative),
          representative.Id
        )
      )
      .Select(x => x.NetworkUser)
      .Where(x => !x.IsDeleted);
    if (locationId is not null)
    {
      initialNetworkUsersQuery = initialNetworkUsersQuery.Where(
        context.ForeignKeyEquals<NetworkUserEntity>(
          nameof(NetworkUserEntity.Location),
          locationId
        )
      );
    }

    var initialNetworkUsers = await initialNetworkUsersQuery.ToListAsync(
      cancellationToken
    );

    return await context
      .NetworkUsers.Where(
        context.PrimaryKeyIn<NetworkUserEntity>(
          initialNetworkUsers.Select(x => x.Id)
        )
      )
      .Where(x => !x.IsDeleted)
      .Include(x => x.Location)
      .Include(x => x.NetworkUserMeasurementLocations.Where(x => !x.IsDeleted))
        .ThenInclude(x => x.Meter)
      .AsSingleQuery()
      .ToListAsync(cancellationToken)
      .ContinueWith(x =>
        x.Result.SelectMany(x =>
            x.NetworkUserMeasurementLocations.Select(
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
                MonthlyAggregates = new List<AggregateEntity>(),
              }
            )
          )
          .ToList()
      );
  }
}
