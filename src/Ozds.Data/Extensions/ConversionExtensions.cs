namespace Ozds.Data.Extensions;

public static class ConversionExtensions
{
  public static object? ChangeKeyType(
    string id,
    Type keyType
  )
  {
    if (keyType == typeof(string))
    {
      return id;
    }

    if (keyType == typeof(Guid))
    {
      return Guid.Parse(id);
    }

    if (keyType == typeof(long))
    {
      return long.Parse(id);
    }

    throw new InvalidOperationException("Unsupported key type.");
  }
}
