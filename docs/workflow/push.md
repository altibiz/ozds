# Push

Messengers push measurements from meters via the `push` API to the server.

The measurement insertion process is rather simple as it only requires JSON
deserialization and mapping from measurement push models to measurement database
entities before the insertion can happen.

The complicated part is creating aggregates over the measurements. We are aware
that `TimescaleDB` has a continuous aggregate feature that can be used to do
this automatically in the database, however, this requires a community
`TimescaleDB` license and this is not available in the Azure cloud. Therefore,
we have implemented a query interceptor that first converts measurements to
aggregates, merges them on the server and then upserts them in the database. The
merge process is required because aggregates have a composite primary key
consisting of their timestamp, meter id, and interval which means that, without
merging on the server, multiple instances of the same aggregate may be present
in one query which is not allowed.

There is one major drawback to our approach. Since `Entity Framework Core`
doesn't yet support upserts we had to use a separate library for that. The
library we use for database upserts does not support upserting in the same query
as measurement insertion. This means that every measurement push results in two
queries to the database. One for inserting measurements and one for upserting
aggregates. This is not ideal but it is the best we could do with the current
state of the libraries we use.

```plantuml
start

:Aggregate interceptor collects
all added measurement entities
from the query;

:Measurement entities are converted
to measurement models;

:Each measurement model is converted
into three aggregates each corresponding
to one supported interval:
  - quarter hourly
  - daily
  - monthly;

:Aggregates are grouped by their
composite primary key properties:
  - timestamp
  - meter id
  - interval;

:Grouped aggregates are merged:
  - minimum measures are taken from
    the aggregate with the minimum value in the group
  - maximum measures are taken from
    the aggregate with the maximum value in the group
  - average measures are taken from
    the average of all measures in the group;

:Aggregates are converted to aggregate entities;

:Aggregates are upserted in the database;

stop
```
