using System.Globalization;
using Ozds.Assets.Entities;
using Ozds.Translation.Client;
using Ozds.Translation.Workers.Abstractions;

namespace Ozds.Translation.Workers;

public record TranslationWorkerItem(
  TranslationDictionaryEntity Dictionary,
  string Key,
  string? Metadata,
  string Text,
  CultureInfo FromCulture,
  CultureInfo ToCulture,
  string? AdditionalPrompt = null
);

public class TranslationWorker(
  TranslateClient client,
  ILogger<TranslationWorker> logger
) : IEnumeratedBackgroundServiceWorker<TranslationWorkerItem>
{
  public async Task ExecuteAsync(
    TranslationWorkerItem item,
    CancellationToken stoppingToken
  )
  {
    var result = await client.Translate(
      item.Text,
      item.FromCulture,
      item.ToCulture,
      stoppingToken,
      item.AdditionalPrompt
    );

    if (result.Thinking is not null)
    {
      logger.LogInformation(
        ">>>>>Prompt\n{Prompt}"
        + "\n>>>>>Thinking\n{Thinking}"
        + "\n>>>>>Translation\n{Translation}"
        + "\n>>>>>",
        result.Prompt,
        result.Thinking,
        result.Translation
      );
    }
    else
    {
      logger.LogInformation(
        ">>>>>Prompt\n{Prompt}"
        + "\n>>>>>Translation\n{Translation}"
        + "\n>>>>>",
        result.Prompt,
        result.Translation
      );
    }

    item.Dictionary.Add(item.Key, item.Metadata, result.Translation);
  }
}
