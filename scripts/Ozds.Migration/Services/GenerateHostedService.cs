using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Procedures;
using Ozds.Migration.Arguments;
using Ozds.Migration.Extensions;

namespace Ozds.Migration.Services;

public class GenerateHostedService(
  IServiceProvider serviceProvider,
  ILogger<GenerateHostedService> logger,
  OzdsMigrationGenerateArguments arguments
) : BackgroundService
{
  private static readonly List<Type> MeasurementTypes =
  [
    typeof(AbbB2xMeasurementEntity),
    typeof(SchneideriEM3xxxMeasurementEntity)
  ];

  private static readonly List<Type> AggregateTypes =
  [
    typeof(AbbB2xAggregateEntity),
    typeof(SchneideriEM3xxxAggregateEntity)
  ];

  private static readonly List<IntervalEntity> Intervals =
    Enum.GetValues<IntervalEntity>().ToList();

  protected override async Task ExecuteAsync(
    CancellationToken stoppingToken
  )
  {
    await using var scope = serviceProvider
      .CreateAsyncScope();

    var up = await Up(scope.ServiceProvider, stoppingToken);
    var down = await Down(scope.ServiceProvider, stoppingToken);
    // TODO: figure out why Indent(18) and not Indent(12) is needed
    var migration = $@"
      using Microsoft.EntityFrameworkCore.Migrations;

      #nullable disable

      namespace Ozds.Data.Migrations
      {{
          /// <inheritdoc />
          public partial class {arguments.Name} : Migration
          {{
              /// <inheritdoc />
              protected override void Up(MigrationBuilder migrationBuilder)
              {{
                  {up.Indent(18).Trim()}
              }}

              /// <inheritdoc />
              protected override void Down(MigrationBuilder migrationBuilder)
              {{
                  {down.Indent(18).Trim()}
              }}
          }}
      }}
    ".Dedent(6).Trim();

    await File.WriteAllTextAsync(
      arguments.Output,
      migration,
      stoppingToken);

    var applicationLifetime = scope.ServiceProvider
      .GetRequiredService<IHostApplicationLifetime>();
    applicationLifetime.StopApplication();
  }

  private async Task<string> Up(
    IServiceProvider serviceProvider,
    CancellationToken cancellationToken
  )
  {
    var measurementProcedures = serviceProvider
      .GetRequiredService<MeasurementProcedures>();
    await using var context = await serviceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContextAsync(cancellationToken);

    var statements = new List<string>();
    foreach (var type in MeasurementTypes)
    {
      statements.Add(
        await Statement(
          measurementProcedures
            .OverwriteUpsertMeasurements(context, type),
          cancellationToken));
    }

    foreach (var (type, interval) in AggregateTypes.SelectMany(
      type =>
        Intervals.Select(interval => (type, interval))))
    {
      statements.Add(
        await Statement(
          measurementProcedures
            .OverwriteUpsertAggregates(context, type, interval),
          cancellationToken));
    }

    return string.Join(Environment.NewLine, statements);
  }

  private async Task<string> Down(
    IServiceProvider serviceProvider,
    CancellationToken cancellationToken
  )
  {
    var measurementProcedures = serviceProvider
      .GetRequiredService<MeasurementProcedures>();
    await using var context = await serviceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContextAsync(cancellationToken);

    var statements = new List<string>();
    foreach (var type in MeasurementTypes)
    {
      statements.Add(
        await Statement(
          measurementProcedures
            .DeleteUpsertMeasurements(context, type),
          cancellationToken));
    }

    foreach (var (type, interval) in AggregateTypes.SelectMany(
      type =>
        Intervals.Select(interval => (type, interval))))
    {
      statements.Add(
        await Statement(
          measurementProcedures
            .DeleteUpsertAggregates(context, type, interval),
          cancellationToken));
    }

    return string.Join(Environment.NewLine, statements);
  }

  private async Task<string> Statement(
    string sql,
    CancellationToken cancellationToken
  )
  {
    if (arguments.Formatter is { } formatter)
    {
      try
      {
        var formatterCommand = formatter
          .Split(' ', '\n', '\r', '\t')
          .Where(x => !string.IsNullOrWhiteSpace(x));
        var formatterPath = formatterCommand.First();
        var formatterArguments = formatterCommand.Skip(1).ToArray();
        var processStartInfo = new ProcessStartInfo(
          formatterPath,
          formatterArguments
        )
        {
          RedirectStandardInput = true,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false
        };
        using var process = Process.Start(processStartInfo)
          ?? throw new InvalidOperationException(
            $"Failed to start formatter:\n{formatter}");
        await process.StandardInput.WriteAsync(
          sql.AsMemory(),
          cancellationToken);
        await process.StandardInput.FlushAsync(cancellationToken);
        process.StandardInput.Close();
        sql = await process.StandardOutput.ReadToEndAsync(cancellationToken);
        await process.WaitForExitAsync(cancellationToken);
        if (process.ExitCode != 0)
        {
          throw new InvalidOperationException(
            $"Formatter exited with code {process.ExitCode}:\n{formatter}");
        }
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Failed to format SQL:\n{Sql}",
          sql);
      }
    }

    return $@"
      migrationBuilder.Sql(
        @""
          {sql.Indent(4).Trim()}
        ""
      );
    ".Dedent(6).Trim();
  }
}
