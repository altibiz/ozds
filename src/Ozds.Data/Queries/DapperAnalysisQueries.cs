using System.Data;
using Dapper;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class DapperAnalysisQueries(
  IServiceScopeFactory factory
) : IQueries
{
  public async Task<PaginatedList<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentative(
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    await using var scope = factory.CreateAsyncScope();
    using var connection = scope.ServiceProvider
      .GetRequiredService<IDbConnection>();

    var results = await connection
      .QueryAsync<DetailedMeasurementLocationsByRepresentativeIntermediary>(
      """
        SELECT 1;
      """
    );

    return MakeAnalysisBases(results, 0);
  }

  private PaginatedList<AnalysisBasisEntity>
    MakeAnalysisBases(
      IEnumerable<DetailedMeasurementLocationsByRepresentativeIntermediary> items,
      int count
    )
  {
    return items
      .GroupBy(x => x.MeasurementLocation.Id)
      .Select(
        x =>
        {
          var first = x.FirstOrDefault();
          if (first is null)
          {
            return null;
          }

          var lastMeasurement =
            Enumerable.Empty<MeasurementEntity>()
              .Concat(x
                .Select(x => x.AbbLastMeasurement)
                .OfType<MeasurementEntity>())
              .Concat(x
                .Select(x => x.SchneiderLastMeasurement)
                .OfType<MeasurementEntity>())
              .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbMonthlyAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderMonthlyAggregate)
                .OfType<AggregateEntity>())
              .ToList();

          var monthlyMaxPowerAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbMaxPowerAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderMaxPowerAggregate)
                .OfType<AggregateEntity>())
              .ToList();

          var calculations = x
            .Select(x => x.NetworkUserCalculation)
            .OfType<CalculationEntity>()
            .ToList();

          var invoices = x
            .Select(x => x.NetworkUserInvoice)
            .OfType<InvoiceEntity>()
            .DistinctBy(x => x.Id)
            .ToList();

          return new AnalysisBasisEntity(
            first.Location,
            first.NetworkUser,
            first.MeasurementLocation,
            first.Meter,
            calculations,
            invoices,
            lastMeasurement,
            monthlyAggregates,
            monthlyMaxPowerAggregates
          );
        })
      .OfType<AnalysisBasisEntity>()
      .ToPaginatedList(count);
  }

  private sealed class DetailedMeasurementLocationsByRepresentativeIntermediary
  {
    public LocationEntity Location { get; init; } = default!;

    public NetworkUserEntity? NetworkUser { get; init; }

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    } = default!;

    public MeterEntity Meter { get; init; } = default!;

    public NetworkUserCalculationEntity? NetworkUserCalculation { get; init; }

    public NetworkUserInvoiceEntity? NetworkUserInvoice { get; init; }

    public AbbB2xMeasurementEntity? AbbLastMeasurement { get; init; }

    public SchneideriEM3xxxMeasurementEntity? SchneiderLastMeasurement
    {
      get;
      init;
    }

    public AbbB2xAggregateEntity? AbbMonthlyAggregate { get; init; }

    public SchneideriEM3xxxAggregateEntity? SchneiderMonthlyAggregate
    {
      get;
      init;
    }

    public AbbB2xAggregateEntity? AbbMaxPowerAggregate { get; init; }

    public SchneideriEM3xxxAggregateEntity? SchneiderMaxPowerAggregate
    {
      get;
      init;
    }
  }
}
