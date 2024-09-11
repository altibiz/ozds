using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ozds.Messaging.Context;

public class MessagingDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<MessagingDbContext>
{
  public MessagingDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<MessagingDbContext>();
    optionsBuilder
      .UseNpgsql(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
        x =>
        {
          x.MigrationsAssembly(
            typeof(MessagingDbContext).Assembly.GetName().Name);
          x.MigrationsHistoryTable(
            $"__{nameof(MessagingDbContext)}");
        });
    return new MessagingDbContext(optionsBuilder.Options);
  }
}
