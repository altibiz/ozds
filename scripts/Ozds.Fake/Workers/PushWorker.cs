using Ozds.Business.Conversion;
using Ozds.Business.Queries;
using Ozds.Fake.Client;
using Ozds.Fake.Conversion;
using Ozds.Fake.Extensions;
using Ozds.Fake.Generation;
using Ozds.Fake.Identification;
using Ozds.Fake.Packing;
using Ozds.Fake.Workers.Abstractions;

namespace Ozds.Fake.Workers;

public record PushWorkerItem(
  DateTimeOffset DateFrom,
  DateTimeOffset DateTo,
  string MessengerId,
  List<MeasurementLocationMeterId> Ids,
  int BatchSize,
  PushClientBufferBehavior BufferBehavior
);

public class PushWorker(
  MeasurementRecordGenerator generator,
  MeasurementRecordConverter recordConverter,
  PushRequestMeasurementConverter pushRequestConverter,
  MessengerPushRequestPacker packer,
  PushClient client,
  ClockQueries clock
) : IEnumeratedBackgroundServiceWorker<PushWorkerItem>
{
  public async Task ExecuteAsync(
    PushWorkerItem item,
    CancellationToken stoppingToken
  )
  {
    var records = generator.BatchGenerateMeasurementRecords(
      item.DateFrom,
      item.DateTo,
      item.Ids,
      stoppingToken);

    var models = recordConverter.ConvertToModels(
      records,
      stoppingToken
    );

    var requests = pushRequestConverter.ToPushRequests(
      models,
      stoppingToken
    );

    await foreach (var batch in requests
      .Batch(item.BatchSize, stoppingToken))
    {
      var request = await packer.Pack(
        item.MessengerId,
        clock.Timestamp(),
        batch,
        stoppingToken
      );

      await client.Push(
        item.MessengerId,
        item.BufferBehavior,
        request,
        stoppingToken
      );
    }
  }
}
