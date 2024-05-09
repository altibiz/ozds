# Auditing

Auditing is the process of tracking changes to auditable data such as locations
and network users via audit events. Audit events are created by the server each
time auditable data is created, updated, or deleted. These events are stored in
the database and can be queried to determine who made the change, when the
change was made, and what the change was.

Auditing is implemented via a query interceptor that creates an audit event each
time auditable data is created, updated, or deleted.

```plantuml
start
:Database is queried;

:Query interceptor starts reading changed entities;

repeat :Entity processing starts;
  if (Entity is auditable and mutated?) then (yes)
    :Modify audit properties on entity;
    :Create audit event;
    :Add audit event creation to query;
  else (no)
  endif
backward :Process next entity;
repeat while (More entities?);

:Query is executed;

stop
```
