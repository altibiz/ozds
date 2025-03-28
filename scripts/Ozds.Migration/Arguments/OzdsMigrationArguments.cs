using CommandLine;

namespace Ozds.Migration.Arguments;

[Verb("migrate", HelpText = "Migrate the database.")]
public class OzdsMigrationMigrateArguments
{
  [Option('t', "timeout", Required = false, HelpText = "Timeout in seconds.")]
  public int Timeout_s { get; set; } = 3 * 60 * 60;
}

[Verb("generate", HelpText = "Generate additional migration sql.")]
public class OzdsMigrationGenerateArguments
{
  [Option('o', "output", Required = true, HelpText = "Output file.")]
  public string Output { get; set; } = default!;

  [Option('n', "name", Required = false, HelpText = "Name of the migration.")]
  public string Name { get; set; } = "MigrationName";

  [Option('f', "formatter", Required = false, HelpText = "Path to formatter.")]
  public string? Formatter { get; set; }
}

public static class OzdsMigrationArguments
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
        OzdsMigrationMigrateArguments,
        OzdsMigrationGenerateArguments>(args);

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
