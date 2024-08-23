using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Timers;

public class OzdsTimersDbContext(
  DbContextOptions<OzdsTimersDbContext> options
) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.AddQuartz(builder => builder.UsePostgreSql());
  }
}
