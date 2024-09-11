using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models;

public class MessengerNotificationModel : ResolvableNotificationModel
{
  [Required]
  public required string MessengerId { get; set; } = default!;

  public static MessengerNotificationModel New()
  {
    return new MessengerNotificationModel
    {
      Id = default!,
      Title = "",
      Summary = "",
      Content = "",
      Timestamp = DateTimeOffset.UtcNow,
      EventId = default!,
      ResolvedById = default!,
      ResolvedOn = default!,
      Topics = new List<TopicModel>(),
      MessengerId = default!
    };
  }
}
