# Push

<div style="display: none;">
  \page report-2024-q1-push Push
</div>

Measurements are pushed to the server by messenger devices via a simple REST
API. The endpoint is on `/iot/push/{messengerId}` and accepts JSON payloads with
the following schema:

```json
{
  # Time when request was sent
  "timestamp": "<ISO 8601 timestamp>",
  "measurements" : [
    {
      # Meter id is a string with the following schema:
      # <manufacturer>-<model>-<serial>
      "meterId": "<meterId>",
      # Time when measurement was taken
      "timestamp": "<ISO 8601 timestamp>",
      "data": {
        <registers depending on type of device> ...
      }
    },
    <more measurements> ...
  ]
}
```
