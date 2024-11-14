using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Specimens;

public class IgnoreForeignKeysSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo or FieldInfo
      && foreignKeys.Value.Contains(request))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }

  private readonly Lazy<HashSet<MemberInfo>> foreignKeys =
    new(dbContext.GetForeignKeys);
}
