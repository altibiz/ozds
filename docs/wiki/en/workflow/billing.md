# Billing

Billing is the process of issuing invoices to network users and locations. Only
network user billing is currently implemented. Currently, operators can issue
invoices on demand for the last billing period (last month).

Billing is implemented via a set of classes that calculate different parts of
the invoice depending on measurement locations and tariffs.

```plantuml
@startuml

start

:Operator representative clicks on invoice issuing button for network user;

:Database is queried for an invoice issuing basis for the network user;

:Network user invoice calculator starts reading calculation
bases corresponding to each of their measurement locations;

repeat :Calculation basis processing starts;
  :Pick calculator depending on tariff:
    - white medium,
    - white low,
    - red low,
    - blue low;

  :Calculator starts reading calculation item bases;

  repeat :Calculation item basis processing starts;
    :Pick calculator depending on type of calculation item:
      - active energy T0/T1/T2,
      - reactive energy,
      - active power peak,
      - meter fee,
      - business usage fee,
      - renewable energy fee;

    :Calculate item values;
  backward :Process next calculation item basis;
  repeat while (More calculation item bases?);

  :Calculate total cost of calculation;
backward :Process next calculation basis;
repeat while (More calculation bases?);

:Calculate totals of calculation items spanning across all calculations;

:Calculate total, tax, and total with tax;

:Save invoice to database and get created invoice id;

:Start reading calculated calculations;

repeat :Calculation is read;
  :Link calculation to invoice;

  :Track calculation as created;
backward :Process next calculation;
repeat while (More calculations?);

:Save calculations to database;

stop

@enduml
```
