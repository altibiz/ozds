using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Jobs.Context;

public class JobsDbContext(
  DbContextOptions<JobsDbContext> options
) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.AddQuartz(builder => builder.UsePostgreSql());
  }
}
