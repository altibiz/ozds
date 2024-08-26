using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Jobs;

public class OzdsJobsDbContext(
  DbContextOptions<OzdsJobsDbContext> options
) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.AddQuartz(builder => builder.UsePostgreSql());
  }
}
