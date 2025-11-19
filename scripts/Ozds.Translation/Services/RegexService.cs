using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ozds.Assets.Entities;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Translation.Arguments;
using Ozds.Translation.Services.Base;
using Ozds.Translation.Workers;

namespace Ozds.Translation.Services;

public partial class RegexService(
  IServiceProvider services,
  OzdsTranslationRegexArguments arguments,
  ICultureQueries cultureQueries,
  ILogger<RegexService> logger
) : AsyncEnumeratedService<TranslationWorkerItem, TranslationWorker>(
  services
)
{
  private static readonly string? AdditionalPrompt = null;

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

  protected override async IAsyncEnumerable<TranslationWorkerItem>
    GetEnumerable(
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
  {
    var items = await GroupTranslationWorkerItems(
        GetTranslationWorkerItems(cancellationToken))
      .ToListAsync(cancellationToken);

    if (arguments.RemoveUnused)
    {
      var managedItems = dictionary
        .ToList()
        .Where(
          item => item.Metadata is { } metadata
            && metadata.StartsWith("From file"))
        .ToList();

      // TODO: better way to detect managed translations
      var unusedManagedItems = managedItems
        .Where(
          dictionaryItem => !items
            .Exists(item => item.Key == dictionaryItem.Key))
        .ToList();

      foreach (var key in unusedManagedItems.Select(x => x.Key))
      {
        dictionary.Remove(key);
        logger.LogInformation("Removed key '{Key}'", key);
      }
    }

    foreach (var item in items)
    {
      if (dictionary.Get(item.Key) is { } translation)
      {
        dictionary.AddOrUpdate(item.Key, item.Metadata, translation);
        logger.LogInformation(
          "Updated key '{Key}' metadata:\n{Metadata}",
          item.Key,
          item.Metadata
        );
        continue;
      }

      logger.LogInformation("Found new key: {Key}", item.Key);
      yield return item;
    }
  }

  private static async IAsyncEnumerable<TranslationWorkerItem>
    GroupTranslationWorkerItems(
      IAsyncEnumerable<TranslationWorkerItem> items
    )
  {
    await foreach (var item in items
      .GroupBy(item => item.Key)
      .SelectAwait(
        async group =>
        {
          var first = await group.FirstAsync();

          var metadata = await group
            .Select(item => item.Metadata)
            .AggregateAsync((x, y) => $"{x}\n{y}");

          return first with
          {
            Metadata = metadata
          };
        }))
    {
      yield return item;
    }
  }

  private async IAsyncEnumerable<TranslationWorkerItem>
    GetTranslationWorkerItems(
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
  {
    var culture = cultureQueries.IdToCulture(arguments.Language);
    if (culture is null)
    {
      throw new InvalidOperationException(
        $"Could not find culture '{arguments.Language}'");
    }

    var razorFiles = Directory
      .GetFiles(
        arguments.InputRazorFolderPath,
        "*.razor",
        SearchOption.AllDirectories)
      .OrderBy(file => file);

    var translateRegex = TranslateRegex();
    var translateWithCultureRegex = TranslateWithCultureRegex();

    foreach (var file in razorFiles)
    {
      var content = await File.ReadAllTextAsync(file, cancellationToken);
      var translateMatches = translateRegex.Matches(content);
      var translateWithCultureMatches = translateWithCultureRegex
        .Matches(content);
      var matches = translateMatches
        .Concat(translateWithCultureMatches)
        .OfType<Match>()
        .OrderBy(match => match.Groups[1].Value);
      foreach (var match in matches)
      {
        if (match.Success)
        {
          var key = match.Groups[1].Value;

          var index = IndexOf(content, match.Index);
          var relativePath = Path.GetRelativePath(
            arguments.InputRazorFolderPath,
            file
          );
          var metadata = $"""
            From file '{relativePath}' line {index.Line} column {index.Column}
          """.Trim();

          yield return new TranslationWorkerItem(
            dictionary,
            key,
            metadata,
            key,
            cultureQueries.EnglishCulture,
            culture,
            AdditionalPrompt
          );
        }
      }
    }
  }

  private static Index IndexOf(string text, int index)
  {
    var line = 1;
    var column = 1;
    for (var i = 0; i < index; i++)
    {
      if (text[i] == '\n')
      {
        line++;
        column = 1;
      }
      else
      {
        column++;
      }
    }

    return new Index(line, column);
  }

  [GeneratedRegex(@"Translate\(""([^""]+)""\)")]
  private static partial Regex TranslateRegex();

  [GeneratedRegex(@"Translate\([^,]+,[^""]+""([^""]+)""\)")]
  private static partial Regex TranslateWithCultureRegex();

  private sealed record Index(int Line, int Column);
}
