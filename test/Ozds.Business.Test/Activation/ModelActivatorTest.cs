using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Test.Activation;

public class ModelActivatorTest
{
  public static readonly
    TheoryData<Type> TestData = new(
      AppDomain.CurrentDomain
        .GetAssemblies()
        .SelectMany(assembly => assembly
          .GetTypes()
          .Where(type =>
            !type.IsGenericType
            && type.IsAssignableTo(typeof(IModel)))));

  [Theory]
  [MemberData(nameof(TestData))]
  public void Activates(Type modelType)
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddActivation();

    var serviceProvider = serviceCollection.BuildServiceProvider();
    var activator = serviceProvider
      .GetRequiredService<AgnosticModelActivator>();

    var model = activator.ActivateDynamic(modelType);
    model.Should().NotBeNull().And.BeOfType(modelType);
  }
}
