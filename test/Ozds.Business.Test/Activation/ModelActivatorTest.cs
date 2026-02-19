using Ozds.Business.Activation;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Test.Base;

namespace Ozds.Business.Test.Activation;

public class ModelActivatorTest : OzdsBusinessHostTestBase
{
  public static IEnumerable<Type> TestData()
  {
    return AppDomain
      .CurrentDomain.GetAssemblies()
      .Where(x => x.FullName is { } name && name.Contains("Ozds"))
      .SelectMany(assembly =>
        assembly
          .GetTypes()
          .Where(type =>
            !type.IsGenericType && type.IsAssignableTo(typeof(IModel))
          )
      );
  }

  [Test]
  [MethodDataSource(nameof(TestData))]
  public void Activates(Type modelType)
  {
    var activator = Host.Services.GetRequiredService<ModelActivator>();

    var model = activator.ActivateDynamic(modelType);
    model.Should().NotBeNull().And.BeAssignableTo(modelType);
  }
}
