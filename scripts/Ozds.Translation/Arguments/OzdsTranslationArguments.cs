using CommandLine;

namespace Ozds.Translation.Arguments;

public interface IOzdsTranslationArguments
{
}

[Verb("regex", HelpText = "Translate regexes.")]
public class OzdsTranslationRegexArguments : IOzdsTranslationArguments
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

  [Option(
    'r', "remove-unused", Required = false, Default = false,
    HelpText = "Remove unused strings.")]
  public bool RemoveUnused { get; set; } = default!;
}

[Verb("type", HelpText = "Translate types.")]
public class OzdsTranslationTypeArguments : IOzdsTranslationArguments
{
  [Option(
    'l', "language", Required = true,
    HelpText = "The target language in two letter ISO format.")]
  public string Language { get; set; } = default!;

  [Option('a', "assemblies", Required = true, HelpText = "Input assemblies.")]
  public IEnumerable<string> InputAssemblies { get; set; } = default!;

  [Option(
    'n', "namespaces", Required = true,
    HelpText = "Input namespaces.")]
  public IEnumerable<string> InputNamespaces { get; set; } = default!;

  [Option(
    'u', "update", Required = false, HelpText = "Path of file to update.")]
  public string? UpdateFilePath { get; set; } = default!;

  [Option('o', "output", Required = true, HelpText = "Output file path.")]
  public string OutputFilePath { get; set; } = default!;

  [Option(
    'R', "remove-overrides", Required = false, Default = false,
    HelpText = "Remove overrides.")]
  public bool RemoveOverrides { get; set; } = default!;

  [Option(
    'r', "remove-unused", Required = false, Default = false,
    HelpText = "Remove unused types and properties.")]
  public bool RemoveUnused { get; set; } = default!;
}

public static class OzdsTranslationArguments
{
  public static IOzdsTranslationArguments? Parse(string[] args)
  {
    try
    {
      var result = new Parser(with =>
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

      return result.Value as IOzdsTranslationArguments;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(ex.Message);
      Environment.Exit(1);
    }

    return default;
  }
}
