using System.Collections;
using System.Reflection;
using Ozds.Assets.Entities;
using Ozds.Assets.Extensions;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Translation.Arguments;
using Ozds.Translation.Services.Base;
using Ozds.Translation.Workers;

// NOTE: \n is ok here because we're sending it to ollama anyway

namespace Ozds.Translation.Services;

public class TypeService(
  IServiceProvider services,
  OzdsTranslationTypeArguments arguments,
  ITranslationQueries translationQueries,
  ICultureQueries cultureQueries,
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
    ".Dedent(6, "\n").Trim();

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
      English: Pidgeon messenger
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
    ".Dedent(6, "\n").Trim();

  private static readonly string AdditionalPluralTypePrompt =
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

      You are translating types in plural form. The first character only denotes
      the plurality of the type. The rest of the name is the same as in singular
      form.

      Use the following examples as reference - you will most likely be tasked
      to translate to a language other than English.
      Examples:
      ---------
      Translate the following from English to English.
      English: ~AbbB2xAggregateModel
      English: abb-B2x metering aggregates
      ---------
      Translate the following from English to English.
      English: ~SchneideriEM3xxxAggregateModel
      English: schneider-iEM3xxx metering aggregates
      ---------
      Translate the following from English to English.
      English: ~RedLowNetworkUserCatalogueModel
      English: Red low network user catalogues
      ---------
      Translate the following from English to English.
      English: ~NetworkUserInvoiceModel
      English: Network user invoices
      ---------
      Translate the following from English to English.
      English: ~MeasurementLocationAnalysis
      English: Measurement location analyses
      ---------
      Translate the following from English to English.
      English: ~PidgeonMessengerModel
      English: Pidgeon messengers
      ---------
      Translate the following from English to English.
      English: ~ActiveEnergyTotalImportT0CalculationItemModel
      English: Calculation items of active energy import on all phases in one tariff
      ---------
      Translate the following from English to English.
      English: ~UsageReactiveEnergyTotalRampedT0CalculationItemModel
      English: Calculation items of ramped reactive energy import on all phases in one tariff
      ---------
      End of examples.
      Remember: these were only examples of translation.
    ".Dedent(6, "\n").Trim();

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
    var culture = cultureQueries.IdToCulture(arguments.Language);
    if (culture is null)
    {
      throw new InvalidOperationException(
        $"Could not find culture '{arguments.Language}'");
    }

    var items = GroupItemsAcrossAssemblies(GroupItemsByDeclaration(GetItems()))
      .ToList();

    if (arguments.RemoveUnused)
    {
      // TODO: better way to detect managed translations
      var managedItems = dictionary
        .ToList()
        .Where(
          item => item.Metadata is { } metadata
            && (metadata.StartsWith("Type")
              || metadata.StartsWith("Property")))
        .ToList();

      var unusedManagedItems = managedItems
        .Where(
          dictionaryItem => !items.Exists(
            item =>
              item.Key == dictionaryItem.Key
              || item.ShortKey == dictionaryItem.Key
              || item.AdditionalKeys.Exists(
                x =>
                  x.Key == dictionaryItem.Key
                  || x.ShortKey == dictionaryItem.Key)))
        .ToList();

      foreach (var key in unusedManagedItems.Select(x => x.Key))
      {
        dictionary.Remove(key);
        logger.LogInformation("Removed key '{Key}'", key);
      }
    }

    foreach (var item in items)
    {
      var translated = dictionary.Get(item.Key) ??
        item.AdditionalKeys
          .Select(x => dictionary.Get(x.Key))
          .FirstOrDefault(x => x is not null);
      if (translated is not null)
      {
        dictionary.AddOrUpdate(item.Key, item.Metadata, translated);
        logger.LogInformation(
          "Updated metadata for '{Key}' to\n{Metadata}",
          item.Key,
          item.Metadata
        );

        if (arguments.RemoveOverrides)
        {
          foreach (var (key, shortKey) in item.AdditionalKeys)
          {
            dictionary.Remove(key);
            if (item.IsProperty)
            {
              dictionary.Remove(shortKey);
            }

            logger.LogInformation(
              "Removed key '{Key}' and short key '{ShortKey}'",
              key,
              shortKey
            );
          }
        }

        continue;
      }

      logger.LogInformation("Found new key: {Key}", item.Key);

      yield return new TranslationWorkerItem(
        dictionary,
        item.Key,
        item.Metadata,
        item.Key,
        cultureQueries.EnglishCulture,
        culture,
        item.AdditionalPrompt
      );
    }
  }

  private IEnumerable<GroupedAcrossAssembliesTranslationItem>
    GroupItemsAcrossAssemblies(
      IEnumerable<GroupedByDeclarationTranslationItem> items
    )
  {
    return items
      .GroupBy(
        item => item.Property is { } property
          ? translationQueries.GeneralKey(item.Type, property)
          : translationQueries.GeneralKey(item.Type, plural: item.Plural))
      .Select(
        group =>
        {
          var metadata = string
            .Join("\n\n", group.Select(x => x.Metadata));
          return new GroupedAcrossAssembliesTranslationItem(
            false,
            group.Key,
            group.Key,
            group
              .Select(x => (x.Key, x.ShortKey))
              .Concat(
                group
                  .Where(x => x.Property == null)
                  .GroupBy(
                    x => translationQueries
                      .GeneralKey(x.Type, false))
                  .Where(x => x.Key != group.Key)
                  .Select(x => (x.Key, x.Key)))
              .ToList(),
            metadata,
            AdditionalPropertyPrompt
          );
        });
  }

  private IEnumerable<GroupedByDeclarationTranslationItem>
    GroupItemsByDeclaration(
      IEnumerable<TypeTranslationItem> items
    )
  {
    return items
      .GroupBy(
        item => (
          Type: item.DeclaringType,
          IsPlural: item.Plural,
          Property: item.EnumName ?? item.Property?.Name
        ))
      .Select(
        group =>
        {
          var type = group.Key.Type;
          var property = group.Key.Property;

          if (property is null)
          {
            var prompt = group.Key.IsPlural
              ? AdditionalPluralTypePrompt
              : AdditionalTypePrompt;
            var first = group.First();
            var typeMetadata = $"""
            Type '{type.FullName}'
          """.Trim();
            return new GroupedByDeclarationTranslationItem(
              type,
              null,
              first.Key,
              first.ShortKey,
              [],
              typeMetadata,
              prompt,
              group.Key.IsPlural
            );
          }

          var typeKey = translationQueries.Key(type);
          var declaredItem = group.FirstOrDefault(
            x =>
              x.Key.StartsWith(typeKey));
          if (declaredItem is null)
          {
            var keys = string.Join(
              "\n",
              group.Select(x => x.Key));
            throw new InvalidOperationException(
              $"Could not find declared item for '{typeKey}.{property}'"
              + $" out of:\n{keys}");
          }

          var additional = group
            .Where(x => x != declaredItem)
            .ToList();
          var memberMetadata = additional.Count == 0
            ? $"Property '{property}' of type '{typeKey}'"
            : $"""
              Property '{property}' of type '{typeKey}' with overrides:
            {string
              .Join("\n", additional.Select(x => x.ShortKey))
              .Indent(2, "\n")}
            """.Trim().Dedent(12, "\n");
          return new GroupedByDeclarationTranslationItem(
            type,
            property,
            declaredItem.Key,
            declaredItem.ShortKey,
            additional
              .Select(x => (x.Key, x.ShortKey))
              .ToList(),
            memberMetadata,
            AdditionalPropertyPrompt,
            false
          );
        });
  }

  private IEnumerable<TypeTranslationItem> GetItems()
  {
    var assemblies = AppDomain.CurrentDomain
      .GetAssemblies()
      .Where(
        assembly =>
          arguments.InputAssemblies.Contains(assembly.GetName().Name));

    logger.LogInformation(
      "Found assemblies:\n{Assemblies}",
      string.Join("\n", assemblies)
    );

    var types = assemblies
      .SelectMany(
        assembly => assembly
          .GetTypes()
          .Where(type => !(type.Name?.StartsWith('<') ?? false))
          .Where(type => !(type.Name?.EndsWith("Extensions") ?? false))
          .Where(
            type =>
              type.Namespace is not null
              && arguments.InputNamespaces.Any(
                type.Namespace.StartsWith)));

    foreach (var type in types)
    {
      var prefix = translationQueries.Key(type);
      var shortPrefix = translationQueries.ShortKey(type);

      yield return new TypeTranslationItem(
        type,
        type,
        null,
        null,
        prefix,
        shortPrefix,
        false
      );

      foreach (var item in GetTypeItems(type, prefix, shortPrefix))
      {
        yield return item;
      }

      var pluralPrefix = translationQueries.Key(type, true);
      var pluralShortPrefix = translationQueries.ShortKey(type, true);

      yield return new TypeTranslationItem(
        type,
        type,
        null,
        null,
        pluralPrefix,
        pluralShortPrefix,
        true
      );

      foreach (var item in GetTypeItems(type, pluralPrefix, pluralShortPrefix))
      {
        yield return item;
      }
    }
  }

  private IEnumerable<TypeTranslationItem> GetTypeItems(
    Type type,
    string pathPrefix,
    string shortPathPrefix
  )
  {
    if (type.IsEnum)
    {
      foreach (var name in type.GetEnumNames())
      {
        var fullPath = $"{pathPrefix}.{name}";
        var fullShortPath = $"{shortPathPrefix}.{name}";

        yield return new TypeTranslationItem(
          type,
          type,
          null,
          name,
          fullPath,
          fullShortPath,
          false
        );
      }
    }

    var properties = type.GetProperties(
      BindingFlags.Public | BindingFlags.Instance
    );

    foreach (var property in properties)
    {
      var propertyType = property.PropertyType;
      var fullPath = $"{pathPrefix}.{property.Name}";
      var fullShortPath = $"{shortPathPrefix}.{property.Name}";

      var declaringType = GetLogicalDeclaringType(property);
      yield return new TypeTranslationItem(
        declaringType,
        type,
        property,
        null,
        fullPath,
        fullShortPath,
        false
      );

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
          && elementType.Namespace != null
          && arguments.InputNamespaces.Any(
            elementType.Namespace.StartsWith)
        )
        {
          foreach (var item in
            GetTypeItems(
              elementType,
              fullPath,
              fullShortPath))
          {
            yield return item;
          }
        }
      }
      else if (
        propertyType.Namespace != null
        && arguments.InputNamespaces.Any(
          propertyType.Namespace.StartsWith)
      )
      {
        foreach (var item in
          GetTypeItems(
            propertyType,
            fullPath,
            fullShortPath))
        {
          yield return item;
        }
      }
    }
  }

  private Type GetLogicalDeclaringType(PropertyInfo property)
  {
    var declaringType = property.DeclaringType
      ?? throw new InvalidOperationException(
        $"Could not find declaring type for '{property.Name}'");

    if (declaringType.IsInterface)
    {
      return declaringType;
    }

    if (declaringType.IsGenericType)
    {
      declaringType = declaringType.GetGenericTypeDefinition();
    }

    var declaringTypeProperty = declaringType.GetProperty(
      property.Name,
      BindingFlags.Public | BindingFlags.Instance)!;

    var interfaces = GetAllInterfacesInHierarchy(declaringType);

    foreach (var @interface in interfaces)
    {
      var mapping = declaringType.GetInterfaceMap(@interface);

      var getter = declaringTypeProperty.GetGetMethod();
      var setter = declaringTypeProperty.GetSetMethod();

      if ((getter != null && mapping.TargetMethods.Contains(getter)) ||
        (setter != null && mapping.TargetMethods.Contains(setter)))
      {
        return @interface;
      }
    }

    return declaringType;
  }

  private HashSet<Type> GetAllInterfacesInHierarchy(Type type)
  {
    var interfaces = new HashSet<Type>();

    while (type != null)
    {
      foreach (var @interface in GetAllInterfacesRecursively(type))
      {
        interfaces.Add(@interface);
      }

      type = type.BaseType!;

      if (type.Namespace == null
        || !arguments.InputNamespaces.Any(type.Namespace.StartsWith))
      {
        break;
      }
    }

    return interfaces;
  }

  private HashSet<Type> GetAllInterfacesRecursively(Type type)
  {
    var interfaces = new HashSet<Type>();

    foreach (var @interface in type.GetInterfaces())
    {
      if (@interface.Namespace == null
        || !arguments.InputNamespaces.Any(@interface.Namespace.StartsWith))
      {
        continue;
      }

      interfaces.Add(@interface);

      foreach (var nestedInterface in GetAllInterfacesRecursively(@interface))
      {
        interfaces.Add(nestedInterface);
      }
    }

    return interfaces;
  }

  private sealed record GroupedAcrossAssembliesTranslationItem(
    bool IsProperty,
    string Key,
    string ShortKey,
    List<(string Key, string ShortKey)> AdditionalKeys,
    string Metadata,
    string AdditionalPrompt
  );

  private sealed record GroupedByDeclarationTranslationItem(
    Type Type,
    string? Property,
    string Key,
    string ShortKey,
    List<(string Key, string ShortKey)> AdditionalKeys,
    string Metadata,
    string AdditionalPrompt,
    bool Plural
  );

  private sealed record TypeTranslationItem(
    Type DeclaringType,
    Type ReflectedType,
    PropertyInfo? Property,
    string? EnumName,
    string Key,
    string ShortKey,
    bool Plural
  );
}
