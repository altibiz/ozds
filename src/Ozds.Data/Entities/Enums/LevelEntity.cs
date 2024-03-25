using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Enums;

[PostgresqlEnum]
public enum LevelEntity
{
  /// <summary>
  ///   Use for every query. Only written in development.
  /// </summary>
  Trace,

  /// <summary>
  ///   Use for every mutation. Only written in development.
  /// </summary>
  Debug,

  /// <summary>
  ///   Use for mutations on data directly related to users.
  /// </summary>
  Info,

  /// <summary>
  ///   Use when something fails validation on business layer.
  /// </summary>
  Warning,

  /// <summary>
  ///   Use for things that make the system unusable on the network level like
  ///   meter failures.
  /// </summary>
  Error,

  /// <summary>
  ///   Use for things that make the system unusable on the server level like
  ///   migration failures.
  /// </summary>
  Critical
}
