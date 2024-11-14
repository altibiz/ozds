using System.Reflection;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Assertions;

public class IgnoreNavigationsOptions(
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
      navigations.Value.Contains(
        member.DeclaringType
          .GetMember(member.Name)
          .OfType<PropertyInfo>()
          .FirstOrDefault()!) ||
      navigations.Value.Contains(
        member.DeclaringType
          .GetMember(member.Name)
          .OfType<FieldInfo>()
          .FirstOrDefault()!));

  private readonly Lazy<HashSet<MemberInfo>> navigations =
    new(dbContext.GetNavigations);
}
