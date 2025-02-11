using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Ozds.Business.Import.Abstractions;

namespace Ozds.Business.Import;

public class CsvParser : ICsvParser
{
  public IEnumerable<T> ParseCsv<T>(Stream csvStream)
    where T : new()
  {
    var results = new List<T>();

    using var reader = new StreamReader(csvStream);
    // Read the header row
    string? headerLine = reader.ReadLine();
    if (string.IsNullOrWhiteSpace(headerLine))
    {
      return results; // or throw an exception if a header is required
    }

    // Split header into column names
    string[] headers = headerLine.Split(',');

    // Process each subsequent line
    while (!reader.EndOfStream)
    {
      string? line = reader.ReadLine();
      if (string.IsNullOrWhiteSpace(line))
      {
        continue;
      }

      string[] values = line.Split(',');

      T obj = new T();
      for (int i = 0; i < headers.Length && i < values.Length; i++)
      {
        string header = headers[i].Trim();
        string value = values[i].Trim();

        // Look for a public instance property that matches the header name (ignoring case)
        PropertyInfo? property = typeof(T).GetProperty(
          header,
          BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase
        );
        if (property != null && property.CanWrite)
        {
          try
          {
            // Convert the CSV string value to the property type
            object? convertedValue = ConvertToType(
              value,
              property.PropertyType
            );
            property.SetValue(obj, convertedValue);
          }
          catch (Exception ex)
          {
            // Log or handle the conversion error as needed
            // For example: Console.WriteLine($"Error converting value '{value}' to {property.PropertyType.Name}: {ex.Message}");
          }
        }
      }
      results.Add(obj);
    }

    return results;
  }

  /// <summary>
  /// Converts a string value to the specified target type.
  /// </summary>
  private object? ConvertToType(string value, Type targetType)
  {
    if (targetType == typeof(string))
    {
      return value;
    }

    if (targetType.IsEnum)
    {
      return Enum.Parse(targetType, value);
    }

    if (targetType == typeof(Guid))
    {
      return Guid.Parse(value);
    }

    if (targetType == typeof(DateTime))
    {
      return DateTime.Parse(value, CultureInfo.InvariantCulture);
    }

    // For other convertible types (e.g. int, double, bool)
    return Convert.ChangeType(value, targetType);
  }
}
