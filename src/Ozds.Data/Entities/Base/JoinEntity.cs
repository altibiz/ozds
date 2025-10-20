using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Base;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods
public abstract class JoinEntity : IJoinEntity
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
{
  public abstract string LeftId { get; }

  public abstract string RightId { get; }
}

public class JoinEntityConfiguration
  : EntityTypeHierarchyConfiguration<JoinEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.Ignore(nameof(AuditableJoinEntity.LeftId));
    builder.Ignore(nameof(AuditableJoinEntity.RightId));
  }
}
