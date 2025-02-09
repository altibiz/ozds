namespace Ozds.Business.Observers.EventArgs;

public class JobsMessengerJobEventArgs : System.EventArgs
{
  public required string Id { get; init; }

  public required DateTimeOffset ScheduledAt { get; init; }

  public required DateTimeOffset StartedAt { get; init; }

  public required DateTimeOffset ScheduledFireAt { get; init; }

  public required DateTimeOffset? FiredAt { get; init; }

  public required int RefireCount { get; init; }
}
