using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Specimens;

public class IgnoreKeysSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  private readonly Lazy<HashSet<MemberInfo>> keys = new(dbContext.GetKeys);

  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo or FieldInfo
      && keys.Value.Contains(request))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }
}
