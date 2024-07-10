using Ozds.Business.Finance;
using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

// TODO: test issuer and date

namespace Ozds.Business.Test.Finance;

public class BlueLowNetworkUserCalculationCalculatorTest
{
  public static readonly
    TheoryData<
      BlueLowNetworkUserCalculationModel>
    TestData = new(new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Build<BlueLowNetworkUserCalculationModel>()
      .CreateMany(Constants.DefaultFuzzCount)
      .Select((BlueLowNetworkUserCalculationModel x) =>
      {
        x.UsageNetworkUserCatalogueId =
          x.ConcreteArchivedUsageNetworkUserCatalogue.Id;
        x.SupplyRegulatoryCatalogueId =
          x.ArchivedSupplyRegulatoryCatalogue.Id;
        x.NetworkUserMeasurementLocationId =
          x.ArchivedNetworkUserMeasurementLocation.Id;
        x.MeterId = x.ArchivedMeter.Id;

        var faker = new Faker();

        x.UsageActiveEnergyTotalImportT0.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.UsageReactiveEnergyTotalRampedT0.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.UsageMeterFee.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.SupplyActiveEnergyTotalImportT1.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.SupplyActiveEnergyTotalImportT2.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.SupplyBusinessUsageFee.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.SupplyRenewableEnergyFee.Total_EUR = System.Math.Round(
          faker.Random.Decimal(
            Constants.MinTotalValue,
            Constants.MaxTotalValue),
          2);

        x.UsageFeeTotal_EUR = System.Math.Round(
          x.UsageActiveEnergyTotalImportT0.Total
          + x.UsageReactiveEnergyTotalRampedT0.Total
          + x.UsageMeterFee.Total,
          2);
        x.SupplyFeeTotal_EUR = System.Math.Round(
          x.SupplyActiveEnergyTotalImportT1.Total
          + x.SupplyActiveEnergyTotalImportT2.Total
          + x.SupplyBusinessUsageFee.Total
          + x.SupplyRenewableEnergyFee.Total,
          2);
        x.Total_EUR = System.Math.Round(
          x.SupplyFeeTotal_EUR + x.UsageFeeTotal_EUR,
          2);

        return x;
      }));

  [Theory]
  [MemberData(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xMeter(
    BlueLowNetworkUserCalculationModel expected)
  {
    var calculationItemCalculatorMock =
      new Mock<AgnosticCalculationItemCalculator>(
        MockBehavior.Strict,
        new Mock<IServiceProvider>().Object);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(UsageActiveEnergyTotalImportT0CalculationItemModel)))
      .Returns(expected.UsageActiveEnergyTotalImportT0);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(UsageReactiveEnergyTotalRampedT0CalculationItemModel)))
      .Returns(expected.UsageReactiveEnergyTotalRampedT0);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(UsageMeterFeeCalculationItemModel)))
      .Returns(expected.UsageMeterFee);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(SupplyActiveEnergyTotalImportT1CalculationItemModel)))
      .Returns(expected.SupplyActiveEnergyTotalImportT1);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(SupplyActiveEnergyTotalImportT2CalculationItemModel)))
      .Returns(expected.SupplyActiveEnergyTotalImportT2);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(SupplyBusinessUsageCalculationItemModel)))
      .Returns(expected.SupplyBusinessUsageFee);

    calculationItemCalculatorMock
      .Setup(x => x
        .Calculate(
          It.IsAny<CalculationItemBasisModel>(),
          typeof(SupplyRenewableEnergyCalculationItemModel)))
      .Returns(expected.SupplyRenewableEnergyFee);

    var calculator = new BlueLowNetworkUserCalculationCalculator(
      calculationItemCalculatorMock.Object);

    var fixture = new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(INetworkUserCatalogue),
          typeof(BlueLowNetworkUserCatalogueModel)).ToCustomization());
    var basis = fixture
      .Build<NetworkUserCalculationBasisModel>()
      .With(x => x.FromDate, expected.FromDate)
      .With(x => x.ToDate, expected.ToDate)
      .With(
        x => x.MeasurementLocation,
        expected.ArchivedNetworkUserMeasurementLocation)
      .With(
        x => x.SupplyRegulatoryCatalogue,
        expected.ArchivedSupplyRegulatoryCatalogue)
      .With(
        x => x.UsageNetworkUserCatalogue,
        expected.ConcreteArchivedUsageNetworkUserCatalogue)
      .With(x => x.Meter, expected.ArchivedMeter)
      .Create();

    var actual = calculator.Calculate(basis);

    actual.Should()
      .BeOfType<BlueLowNetworkUserCalculationModel>().And
      .BeEquivalentTo(
        expected,
        c => c
          .Excluding(x => x.Id)
          .Excluding(x => x.Title)
          .Excluding(x => x.NetworkUserInvoiceId)
          .Excluding(x => x.IssuedOn)
          .Excluding(x => x.IssuedById));
  }
}
