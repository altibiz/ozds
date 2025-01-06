namespace Ozds.Jobs.Observers.EventArgs;

public class MessengerJobEventArgs : System.EventArgs
{
  public required string Id { get; init; }

  public required DateTimeOffset ScheduledAt { get; init; }

  public required DateTimeOffset StartedAt { get; init; }

  public required DateTimeOffset ScheduledFireAt { get; init; }

  public required DateTimeOffset? FiredAt { get; init; }

  public required int RefireCount { get; init; }
}
