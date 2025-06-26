using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ozds.Assets;
using Ozds.Assets.Entities;
using Ozds.Translation.Arguments;
using Ozds.Translation.Services.Base;
using Ozds.Translation.Workers;

namespace Ozds.Translation.Services;

public partial class RegexService(
  IServiceProvider services,
  OzdsTranslationRegexArguments arguments,
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
    var razorFiles = Directory.GetFiles(
      arguments.InputRazorFolderPath,
      "*.razor",
      SearchOption.AllDirectories
    );

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
        .OfType<Match>();
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

          if (dictionary.Get(key) is { } translation)
          {
            dictionary.Replace(key, metadata, translation);
            logger.LogInformation(
              "Updated key '{Key}' metadata:\n{Metadata}",
              key,
              metadata
            );
            continue;
          }

          logger.LogInformation("Found new key: {Key}", key);
          yield return new TranslationWorkerItem(
            dictionary,
            key,
            metadata,
            key,
            AssetConstants.EnglishCulture,
            new CultureInfo(arguments.Language),
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

  private sealed record Index(int Line, int Column);

  [GeneratedRegex(@"Translate\(""([^""]+)""\)")]
  private static partial Regex TranslateRegex();

  [GeneratedRegex(@"Translate\([^,]+,[^""]+""([^""]+)""\)")]
  private static partial Regex TranslateWithCultureRegex();
}
