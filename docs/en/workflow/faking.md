# Faking

Faking measurements is done in two modes: pushing and seeding. In push mode,
measurements are faked continuously and sent to the server. In seed mode,
measurements are faked for a specified interval (e.g. last week) and sent to the
server in large batches as fast as possible.

## Pushing

Pushing is a continuous process that sends measurements to the server every
specified interval (e.g. last minute) for measurements in that interval. The
requested interval is first projected into the time of the CSV file and then the
records are projected into the future.

```plantuml
start

repeat
  :Read specified interval
  projected into the time of
  CSV file;

  :Project records into the future;

  :Convert records to push request;

  :Send push request to server;

  :Wait for specified interval;
repeat while ()

stop
```

## Seeding

Seeding is a one-time operation that sends measurements to the server for a
specified interval (e.g. last week) in large batches as fast as possible. This
is accomplished the same way as pushing, but the requests are sent once without
waiting for the next interval.

```plantuml
start

:Start of current interval is determined;

repeat
  :CSV records for a day
  from the start of the current interval
  projected into the time of the CSV file are read;

  :Project records into the future;

  :Convert records into push request measurements;

  repeat :Take a specified batch of measurements;
    :Remove batch from the list of measurements;

    :Pack request measurements into a request;

    :Send request to server;
  backward :Process next batch of measurements;
  repeat while (More measurements?);
backward :Start of current interval is moved to the next day;
repeat while (Start of current interval < now?)

stop
```
