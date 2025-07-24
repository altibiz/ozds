using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Fake.Identification;
using Ozds.Server.Test.Base;
using Ozds.Server.Test.Extensions;

namespace Ozds.Server.Test.Reactors;

public class JobsMessengerInactivityJobReactorTest : OzdsServerTestBase
{
  [Test]
  public async Task MessengerInactivityJobReactor_Reacts(
    CancellationToken cancellationToken
  )
  {
    var inactivityPeriod = new PeriodModel
    {
      Duration = DurationModel.Minute,
      Multiplier = 1
    };

    var inactivityTimeSpan = Services.GetRequiredService<TimeQueries>()
      .PeriodTimeSpan(inactivityPeriod);

    var x = await MeasurementLocation.Create(
      cancellationToken, x => x
        .WithNetworkUser(
          y => y
            .WithLocation(
              z => z
                .WithMessenger(
                  m =>
                    m.MaxInactivityPeriod = inactivityPeriod))));

    {
      using var cts = inactivityTimeSpan
        .CancelIn(cancellationToken);
      var anyPushed = await Measurement
        .Push(
          x.Messenger.Id,
          x.Messenger.Title, // NOTE: API key when it gets implemented
          [
            new MeasurementLocationMeterIdWithValidator(
              x.MeasurementLocation.Id,
              x.Meter.Id,
              x.MeasurementValidator)
          ],
          inactivityTimeSpan / 2,
          cts.Token)
        .AnyAsync(cts.Token);
      anyPushed.Should().BeTrue();
    }

    await Task.Delay(inactivityTimeSpan * 2, cancellationToken);

    var modelQueries = Services.GetRequiredService<ModelQueries>();
    var notifications = await modelQueries
      .Read<MessengerNotificationModel>(0, cancellationToken);
    notifications.Items.Should().HaveCount(1);
    notifications.TotalCount.Should().Be(1);
    var notification = notifications.Items.First();
    notification.MessengerId.Should().Be(x.Messenger.Id);
    notification.Topics.Should().BeSubsetOf(
      [TopicModel.All, TopicModel.Messenger, TopicModel.MessengerInactivity]);
  }
}
