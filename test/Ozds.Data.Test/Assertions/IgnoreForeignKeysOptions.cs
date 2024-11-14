using System.Reflection;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Assertions;

public class IgnoreForeignKeysOptions(
  DbContext dbContext
)
{
  public SelfReferenceEquivalencyAssertionOptions<TSelf> Configure<
    TSelf
  >(
    SelfReferenceEquivalencyAssertionOptions<TSelf> options
  )
    where TSelf : SelfReferenceEquivalencyAssertionOptions<TSelf>
  =>
    options.Excluding(member =>
      foreignKeys.Value.Contains(
        member.DeclaringType
          .GetMember(member.Name)
          .OfType<PropertyInfo>()
          .FirstOrDefault()!) ||
      foreignKeys.Value.Contains(
        member.DeclaringType
          .GetMember(member.Name)
          .OfType<FieldInfo>()
          .FirstOrDefault()!));

  private readonly Lazy<HashSet<MemberInfo>> foreignKeys =
    new(dbContext.GetForeignKeys);
}
