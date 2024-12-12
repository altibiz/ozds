using System.Data;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

public class MeasurementUpsertMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task UpsertMeasurements(
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    await UpsertMeasurements(
      context,
      measurements,
      cancellationToken
    );
  }

#pragma warning disable CA1822 // Mark members as static
  internal async Task UpsertMeasurements(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
#pragma warning restore CA1822 // Mark members as static
  {
    var connection = context.GetDapperDbConnection();

    var parameters = context.CreateBulkParameters(measurements);
    var sql = Upsert(context, measurements);

    while (true)
    {
      try
      {
        var isolationLevel = IsolationLevel.RepeatableRead;

        var transaction = await connection
          .BeginTransactionAsync(isolationLevel, cancellationToken);

        var command = new CommandDefinition(
            sql,
            parameters,
            transaction: transaction,
            cancellationToken: cancellationToken
        );

        await connection.ExecuteAsync(command);

        await context.Database.CommitTransactionAsync(cancellationToken);

        break;
      }
      catch (PostgresException ex)
      {
        if (ex.Message.StartsWith("40001"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        if (ex.Message.StartsWith("40P01"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        if (ex.Message.StartsWith("P0002"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        throw;
      }
    }
  }

  private static string Upsert(
      DataDbContext context,
      IEnumerable<IMeasurementEntity> measurements)
  {
    var sql = new StringBuilder();

    var groupedMeasurements = measurements
        .Select((x, i) => (Index: i, Measurement: x))
        .GroupBy(x => x.Measurement.GetType());

    foreach (var group in groupedMeasurements)
    {
      var entityType = context.Model.FindEntityType(group.Key)
          ?? throw new InvalidOperationException(
            $"No entity type found for {group.Key}.");
      var tableName = entityType.GetTableName()
          ?? throw new InvalidOperationException(
            $"No table name found for {group.Key}.");

      var properties = entityType.GetProperties().ToList();
      var columnNames = properties.Select(p => p.GetColumnName()).ToList();
      var primaryKey = entityType.FindPrimaryKey()
        ?? throw new InvalidOperationException(
          $"No primary key found for {entityType.Name}.");
      var primaryKeyColumns = primaryKey.Properties
        .Select(p => p.GetColumnName())
        .ToList();

      bool isAggregate = typeof(IAggregateEntity)
        .IsAssignableFrom(group.Key);
      bool isMeasurement = typeof(IMeasurementEntity)
        .IsAssignableFrom(group.Key) && !isAggregate;

      foreach (var (index, measurement) in group)
      {
        var columns = string.Join(", ", columnNames);
        var values = string.Join(", ", columnNames
          .Select(column => $"@p{index}_{column}"));
        var conflict = string.Join(", ", primaryKeyColumns);

        string updateClause;
        if (isMeasurement)
        {
          updateClause = "DO NOTHING";
        }
        else if (isAggregate)
        {
          var countColumn = context
            .GetColumnName(measurement.GetType(),
            nameof(IAggregateEntity.Count));

          var setClauses = new List<string>();

          setClauses.Add(
            $"{countColumn} = {tableName}.{countColumn} "
              + $"+ EXCLUDED.{countColumn}");

          foreach (var prop in properties)
          {
            var col = prop.GetColumnName();
            if (col == countColumn)
            {
              continue;
            }

            if (col.Contains("avg"))
            {
              setClauses.Add(
                  $"{col} = (({tableName}.{col} * {tableName}.{countColumn})"
                  + $" + (EXCLUDED.{col} * EXCLUDED.{countColumn}))"
                  + $" / ({tableName}.{countColumn} + EXCLUDED.{countColumn})");
            }
            else if (col.Contains("min"))
            {
              setClauses.Add(
                $"{col} = LEAST({tableName}.{col}, EXCLUDED.{col})");
            }
            else if (col.Contains("max"))
            {
              setClauses.Add(
                $"{col} = GREATEST({tableName}.{col}, EXCLUDED.{col})");
            }
          }

          updateClause = "DO UPDATE SET " + string.Join(", ", setClauses);
        }
        else
        {
          throw new InvalidOperationException(
            $"Unknown measurement type {group.Key}.");
        }

        var row = $@"
          INSERT INTO {tableName} ({columns})
          VALUES ({values})
          ON CONFLICT ({conflict}) {updateClause};
        ";

        sql.AppendLine(row);
      }
    }

    return sql.ToString();
  }
}
