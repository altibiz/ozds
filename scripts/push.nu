#!/usr/bin/env nu

def main [path: path, batch: int = 10000, url: string = "http://localhost:5000/iot/push/messenger"] {
  let timestamped = dfr open $path | 
    dfr with-column (dfr open $path |
      dfr select Timestamp |
      dfr as-datetime "%Y-%m-%d %H:%M:%S.%9f%z") --name Timestamp
    | dfr filter (dfr col Timestamp | dfr is-not-null)
    | dfr filter (dfr col MeterId | dfr is-not-null)

  mut i = 0

  loop {
    let start = $i * $batch
    let measurements = $timestamped | dfr slice $start $batch | dfr into-nu
    if (($measurements | length) == 0) {
      break
    }

    let request = {
      Timestamp: (date now | date to-timezone utc | format date %+),
      Measurements: ($measurements | 
        each { |x| {
          MeterId: $x.MeterId,
          Timestamp: ($x.Timestamp | date to-timezone utc | format date %+),
          Data: ($x | reject index MeterId Timestamp)
        } })
    }

    loop {
      try {
        let $response = http post --content-type application/json $url $request
        if ($response | is-empty) {
          print $"Successfully pushed ($measurements | length) measurements at ($start)"
        } else {
          print $"Successfully pushed ($measurements | length) measurements at ($start), and got response:\n($response)"
        }
        break
      } catch {
        print $"Failed pushing ($batch) rows at ($start)"
        sleep 1sec
      }
    }

    $i += 1
  }
}
