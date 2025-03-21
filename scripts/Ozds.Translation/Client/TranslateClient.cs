using System.Globalization;
using System.Text.RegularExpressions;
using OpenAI;
using OpenAI.Chat;
using Ozds.Assets.Extensions;

namespace Ozds.Translation.Client;

public record TranslationResult(
  string Text,
  string? Thinking,
  string Translation,
  CultureInfo FromCulture,
  CultureInfo ToCulture,
  string Prompt
);

public partial class TranslateClient(
  OpenAIClient client
)
{
  private const string Model = "deepseek-chat";

  private static readonly string CommandPromptTemplate =
    @"
      Translate the following from {1} to {2}.
      {1}: {0}
      {2}:
    ".Dedent(6).Trim();

  private static readonly string BasePromptTemplate =
    @"
      You are a professional UI/UX translator. You ONLY provide the
      translations. You DO NOT provide any additional information or
      explanations or further considerations or context. You ONLY translate to
      the target language and not a similar language.

      You follow these guidelines:
      - Keep translations concise and clear - this is UI content that users
        need to understand quickly
      - Use natural language that would make sense to a native speaker
      - Maintain consistent terminology throughout related terms
      - Avoid technical jargon unless it's a standard industry term
      - For technical terms always use the most standard industry translation
        used in the target language
      - For anglicized terms that might have multiple translations, always use
        the most anglicized one
      - For other terms, always use the most common translation in the target
      - Aim for content that would look clean and professional in a form or
        dashboard or other UI elements

      The application that you are translating text for is an electrical
      metering and billing app. Therefore, if certain terms are ambiguous or
      don't make sense in the context of the app, you should translate them in
      the context of energy/finance/utility management.  The application is a
      web application with specific rules for how to translate specific text
      elements.

      Make sure to follow these specific rules:
      - T0 is expanded to 'one tariff'
      - T1 is expanded to 'higher tariff'
      - T1 is expanded to 'lower tariff'
      - Timestamp is always translated as just 'time'
      - Id is always expanded to 'identifier'
      - Units are always kept as-is and enclosed in parentheses (unless in the
        case of percentages)
    ".Dedent(6).Trim();

  private static readonly string TranslatePromptTemplate =
    @$"
      {BasePromptTemplate.Indent(6).Trim()}

      {CommandPromptTemplate.Indent(6).Trim()}
    ".Dedent(6).Trim();

  private static readonly string TranslatePromptTemplateWithAdditionalPrompt =
    @$"
      {BasePromptTemplate.Indent(6).Trim()}

      {{3}}

      {CommandPromptTemplate.Indent(6).Trim()}
    ".Dedent(6).Trim();

  public async Task<TranslationResult> Translate(
    string text,
    CultureInfo fromCulture,
    CultureInfo toCulture,
    CancellationToken cancellationToken,
    string? additionalPrompt = null
  )
  {
    var chatClient = client.GetChatClient(Model);

    string prompt;
    if (additionalPrompt is not null)
    {
      prompt = string.Format(
        CultureInfo.InvariantCulture,
        TranslatePromptTemplateWithAdditionalPrompt,
        text.Trim(),
        fromCulture.DisplayName,
        toCulture.DisplayName,
        additionalPrompt.Trim()
      );
    }
    else
    {
      prompt = string.Format(
        CultureInfo.InvariantCulture,
        TranslatePromptTemplate,
        text.Trim(),
        fromCulture.DisplayName,
        toCulture.DisplayName
      );
    }

    var content = ChatMessageContentPart.CreateTextPart(prompt);

    var message = ChatMessage.CreateUserMessage(content);

    var response = await chatClient.CompleteChatAsync(
      [message],
      cancellationToken: cancellationToken
    );

    var completion = response.Value.Content.Last().Text.Trim();

    var thoughts = ThinkingRegex()
      .Matches(completion);

    var thinking =
      string.Join("\n", thoughts.Select(thought => thought.Groups[1].Value))
        .Trim();

    var translation = thoughts
      .Aggregate(
        completion, (current, thought) => current
          .Replace(thought.Groups[0].Value, string.Empty))
      .Trim();

    return new TranslationResult(
      text,
      thinking,
      translation,
      fromCulture,
      toCulture,
      prompt
    );
  }

  [GeneratedRegex(@"<think>([^<]+)</think>", RegexOptions.Multiline)]
  private partial Regex ThinkingRegex();
}
