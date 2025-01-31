using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Specimens;

public class DiscriminatorColumnSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  private readonly Lazy<HashSet<MemberInfo>> discriminatorColumns =
    new(dbContext.GetDiscriminatorColumns);

  public object Create(object request, ISpecimenContext context)
  {
    if (request is MemberInfo discriminatorColumn
      && discriminatorColumns.Value.Contains(discriminatorColumn))
    {
      return discriminatorColumn.ReflectedType?.Name
        ?? throw new InvalidOperationException(
          "Failed to get discriminator property type name"
        );
    }

    return new NoSpecimen();
  }
}
