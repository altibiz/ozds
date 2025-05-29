# OZDS Architecture

## 1. Overview

OZDS is designed as a **modular monolith** with a long-term vision of evolving
into a more distributed system, particularly for high-load components like data
ingestion. The architecture prioritizes clear separation of concerns between its
modules, with `Ozds.Business` serving as the core domain logic.

The system handles the ingestion, processing, aggregation, and management of
electricity meter data, integrating with an external ERP system for processes
like invoicing.

## 2. High-Level Architectural Pattern

- **Current:** Modular Monolith.
  - **Reasoning:** Allows for easier initial development and deployment, with
    clear boundaries for future separation. Helps manage performance for
    different aspects of the application.
- **Future Aspiration:** Hybrid approach, with specific services (e.g., Ingest
  Service) separated for scalability and independent deployment.

## 3. Key Modules & Responsibilities

The solution is structured into several .NET projects:

- **`Ozds.Business`**: Core domain logic, business rules, and central
  orchestration. Acts as a facade for its dependencies and is a dependency for
  UI/API layers.
- **`Ozds.Data`**: Data access layer, currently using PostgreSQL with the
  TimescaleDB extension.
  - _Future Plan:_ Split into `Ozds.Relational` (for standard relational data,
    potentially migrating to a distributed SQL DB like CockroachDB) and
    `Ozds.Timeseries` (for time-series data, exploring alternatives to
    TimescaleDB with better licensing and native distributed capabilities).
- **`Ozds.Client`**: Frontend Blazor application, depends on `Ozds.Business`.
- **`Ozds.Server`**: The main startup project, hosting the ASP.NET Core
  application and depending on most other relevant projects.
- **`Ozds.Iot`**: Handles interactions with IoT devices (Raspberry Pis),
  including data models for meter readings.
- **`Ozds.Messaging`**: Manages asynchronous communication and saga
  orchestration, particularly for ERP integration using Azure Service Bus.
- **`Ozds.Document`**: Handles document generation like invoice PDF/HTML.
- **`Ozds.Report`**: Handles report generation and ingest like measurement
  summmary export or bulk ingest of database entities via CSV files.
- **`Ozds.Jobs`**: Dynamic background job scheduling via Quartz.NET.
- **`Ozds.Users`**: Queries/Mutations on auth data. For now a simple OrchardCore
  UserService facade.
  - _Future Plan:_ Ditch OrchardCore and switch to something like Auth0 for
    authentication.
- **`Ozds.Email`**: Email sending functionalities.

**Dependency Flow:** A loose adherence to a 12-factor app approach is followed.
`Ozds.Business` is central, with UI/API layers (`Ozds.Client`, `Ozds.Server`)
depending on it. `Ozds.Server` as the startup project has the widest set of
dependencies.

## 4. Data Ingestion Pipeline (IoT & Pidgeon)

1.  **Data Source:** Raspberry Pi (RPi) devices ("Pidgeons") running a Rust
    program and a local PostgreSQL instance as a buffer.
    - RPis collect data from electricity meters via Modbus.
    - NixOS is used for RPi configuration.
    - Data integrity is ensured through retries from the RPi to the server.
2.  **Data Transmission:** RPis send measurement data to the OZDS server
    (currently HTTP, planned migration to WebSocket + Protobuf for efficiency).
3.  **Server-Side Ingestion:**
    - Incoming JSON data is deserialized.
    - An event is published using an internal C# channel-based pub/sub system.
    - A **Reactor** (automatic subscription class in DI) picks up the event.
    - **Model Creation:** Entities are converted to models. For each
      measurement, three aggregate models are also created (quarter-hourly,
      daily, monthly).
    - **Measurement Location Mapping:** Physical meter IDs from RPis are mapped
      to logical measurement location IDs within OZDS.
    - **Buffering (`Ozds.Business/Buffers`):**
      - **Measurements:** Buffered in a `ConcurrentQueue` (up to 10,000,
        inspired by ELK limits for JSON bulk inserts).
      - **Aggregates:** Buffered and flushed in a `ConcurrentQueue` roughly
        every 15 minutes. The flush is triggered when an aggregation interval
        (e.g., a specific 15-minute window) is "closed" – meaning no more
        measurements are coming in from it. This also triggers flushing for
        corresponding daily and monthly aggregates.
      - The buffer can be bypassed via request headers for single-transaction
        processing if needed.
    - **Database Insertion:**
      - When buffers are flushed, another event is published.
      - A reactor picks this up and calls database insertion functions (raw SQL
        embedded in migrations) to persist measurements and aggregates.
      - Postgres's JSON capabilities are used for insertion to avoid column
        order issues.

**Overall Data Strategy:** "Heavy writes, light reads." Complex aggregation and
processing happen during ingestion to make querying faster and simpler.

## 5. Database Architecture

- **Current:** PostgreSQL with TimescaleDB extension.
  - TimescaleDB's continuous aggregates are **not** used due to licensing
    restrictions in Azure (manual real-time aggregation is implemented instead).
