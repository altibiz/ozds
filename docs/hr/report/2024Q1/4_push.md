# Push

<div style="display: none;">
  \page izvjesce-2024-q1-push Push
</div>

Mjerenja se šalju na server putem posredničkih uređaja preko jednostavnog REST
API-ja. Krajnja točka se nalazi na `/iot/push/{messengerId}` i prihvaća JSON
poruke s sljedećom shemom:

```json
{
  # Vrijeme kada je zahtjev poslan
  "timestamp": "<ISO 8601 vremenska oznaka>",
  "measurements" : [
    {
      # Identifikator mjerača je niz sa sljedećom shemom:
      # <proizvođač>-<model>-<serija>
      "meterId": "<meterId>",
      # Vrijeme kada je mjerenje uzeto
      "timestamp": "<ISO 8601 vremenska oznaka>",
      "data": {
        <registri ovisno o tipu uređaja> ...
      }
    },
    <više mjerenja> ...
  ]
}
```
