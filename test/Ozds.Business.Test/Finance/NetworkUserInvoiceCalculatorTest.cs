using Ozds.Business.Finance;
using Ozds.Business.Finance.Implementations;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

// NITPICK: test issuer and date

namespace Ozds.Business.Test.Finance;

public class NetworkUserInvoiceCalculatorTest
{
  public static readonly
    TheoryData<
      CalculatedNetworkUserInvoiceModel>
    TestData = new(
      new Fixture()
        .Customize(
          new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
            .ToCustomization())
        .Customize(
          new TypeRelay(typeof(AggregateModel), typeof(AbbB2xAggregateModel))
            .ToCustomization())
        .Customize(
          new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
            .ToCustomization())
        .Customize(
          new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
            .ToCustomization())
        .Customize(
          new TypeRelay(
            typeof(INetworkUserCalculation),
            typeof(BlueLowNetworkUserCalculationModel)).ToCustomization())
        .Customize(
          new TypeRelay(
            typeof(NetworkUserCalculationModel),
            typeof(BlueLowNetworkUserCalculationModel)).ToCustomization())
        .Build<CalculatedNetworkUserInvoiceModel>()
        .With(
          x => x.Calculations,
          Enumerable.Empty<NetworkUserCalculationModel>()
            .Concat(
              new Fixture()
                .Customize(
                  new TypeRelay(
                      typeof(AggregateModel), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(
                      typeof(IAggregate), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Build<BlueLowNetworkUserCalculationModel>()
                .CreateMany(Constants.DefaultFuzzCount / 8)
                .Select(
                  x =>
                  {
                    x.UsageNetworkUserCatalogueId =
                      x.ConcreteArchivedUsageNetworkUserCatalogue.Id;
                    x.SupplyRegulatoryCatalogueId =
                      x.ArchivedSupplyRegulatoryCatalogue.Id;
                    x.NetworkUserMeasurementLocationId =
                      x.ArchivedNetworkUserMeasurementLocation.Id;
                    x.MeterId = x.ArchivedMeter.Id;

                    var faker = new Faker();

                    x.UsageActiveEnergyTotalImportT0.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageReactiveEnergyTotalRampedT0.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageMeterFee.Total_EUR = System.Math.Round(
                      faker.Random.Decimal(
                        Constants.MinTotalValue,
                        Constants.MaxTotalValue),
                      2);

                    x.SupplyActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.SupplyActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
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
                  }))
            .Concat(
              new Fixture()
                .Customize(
                  new TypeRelay(
                      typeof(AggregateModel), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(
                      typeof(IAggregate), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Build<RedLowNetworkUserCalculationModel>()
                .CreateMany(Constants.DefaultFuzzCount / 8)
                .Select(
                  x =>
                  {
                    x.UsageNetworkUserCatalogueId =
                      x.ConcreteArchivedUsageNetworkUserCatalogue.Id;
                    x.SupplyRegulatoryCatalogueId =
                      x.ArchivedSupplyRegulatoryCatalogue.Id;
                    x.NetworkUserMeasurementLocationId =
                      x.ArchivedNetworkUserMeasurementLocation.Id;
                    x.MeterId = x.ArchivedMeter.Id;

                    var faker = new Faker();

                    x.UsageActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageReactiveEnergyTotalRampedT0.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageActivePowerTotalImportT1Peak.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageMeterFee.Total_EUR = System.Math.Round(
                      faker.Random.Decimal(
                        Constants.MinTotalValue,
                        Constants.MaxTotalValue),
                      2);

                    x.SupplyActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.SupplyActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
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
                      x.UsageActiveEnergyTotalImportT1.Total
                      + x.UsageActiveEnergyTotalImportT2.Total
                      + x.UsageActivePowerTotalImportT1Peak.Total
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
                  }))
            .Concat(
              new Fixture()
                .Customize(
                  new TypeRelay(
                      typeof(AggregateModel), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(
                      typeof(IAggregate), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Build<WhiteLowNetworkUserCalculationModel>()
                .CreateMany(Constants.DefaultFuzzCount / 8)
                .Select(
                  x =>
                  {
                    x.UsageNetworkUserCatalogueId =
                      x.ConcreteArchivedUsageNetworkUserCatalogue.Id;
                    x.SupplyRegulatoryCatalogueId =
                      x.ArchivedSupplyRegulatoryCatalogue.Id;
                    x.NetworkUserMeasurementLocationId =
                      x.ArchivedNetworkUserMeasurementLocation.Id;
                    x.MeterId = x.ArchivedMeter.Id;

                    var faker = new Faker();

                    x.UsageActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageReactiveEnergyTotalRampedT0.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageMeterFee.Total_EUR = System.Math.Round(
                      faker.Random.Decimal(
                        Constants.MinTotalValue,
                        Constants.MaxTotalValue),
                      2);

                    x.SupplyActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.SupplyActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
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
                      x.UsageActiveEnergyTotalImportT1.Total
                      + x.UsageActiveEnergyTotalImportT2.Total
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
                  }))
            .Concat(
              new Fixture()
                .Customize(
                  new TypeRelay(
                      typeof(AggregateModel), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(
                      typeof(IAggregate), typeof(AbbB2xAggregateModel))
                    .ToCustomization())
                .Customize(
                  new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
                    .ToCustomization())
                .Build<WhiteMediumNetworkUserCalculationModel>()
                .CreateMany(Constants.DefaultFuzzCount / 8)
                .Select(
                  x =>
                  {
                    x.UsageNetworkUserCatalogueId =
                      x.ConcreteArchivedUsageNetworkUserCatalogue.Id;
                    x.SupplyRegulatoryCatalogueId =
                      x.ArchivedSupplyRegulatoryCatalogue.Id;
                    x.NetworkUserMeasurementLocationId =
                      x.ArchivedNetworkUserMeasurementLocation.Id;
                    x.MeterId = x.ArchivedMeter.Id;

                    var faker = new Faker();

                    x.UsageActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageReactiveEnergyTotalRampedT0.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageActivePowerTotalImportT1Peak.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.UsageMeterFee.Total_EUR = System.Math.Round(
                      faker.Random.Decimal(
                        Constants.MinTotalValue,
                        Constants.MaxTotalValue),
                      2);

                    x.SupplyActiveEnergyTotalImportT1.Total_EUR =
                      System.Math.Round(
                        faker.Random.Decimal(
                          Constants.MinTotalValue,
                          Constants.MaxTotalValue),
                        2);

                    x.SupplyActiveEnergyTotalImportT2.Total_EUR =
                      System.Math.Round(
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
                      x.UsageActiveEnergyTotalImportT1.Total
                      + x.UsageActiveEnergyTotalImportT2.Total
                      + x.UsageActivePowerTotalImportT1Peak.Total
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
                  }))
            .OrderBy(_ => Random.Shared.Next())
            .ToList())
        .CreateMany(2)
        .Select(
          x =>
          {
            x.Invoice.NetworkUserId = x.Invoice.ArchivedNetworkUser.Id;
            x.Invoice.BillId = null;

            x.Invoice.UsageActiveEnergyTotalImportT0Fee_EUR = System.Math.Round(
              x.Calculations
                .SelectMany(
                  calculation => calculation.UsageItems
                    .OfType<
                      UsageActiveEnergyTotalImportT0CalculationItemModel>())
                .Sum(item => item.Total),
              2);

            x.Invoice.UsageActiveEnergyTotalImportT1Fee_EUR = System.Math.Round(
              x.Calculations
                .SelectMany(
                  calculation => calculation.UsageItems
                    .OfType<
                      UsageActiveEnergyTotalImportT1CalculationItemModel>())
                .Sum(item => item.Total),
              2);

            x.Invoice.UsageActiveEnergyTotalImportT2Fee_EUR = System.Math.Round(
              x.Calculations
                .SelectMany(
                  calculation => calculation.UsageItems
                    .OfType<
                      UsageActiveEnergyTotalImportT2CalculationItemModel>())
                .Sum(item => item.Total),
              2);

            x.Invoice.UsageActivePowerTotalImportT1PeakFee_EUR =
              System.Math.Round(
                x.Calculations
                  .SelectMany(
                    calculation => calculation.UsageItems
                      .OfType<
                        UsageActivePowerTotalImportT1PeakCalculationItemModel>())
                  .Sum(item => item.Total),
                2);

            x.Invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR =
              System.Math.Round(
                x.Calculations
                  .SelectMany(
                    calculation => calculation.UsageItems
                      .OfType<
                        UsageReactiveEnergyTotalRampedT0CalculationItemModel>())
                  .Sum(item => item.Total),
                2);

            x.Invoice.UsageMeterFee_EUR = System.Math.Round(
              x.Calculations
                .SelectMany(
                  calculation => calculation.UsageItems
                    .OfType<UsageMeterFeeCalculationItemModel>())
                .Sum(item => item.Total),
              2);

            x.Invoice.UsageFeeTotal_EUR = System.Math.Round(
              x.Invoice.UsageActiveEnergyTotalImportT0Fee_EUR
              + x.Invoice.UsageActiveEnergyTotalImportT1Fee_EUR
              + x.Invoice.UsageActiveEnergyTotalImportT2Fee_EUR
              + x.Invoice.UsageActivePowerTotalImportT1PeakFee_EUR
              + x.Invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR
              + x.Invoice.UsageMeterFee_EUR,
              2);

            x.Invoice.SupplyActiveEnergyTotalImportT1Fee_EUR =
              System.Math.Round(
                x.Calculations
                  .SelectMany(
                    calculation => calculation.SupplyItems
                      .OfType<
                        SupplyActiveEnergyTotalImportT1CalculationItemModel>())
                  .Sum(item => item.Total),
                2);

            x.Invoice.SupplyActiveEnergyTotalImportT2Fee_EUR =
              System.Math.Round(
                x.Calculations
                  .SelectMany(
                    calculation => calculation.SupplyItems
                      .OfType<
                        SupplyActiveEnergyTotalImportT2CalculationItemModel>())
                  .Sum(item => item.Total),
                2);

            x.Invoice.SupplyBusinessUsageFee_EUR = System.Math.Round(
              x.Calculations
                .SelectMany(
                  calculation => calculation.SupplyItems
                    .OfType<SupplyBusinessUsageCalculationItemModel>())
                .Sum(item => item.Total),
              2);

            x.Invoice.SupplyRenewableEnergyFee_EUR = System.Math.Round(
              x.Calculations
                .SelectMany(
                  calculation => calculation.SupplyItems
                    .OfType<SupplyRenewableEnergyCalculationItemModel>())
                .Sum(item => item.Total),
              2);

            x.Invoice.SupplyFeeTotal_EUR = System.Math.Round(
              x.Invoice.SupplyActiveEnergyTotalImportT1Fee_EUR
              + x.Invoice.SupplyActiveEnergyTotalImportT2Fee_EUR
              + x.Invoice.SupplyBusinessUsageFee_EUR
              + x.Invoice.SupplyRenewableEnergyFee_EUR,
              2);

            x.Invoice.Total_EUR = System.Math.Round(
              x.Invoice.UsageFeeTotal_EUR + x.Invoice.SupplyFeeTotal_EUR,
              2);
            x.Invoice.InvoiceTaxRate_Percent = System.Math.Round(
              x.Invoice.ArchivedRegulatoryCatalogue.TaxRate_Percent,
              2);
            x.Invoice.InvoiceTax_EUR = System.Math.Round(
              x.Invoice.Total_EUR * x.Invoice.TaxRate_Percent / 100M,
              2);
            x.Invoice.InvoiceTotalWithTax_EUR = System.Math.Round(
              x.Invoice.Total_EUR + x.Invoice.Tax_EUR,
              2);

            return x;
          }));

  [Theory]
  [MemberData(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xMeter(
    CalculatedNetworkUserInvoiceModel expected)
  {
    var calculationItemCalculatorMock =
      new Mock<NetworkUserCalculationCalculator>(
        MockBehavior.Strict,
        new Mock<IServiceProvider>().Object);

    var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
    httpContextAccessorMock.Setup(x => x.HttpContext)
      .Returns(new DefaultHttpContext());

    var mockSequence = calculationItemCalculatorMock.SetupSequence(
      x => x.Calculate(It.IsAny<NetworkUserCalculationBasisModel>()));
    foreach (var calculation in expected.Calculations)
    {
      mockSequence.Returns(calculation);
    }

    mockSequence.Throws(new InvalidOperationException("No more calculations"));

    var calculator = new NetworkUserInvoiceCalculator(
      calculationItemCalculatorMock.Object);

    var fixture = new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(AggregateModel), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(
          typeof(NetworkUserCatalogueModel),
          typeof(BlueLowNetworkUserCatalogueModel)).ToCustomization())
      .Customize(
        new TypeRelay(
          typeof(INetworkUserCatalogue),
          typeof(BlueLowNetworkUserCatalogueModel)).ToCustomization());

    var basis = fixture
      .Build<NetworkUserInvoiceIssuingBasisModel>()
      .With(x => x.FromDate, expected.Invoice.FromDate)
      .With(x => x.ToDate, expected.Invoice.ToDate)
      .With(x => x.Location, expected.Invoice.ArchivedLocation)
      .With(x => x.NetworkUser, expected.Invoice.ArchivedNetworkUser)
      .With(
        x => x.RegulatoryCatalogue,
        expected.Invoice.ArchivedRegulatoryCatalogue)
      .With(
        x => x.NetworkUserCalculationBases,
        expected.Calculations
          .Select(
            expected => fixture
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
                expected.ArchivedUsageNetworkUserCatalogue)
              .With(x => x.Meter, expected.ArchivedMeter)
              .Create())
          .ToList())
      .Create();

    var actual = calculator.Calculate(basis);

    actual.Should()
      .BeOfType<CalculatedNetworkUserInvoiceModel>().And
      .BeEquivalentTo(
        expected,
        c => c
          .Excluding(x => x.Invoice.Id)
          .Excluding(x => x.Invoice.Title)
          .Excluding(x => x.Invoice.IssuedOn)
          .Excluding(x => x.Invoice.IssuedById));
  }
}
