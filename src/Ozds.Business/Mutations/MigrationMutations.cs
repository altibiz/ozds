using Ozds.Business.Mutations.Abstractions;
using DataMigrationMutations = Ozds.Data.Mutations.MigrationMutations;
using JobsMigrationMutations = Ozds.Jobs.Mutations.MigrationMutations;
using MessagingMigrationMutations = Ozds.Messaging.Mutations.MigrationMutations;

namespace Ozds.Business.Mutations;

public class MigrationMutations(
  DataMigrationMutations dataMutations,
  JobsMigrationMutations jobsMutations,
  MessagingMigrationMutations messagingMutations
) : IMutations
{
  public async Task MigrateAsync(CancellationToken cancellationToken)
  {
    await dataMutations.MigrateAsync(cancellationToken);
    await jobsMutations.MigrateAsync(cancellationToken);
    await messagingMutations.MigrateAsync(cancellationToken);
  }
}
