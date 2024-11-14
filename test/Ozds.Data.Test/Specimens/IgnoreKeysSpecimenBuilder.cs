using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Specimens;

public class IgnoreKeysSpecimenBuilder(
  DbContext dbContext
) : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is PropertyInfo or FieldInfo
      && keys.Value.Contains(request))
    {
      return new OmitSpecimen();
    }

    return new NoSpecimen();
  }

  private readonly Lazy<HashSet<MemberInfo>> keys = new(dbContext.GetKeys);
}
