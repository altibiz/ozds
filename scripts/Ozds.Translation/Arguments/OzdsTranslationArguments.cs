using CommandLine;

namespace Ozds.Translation.Arguments;

[Verb("regex", HelpText = "Translate regexes.")]
public class OzdsTranslationRegexArguments
{
  [Option(
    'l', "language", Required = true,
    HelpText = "The target language in two letter ISO format.")]
  public string Language { get; set; } = default!;

  [Option('i', "input", Required = true, HelpText = "Input razor folder path.")]
  public string InputRazorFolderPath { get; set; } = default!;

  [Option(
    'u', "update", Required = false, HelpText = "Path of file to update.")]
  public string? UpdateFilePath { get; set; } = default!;

  [Option('o', "output", Required = true, HelpText = "Output file path.")]
  public string OutputFilePath { get; set; } = default!;
}

[Verb("type", HelpText = "Translate types.")]
public class OzdsTranslationTypeArguments
{
  [Option(
    'l', "language", Required = true,
    HelpText = "The target language in two letter ISO format.")]
  public string Language { get; set; } = default!;

  [Option('i', "input", Required = true, HelpText = "Input assembly name.")]
  public string InputAssemblyName { get; set; } = default!;

  [Option(
    'n', "namespaces", Required = true,
    HelpText = "Input assembly namespaces.")]
  public IEnumerable<string> InputAssemblyNamespaces { get; set; } = default!;

  [Option(
    'u', "update", Required = false, HelpText = "Path of file to update.")]
  public string? UpdateFilePath { get; set; } = default!;

  [Option('o', "output", Required = true, HelpText = "Output file path.")]
  public string OutputFilePath { get; set; } = default!;
}

public static class OzdsTranslationArguments
{
  public static object? Parse(string[] args)
  {
    try
    {
      var result = new Parser(
        with =>
        {
          with.CaseInsensitiveEnumValues = true;
          with.AutoHelp = true;
          with.AutoVersion = true;
          with.HelpWriter = Console.Out;
        }).ParseArguments<
        OzdsTranslationRegexArguments,
        OzdsTranslationTypeArguments>(args);

      if (result.Tag == ParserResultType.NotParsed)
      {
        if (result.Errors.Any(e => !e.StopsProcessing))
        {
          foreach (var error in result.Errors)
          {
            Console.Error.WriteLine(error);
          }

          Environment.Exit(1);
        }
        else
        {
          Environment.Exit(0);
        }
      }

      return result.Value;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(ex.Message);
      Environment.Exit(1);
    }

    return default;
  }
}
