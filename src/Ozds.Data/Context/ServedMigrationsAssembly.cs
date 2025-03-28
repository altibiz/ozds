using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Ozds.Data.Extensions;

namespace Ozds.Data.Context;

public class ServedMigrationsAssembly(
  ICurrentDbContext currentContext,
  IDbContextOptions options,
  IMigrationsIdGenerator idGenerator,
  IDiagnosticsLogger<DbLoggerCategory.Migrations> logger
#pragma warning disable EF1001 // Internal EF Core API usage.
) : MigrationsAssembly(currentContext, options, idGenerator, logger)
#pragma warning restore EF1001 // Internal EF Core API usage.
{
  public override Migration CreateMigration(
    TypeInfo migrationClass,
    string activeProvider
  )
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddOzdsData();
    var serviceProvider = serviceCollection.BuildServiceProvider();

    var migration = ActivatorUtilities.CreateInstance(
        serviceProvider,
        migrationClass.AsType()
      ) as Migration
      ?? throw new InvalidOperationException(
        $"Migration {migrationClass.Name} activation failed.");
    migration.ActiveProvider = activeProvider;

    return migration;
  }
}
