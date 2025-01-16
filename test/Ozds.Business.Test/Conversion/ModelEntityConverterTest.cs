using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Test.Conversion;

public class ModelEntityConverterTest
{
  public static readonly
    TheoryData<Type> TestData = new(
      AppDomain.CurrentDomain
        .GetAssemblies()
        .SelectMany(assembly => assembly
          .GetTypes()
          .Where(type => type.IsAssignableTo(typeof(IModel)))));

  [Theory]
  [MemberData(nameof(TestData))]
  public void Converts(Type modelType)
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddActivation();
    serviceCollection.AddConversion();

    var serviceProvider = serviceCollection.BuildServiceProvider();
    var activator = serviceProvider
      .GetRequiredService<AgnosticModelActivator>();
    var modelEntityConverter = serviceProvider
      .GetRequiredService<AgnosticModelEntityConverter>();

    var activationType = (TestData as IEnumerable<Type>)
      .FirstOrDefault(type =>
        !type.IsAbstract
        && type.IsAssignableTo(modelType))!;
    activationType.Should().NotBeNull();
    var activated = activator.ActivateDynamic(activationType);
    activated.Should().NotBeNull().And.BeOfType(activationType);

    var entityType = modelEntityConverter.EntityType(modelType);
    var entity = modelEntityConverter.ToEntity(activated);
    entity.Should().NotBeNull().And.BeAssignableTo(entityType);

    var converted = modelEntityConverter.ToModel(entity);
    converted.Should().NotBeNull().And.BeAssignableTo(modelType);
    converted.Should().BeEquivalentTo(activated);
  }
}
