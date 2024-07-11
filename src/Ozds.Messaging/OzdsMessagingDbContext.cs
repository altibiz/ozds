using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Messaging;

public class OzdsMessagingDbContext(
  DbContextOptions<OzdsMessagingDbContext> options)
  : SagaDbContext(options)
{
  protected override IEnumerable<ISagaClassMap> Configurations
  {
    get
    {
      return typeof(OzdsMessagingDbContext).Assembly.GetTypes()
        .Where(
          x =>
            x.IsClass &&
            !x.IsAbstract &&
            x.IsAssignableTo(typeof(ISagaClassMap)))
        .Select(Activator.CreateInstance)
        .Cast<ISagaClassMap>();
    }
  }

  protected override void OnConfiguring(
    DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSnakeCaseNamingConvention();

    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.AddInboxStateEntity();
    modelBuilder.AddOutboxMessageEntity();
    modelBuilder.AddOutboxStateEntity();
  }
}
