namespace Ozds.Jobs.Observers.EventArgs;

public class ArchivalJobEventArgs : System.EventArgs
{
  public required DateTimeOffset ScheduledAt { get; init; }

  public required DateTimeOffset StartedAt { get; init; }

  public required DateTimeOffset ScheduledFireAt { get; init; }

  public required DateTimeOffset? FiredAt { get; init; }

  public required int RefireCount { get; init; }
}
