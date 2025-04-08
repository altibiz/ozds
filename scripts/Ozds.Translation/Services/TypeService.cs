using System.Collections;
using System.Globalization;
using System.Reflection;
using Ozds.Assets;
using Ozds.Assets.Entities;
using Ozds.Assets.Extensions;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Translation.Arguments;
using Ozds.Translation.Services.Base;
using Ozds.Translation.Workers;

namespace Ozds.Translation.Services;

public class TypeService(
  IServiceProvider services,
  OzdsTranslationTypeArguments arguments,
  ILogger<TypeService> logger
) : EnumeratedService<TranslationWorkerItem, TranslationWorker>(
  services
)
{
  private static readonly string AdditionalPropertyPrompt =
    @"
      You are tasked with translating model property paths from the source
      language to human-readable UI elements in the target language. Your
      translations will be used as labels, headings, and form fields in a web
      application.

      As these are model property paths, they follow a certain set of rules:
      - For nested properties, always translate only the outermost property or,
        in certain cases, the outermost two as explained below
      - For properties that are like `DateFrom` or `DateTo`, always translate
        them as if they are `Date from` or `Date to` and not `Begin date` or
        `End date`

      Certain properties are electrical terms or their prices (on measurement,
      aggregate, catalogue, calculation and invoice models) and they follow a
      specific format. The general format is:
      `({Item})?{PropertyName}{Phases}{Direction}{Tariff}_{Unit}(.{Aggregate}?)`.
      Please follow these rules for translating these specific properties:
      - Item, if applicable, is either Supply or Usage - in any case, discard
        it
      - Always translate the property name in the target language
      - Phases is always either L1/L2/L3 or Total - in the case of L1/L2/L3,
        leave them as on L1/L2/L3 and in the case of Total, translate them as
        'on all phases'
      - Direction is always import, export or any - import or export should
        always be translated as imported or exported while any has the special
        meaning of the direction not mattering (e.g. for voltage the direction
        doesn't matter) and should be discarded
      - Tariff is always T0, T1, T2 and it should always be translated
        as explained before
      - Unit should be left as-is and kept in parenthesis as explained before
      - If the property ends with `.{Aggregate}` like
        Min/Max/Avg/MinTimestamp/MaxTimestamp it means that the property is an
        aggregate of the property itself and that you should keep that in mind
        when translating the entire property

      Certain properties are archived and you should always disregard that
      they're archived and just focus on translating the property itself.

      Use the following examples as reference - you will most likely be tasked
      to translate to a language other than English.
      Examples:
      ---------
      Translate the following from English to English.
      English: AbbB2xAggregateModel.Timestamp
      English: Time
      ---------
      Translate the following from English to English.
      English: BlueLowNetworkUserCalculationModel.SupplyActiveEnergyTotalImportT2.Min_kWh
      English: Minimum of active energy import in lower tariff on all phases (kWh)
      ---------
      Translate the following from English to English.
      English: MessengerEventModel.Id
      English: Identifier
      ---------
      Translate the following from English to English.
      English: LocationModel.LegalPerson.PostalCode
      English: Postal code
      ---------
      Translate the following from English to English.
      English: NetworkUserCalculationModel<TNetworkUserCatalogue>.TotalWithTax_EUR
      English: Total with tax (EUR)
      ---------
      Translate the following from English to English.
      English: AbbB2xMeterModel.CreatedById
      English: Created by
      ---------
      Translate the following from English to English.
      English: RepresentativeModel.IsDeleted
      English: Deleted
      ---------
      Translate the following from English to English.
      English: FinancialModel.ToDate
      English: To date
      ---------
      Translate the following from English to English.
      English: NetworkUserInvoiceModel.Total_EUR
      English: Total (EUR)
      ---------
      Translate the following from English to English.
      English: BlueLowNetworkUserCalculationModel.UsageMeterFee.Price_EUR
      English: Usage meter fee price (EUR)
      ---------
      Translate the following from English to English.
      English: NetworkUserInvoiceModel.ArchivedNetworkUser.CreatedOn
      English: Created on
      ---------
      Translate the following from English to English.
      English: AbbB2xAggregateModel.DerivedActivePowerL1ImportT0_W.MinTimestamp
      English: Time of imported derived active power on L1 in one tariff minimum (W)
      ---------
      Translate the following from English to English.
      English: SchneideriEM3xxxAggregateModel.QuarterHourCount
      English: Quarter hour aggregate count
      ---------
      Translate the following from English to English.
      English: SchneideriEM3xxxAggregateModel.Count
      English: Measurement count
      ---------
      Translate the following from English to English.
      English: ActiveEnergyTotalImportCalculationItemModel.Min_kWh
      English: Minimal imported active energy on all phases (kWh)
      ---------
      Translate the following from English to English.
      English: RedLowNetworkUserCalculationModel.UsageActivePowerTotalImportT1Peak.Amount_kW
      English: Peak of imported active power on all phases in higher tariff (kW)
      ---------
      End of examples.
      Remember: these were only examples of translation.
    ".Dedent(6).Trim();

  private static readonly string AdditionalTypePrompt =
    @"
      You are tasked with translating types from the source
      language to human-readable UI elements in the target language. Your
      translations will be used as labels, headings, and form fields in a web
      application.

      Types sometimes end with 'Model' or 'Entity' and you should always
      discard those suffixes.

      Certain types use electrical terms or their prices (on measurement,
      aggregate, catalogue, calculation and invoice models). They follow a
      specific format. The general format is:
      `({Item})?{PropertyName}{Phases}{Direction}{Tariff}{Suffix}`.
      Please follow these rules for translating these specific properties:
      - Item, if applicable, is either Supply or Usage - in any case, discard
        it
      - Always translate the property name in the target language
      - Phases is always either L1/L2/L3 or Total - in the case of L1/L2/L3,
        leave them as on L1/L2/L3 and in the case of Total, translate them as
        'on all phases'
      - Direction is always import, export or any - import or export should
        always be translated as imported or exported while any has the special
        meaning of the direction not mattering (e.g. for voltage the direction
        doesn't matter) and should be discarded
      - Tariff is always T0, T1, T2 and it should always be translated
        as explained before
      - Unit should be left as-is and kept in parenthesis as explained before
      - If the property ends with `.{Aggregate}` like
        Min/Max/Avg/MinTimestamp/MaxTimestamp it means that the property is an
        aggregate of the property itself and that you should keep that in mind
        when translating the entire property

      Use the following examples as reference - you will most likely be tasked
      to translate to a language other than English.
      Examples:
      ---------
      Translate the following from English to English.
      English: AbbB2xAggregateModel
      English: abb-B2x metering aggregate
      ---------
      Translate the following from English to English.
      English: SchneideriEM3xxxAggregateModel
      English: schneider-iEM3xxx metering aggregate
      ---------
      Translate the following from English to English.
      English: RedLowNetworkUserCatalogueModel
      English: Red low network user catalogue
      ---------
      Translate the following from English to English.
      English: NetworkUserInvoiceModel
      English: Network user invoice
      ---------
      Translate the following from English to English.
      English: MeasurementLocationAnalysis
      English: Measurement location analysis
      ---------
      Translate the following from English to English.
      English: PidgeonMessengerModel
      English: Pidgeon messenger model
      ---------
      Translate the following from English to English.
      English: ActiveEnergyTotalImportT0CalculationItemModel
      English: Calculation item of active energy import on all phases in one tariff
      ---------
      Translate the following from English to English.
      English: UsageReactiveEnergyTotalRampedT0CalculationItemModel
      English: Calculation item of ramped reactive energy import on all phases in one tariff
      ---------
      End of examples.
      Remember: these were only examples of translation.
    ".Dedent(6).Trim();

  private readonly IServiceProvider services = services;

  private TranslationDictionaryEntity dictionary =
    TranslationDictionaryEntity.Empty;

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    if (arguments.UpdateFilePath is not null)
    {
      dictionary = await TranslationDictionaryEntity.Load(
        arguments.UpdateFilePath,
        cancellationToken
      );
    }

    await base.StartAsync(cancellationToken);
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    await dictionary.Save(arguments.OutputFilePath, cancellationToken);

    await base.StopAsync(cancellationToken);
  }

  protected override IEnumerable<TranslationWorkerItem> GetEnumerable()
  {
    var assembly = AppDomain.CurrentDomain
        .GetAssemblies()
        .FirstOrDefault(
          assembly =>
            assembly.GetName().Name == arguments.InputAssemblyName)
      ?? throw new InvalidOperationException(
        $"Could not find assembly: {arguments.InputAssemblyName}");

    logger.LogInformation("Found assembly: {assembly}", assembly);

    var types = assembly
      .GetTypes()
      .Where(
        type =>
          (type.IsClass || type.IsInterface)
          && type.Namespace is not null
          && arguments.InputAssemblyNamespaces.Any(
            type.Namespace.StartsWith));

    foreach (var type in types)
    {
      // NITPICK: this is disgusting
      using var scope = services.CreateScope();
      var localizationQueries = scope.ServiceProvider
        .GetRequiredService<ILocalizationQueries>();

      var prefix = localizationQueries.Key(type);

      // NITPICK: this is yucky
      if (prefix.StartsWith('<'))
      {
        continue;
      }

      if (!dictionary.Contains(prefix))
      {
        logger.LogInformation("Found new type: {Type}", prefix);

        yield return new TranslationWorkerItem(
          dictionary,
          prefix,
          prefix,
          AssetConstants.EnglishCulture,
          new CultureInfo(arguments.Language),
          AdditionalTypePrompt
        );
      }

      foreach (var path in GetPathsRecursive(type, prefix).Distinct())
      {
        if (dictionary.Contains(path))
        {
          continue;
        }

        logger.LogInformation("Found new property path: {Path}", path);

        yield return new TranslationWorkerItem(
          dictionary,
          path,
          path,
          AssetConstants.EnglishCulture,
          new CultureInfo(arguments.Language),
          AdditionalPropertyPrompt
        );
      }
    }
  }

  private IEnumerable<string> GetPathsRecursive(
    Type type,
    string pathPrefix
  )
  {
    var properties = type.GetProperties(
      BindingFlags.Public | BindingFlags.Instance
    );
    foreach (var property in properties)
    {
      var propertyType = property.PropertyType;
      var fullPath = $"{pathPrefix}.{property.Name}";

      if (
        propertyType != typeof(string)
        && typeof(IEnumerable).IsAssignableFrom(propertyType)
      )
      {
        var elementType = propertyType.IsGenericType
          ? propertyType.GetGenericArguments().FirstOrDefault()
          : null;
        if (
          elementType != null
          && elementType.IsClass
          && elementType.Namespace != null
          && arguments.InputAssemblyNamespaces.Any(
            elementType.Namespace.StartsWith)
        )
        {
          foreach (var path in
            GetPathsRecursive(
              elementType,
              fullPath))
          {
            yield return path;
          }
        }
        else
        {
          yield return fullPath;
        }
      }
      else if (
        propertyType.IsClass
        && propertyType != typeof(string)
        && propertyType.Namespace != null
        && arguments.InputAssemblyNamespaces.Any(
          propertyType.Namespace.StartsWith)
      )
      {
        foreach (var path in
          GetPathsRecursive(
            propertyType,
            fullPath))
        {
          yield return path;
        }
      }
      else
      {
        yield return fullPath;
      }
    }
  }
}
