#!/usr/bin/env nu

# FIXME: this has a lot of problems

def "main" [] {}

def "snake to camel" [] {
  $in |
    split row "_" |
    each { |word| $word | str capitalize } |
    str join ""
}

def "keys to camel" [] {
  $in |
    transpose key |
    each { |x| $x | merge { key: ($x.key | snake to camel) } } |
    transpose -r -d
}

def "main export" [path: path, table: string] {
  psql -c $"copy \(select * from ($table)\) to stdout with csv header;" |
    lines |
    par-each { |x|
      $x |
        split row "," |
        each { |y|
          if ($y | str contains "e+") {
            let split = $y | split row "e+";
            ((($split.0) | into float) * (10 ** ($split.1 | into int))) | into int
          } else {
            $y
          }
    } | str join "," } o> $path
}

def "main import" [path: path, batch: int = 10000, url: string = "http://localhost:5000/iot/push/messenger"] {
  let dataframe = polars open $path |
    polars with-column (polars open $path |
      polars select timestamp |
      polars as-datetime "%Y-%m-%d %H:%M:%S.%9f%z") --name timestamp |
    polars filter (polars col timestamp | polars is-not-null) |
    polars filter (polars col meter_id | polars is-not-null) |
    polars cache

  0.. | par-each { |i|
    print $"Processing batch ($i)..."
    let start = $i * $batch
    let measurements = $dataframe |
      polars slice $start $batch |
      polars into-nu
    if (($measurements | length) == 0) {
      print $"Batch ($i) is empty. Stopping..."
      break
    }

    let request = {
      Timestamp: (date now | date to-timezone utc | forjmat date %+),
      Measurements: ($measurements |
        each { |measurement| {
          MeterId: $measurement.meter_id,
          Timestamp: ($measurement.timestamp | date to-timezone utc | format date %+),
          Data: ($measurement | reject meter_id timestamp | keys to camel)
        } })
    }

    print $"Pushing ($measurements | length) measurements at ($start)..."
    try {
      let $response = (http post
        --max-time 600
        --content-type application/json
        $url
        $request)
      if ($response | is-empty) {
        print $"Successfully pushed ($measurements | length) measurements at ($start)"
      } else {
        print $"Successfully pushed ($measurements | length) measurements at ($start), and got response:\n($response)"
      }
    } catch { |e|
      print $"Failed pushing ($batch) rows at ($start) because ($e.msg). Trying one by one..."
      $measurements | each { |measurement|
        let request = {
          Timestamp: (date now | date to-timezone utc | format date %+),
          Measurements: [{
            MeterId: $measurement.meter_id,
            Timestamp: ($measurement.timestamp | date to-timezone utc | format date %+),
            Data: ($measurement | reject meter_id timestamp | keys to camel)
          }]
        }
        try {
          let $response = (http post
            --max-time 600
            --content-type application/json
            $url
            $request)
          if ($response | is-empty) {
            print $"Successfully pushed ($measurement.meter_id) measurement at ($measurement.timestamp)"
          } else {
            print $"Successfully pushed ($measurement.meter_id) measurement at ($measurement.timestamp), and got response:\n($response)"
          }
        } catch { |e|
          print $"Skipping failed push of ($measurement.meter_id) measurement at ($measurement.timestamp) because ($e.msg): '($request | to json)'"
        }
      }
    }
  }
}
