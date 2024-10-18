using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Client.State;

public record RepresentativeState(
  RepresentativeModel Representative,
  List<AnalysisBasisModel> AnalysisBases,
  List<NotificationModel> Notifications,
  Action<NotificationModel> MarkNotificationAsSeen,
  Action<NotificationModel> AddNotification
);
