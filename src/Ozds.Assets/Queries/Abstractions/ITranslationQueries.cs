using System.Linq.Expressions;

namespace Ozds.Assets.Queries.Abstractions;

public interface ITranslationQueries : IQueries
{
  public string GeneralKey(Type type, bool trimmed = true, bool plural = false);

  public string GeneralKey(Type type, string member);

  public string GeneralKey(MemberExpression member);

  public string Key(Type type, bool plural = false);

  public string Key(Type type, string member);

  public string Key(MemberExpression member);

  public string[] KeyOverrides(Type type, bool plural = false);

  public string[] KeyOverrides(Type type, string member);

  public string[] KeyOverrides(MemberExpression member);

  public string ShortKey(Type type, bool plural = false);

  public string ShortKey(Type type, string member);

  public string ShortKey(MemberExpression member);

  public string[] ShortKeyOverrides(Type type, bool plural = false);

  public string[] ShortKeyOverrides(Type type, string member);

  public string[] ShortKeyOverrides(MemberExpression member);
}
