using Microsoft.EntityFrameworkCore;
using Ozds.Data.Test.Specimens;

namespace Ozds.Data.Test.Extensions;

public static class DbContextExtensions
{
  public static Fixture Fixture(this DbContext context)
  {
    var fixture = new Fixture();

    fixture.Customizations
      .Add(new IgnoreNavigationsSpecimenBuilder(context));
    fixture.Customizations
      .Add(new IgnoreKeysSpecimenBuilder(context));
    fixture.Customizations
      .Add(new IgnoreForeignKeysSpecimenBuilder(context));
    fixture.Customizations
      .Add(new IgnoreIgnoredPropertiesSpecimenBuilder(context));

    return fixture;
  }
}