- **Future Vision:**
  - **Relational Data:** Continue with PostgreSQL, potentially migrating to a
    distributed SQL database like CockroachDB for scalability.
  - **Time-Series Data:** Explore alternatives to TimescaleDB, seeking:
    - Better licensing terms.
    - Strong horizontal scalability and sharding capabilities designed
      specifically for time-series workloads.

## 6. Asynchronous Processing & System Integration

### 6.1. Internal Pub/Sub

- **Current:** Implemented using C# `System.Threading.Channels` and custom
  wrapper classes forming a pub/sub system. Reactors subscribe to these channels
  to perform actions upon data changes (e.g., `NetworkUserInvoiceChangeReactor`
  sending an email).
  - **Pattern:** A form of template strategy pattern is used for reactor base
    logic.
  - **Known Issues:** This introduces statefulness to the application (memory
    usage by channels, graceful shutdown complexities, error handling). Limited
    observability (basic C# logging).
- **Future Plan:** Migrate to a more robust, potentially distributed, internal
  bus library with a PostgreSQL backend.
  - **Goals:** Achieve at-least-once or exactly-once delivery semantics. Improve
    observability and manageability. Abstract the bus implementation so reactors
    can be easily migrated.

### 6.2. ERP Integration & Sagas

- **Mechanism:** Azure Service Bus for communication with the external ERP
  system.
- **Pattern:** Sagas are used to manage long-running processes and maintain
  state consistency between OZDS and the ERP (e.g., for invoice
  synchronization).
  - **Tooling:** Currently using MassTransit.
    - **Issue:** MassTransit has moved to a commercial license. A migration to
      an alternative (or a self-implemented solution) is planned in the medium
      term (approx. 1 year). NServiceBus is not an option due to similar
      licensing concerns.
  - **Saga Error Handling (Example: Invoice Sync):** Abort the invoice process
    and send an email notification to the appropriate operator/user.
  - **Saga Testing:**
    - Currently manual end-to-end testing.
    - ERP is faked on the OZDS side during development.
    - _Future Idea:_ Create a separate shared repository to define saga
      interfaces/contracts for both OZDS and ERP to improve collaboration and
      reduce integration errors. NuGet + semantic versioning would be used.

## 7. Scalability & Performance

- **Current:** Primarily vertical scaling due to stateful components (especially
  the ingest service's channel-based pub/sub).
- **Future Plans for Horizontal Scaling (primarily for Ingest):**
  1.  **Stateless Ingest Service:**
      - Decouple data ingestion from the main monolith.
      - Utilize PostgreSQL listeners on the monolith side to react to ingested
        data, replacing some current channel-based reactor functionalities.
      - RPi communication via WebSocket + Protobuf.
  2.  **Distributed Database:** Migrate time-series data to a horizontally
      scalable database (see Database Architecture).
  3.  **Service Bus Routing:** If a DIY service bus is implemented, message
      routing capabilities might be needed for horizontally scaled OZDS
      components. (Specific routing strategies are TBD).
- **Burst Performance:** Other parts of the system (e.g., user-facing actions,
  scheduled jobs) are considered to require burst performance rather than
  sustained high throughput, making the current monolith structure acceptable
  for them.

## 8. Deployment & Operations (Raspberry Pi - "Pidgeons")

- **Deployment:** NixOS configurations deployed using `deploy-rs` over a Nebula
  VPN.
- **Auto-Updates (Future):** Plan to use `comin` for auto-updates. Rollback
  mechanisms are a key consideration.
- **Connectivity:** Currently relies on available Wi-Fi.
  - _Future Plan:_ Implement 4G connectivity for RPis for more reliable
    communication and to avoid dependency on external Wi-Fi.
- **Monitoring:**
  - **Current:** Timer-based email alerts if an RPi stops sending measurements.
  - **Future (GitHub Issue Tracked):** Comprehensive RPi health monitoring:
    - Battery state (critical for inferring power status in the distribution
      system, aiding billing and complaint handling).
    - SSD health and free space.
    - CPU/SSD temperature.
    - Average CPU/RAM usage.
    - Modbus communication error rates.
    - Local Postgres buffer queue length.

## 9. Security Considerations

- **RPi Communication:** Nebula VPN provides a secure overlay network.
- (Further details on application-level security, authentication, authorization
  are TBD in this document).

## 10. Unwritten Rules & Conventions

- Directory layout conventions for projects.
- Pragmatic approach: "It works, but I know it can be better" for several
  components, with clear ideas for future improvements.

## 11. Open Questions & Areas for Future Definition

- Detailed error handling and retry strategies for all critical communication
  paths.
- Specifics of the "elegant condition" for aggregate buffer flushing beyond "no
  more aggregates being processed for that interval."
- Choice of library/approach for the Postgres-backed internal pub/sub system.
- Versioning strategy for shared saga interfaces if the ERP makes breaking
  changes (currently manual coordination).
- Detailed design for how RPi battery status (via 4G) will integrate into
  billing/support workflows.
- Advanced routing strategies for a potential future DIY service bus.
