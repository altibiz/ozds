<!-- markdownlint-disable MD024 -->

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

### Added

- measurement deletion job/manager/observer in `Ozds.Jobs`
- measurement deletion reactor in `Ozds.Business`
- needed faking classes for `Ozds.Server.Test` fixtures
- `Ozds.Business` flag to start without reactors
- project flags to start without services
- add correction by validator in record correction
- messenger naming conventions
- max inactivity duration to meters
- meter notification model/entity
- messenger by meter cache
- messenger cache
- meter cache
- meter and meter inactivity topics
- ModelQueries and ModelMutations for generic queries and mutations
- bulk queries and mutations for model and auditable queries and mutations
- meter inactivity job, reactor, manager, observer, relay, field
- original entity/model to entity/model change pub/sub
- max inactivity period to meters and topic enum members migration
- messenger inactivity job reactor test
- meter inactivity job reactor test
- e2e test fixture configuration
- e2e test cancellation extensions
- MeterQueries

### Changed

- `Ozds.Client.Test` to `Ozds.Server.Test` for E2E testing
- model faking to `Ozds.Fake` for now
- cleanup `Ozds.Fake` conversion
- cleanup `Ozds.Fake` namespaces
- cleanup script projects DI
- cleanup some remaining uses of `DateTimeOffset.UtcNow`
- catch `OperationCanceledException` in reactors/relays
- messenger API key in `Ozds.Fake` through arguments rather than config
- extracted AnalysisQueries
- simplified some query names
- add API key header in `Ozds.Fake` push client
- record correction adjustments
- reporting conversion corrections
- measurement validation corrections
- messenger id validation by convention
- enumerable queries in `Ozds.Time` instead of extension methods
- validation fixes and validation over multiple models
- adjustments to frontend for new queries and mutations
- data archival fix for enums
- test adjustments for new query, mutation, measure names
- fix dev mail auth
- fix notification n + 1
- job managers to use job manager base

### Removed

- `Ozds.Client.Test`
- SinglePhasicMeasure in favor of SinglePhasicSumMeasure
- PhasicMeasure.PhaseSingle because it was never used and was confusing
- CalculatedInvoiceMutations in favor of NetworkUserInvoiceMutations
- NetworkUserInvoiceIssuer in favor of NetworkUserInvoiceMutations
- JoinMutations in favor of ModelMutations
- ReadonlyMutations in favor of ModelMutations
- ReadonlyQueries in favor of ModelQueries
- dynamic queries
- uses of DataDbContext in reactors
- AddRecipients in NotificationMutations
- notification recipients on network user invoice messages in favor of reactor
- TestReadonlyFixture as it was never used
- PhaseSingle test

## [1.4.2] - 2025-07-09

### Added

- controller authorization
- meter id validation
- class/style to loading, mutating, paging, table, upload tabs
- upload tabs translation
- upload tabs dialog
- upload tabs title
- add none text to fields on null/empty on model components
- field section and field chapter
- calculation document render
- calculation document page
- chart height parameter
- tariff model field to calculation details
- plural translations

### Changed

- validation fixes
- improved mutating translations
- theme state provider corrections
- layout styling fixes
- all pages styling
- invoice details styling
- calculation details styling
- period translation fix for zero multiplier
- analysis cleanup
- network user dashboard chart fix
- disable reacting to network user interface changes for now
- translation updates/fixups
- monthly analysis date column
- aggregate phasic measure fix

### Removed

- console log from mutating
- amount from peak power calculation item detail

## [1.4.1]

### Added

- styling for notification error text
- `NetworkUserAnalysisDetails` fields
- `launch.json` for translation
- `--remove-unused` for translation

### Changed

- round for numbers given on chart to 2 decimals
- number field in columns so that i can take a number of decimal places
- catalogue decimal place display
- styling around button on entity pages so they display better
- align `Ozds.Client` drawers
- cleaner account page
- set `DetailsField`, `CollapseField` and `TableCollapse` max width
- auditable details change history in collapse
- `PeriodDetails` and similar `MessengerDetails` styling
- `PeriodModel` and `DurationModel` functions in `LocalizationQueries` and
  `TimeQueries`
- push event title fix
- hide search boxes when small amounts of data are shown
- `Ozds.Report` import fix
- translation corrections
- modified translation keys
- minor translation key fixes

## [1.4.0]

### Added

- playwright initialization logs
- translation metadata
- `Ozds.Assets` translation queries to get translation keys
- fix for getting meters with their types in `ReadByMeterIds`
- logic for chart to work with meters like it does with measurement locations
- check on filtering so it doesn't do anything if it doesn't need to

### Changed

- switch to nixpkgs 25.05
- removed usages of preprocessor directives in favor of runtime configuration
- switch to TUnit instead of xUnit
- switch to Testcontainers instead of GitHub services
- migrated time related functionality and testing into `Ozds.Time` and
  `Ozds.Time.Test` projects
- change most usages of `DateTimeOffset.UtcNow` in favor of `ClockService` from
  `Ozds.Time`
- `Ozds.Users` to use OIDC and LDAP in favor of OrchardCore authentication and
  user management
- `Ozds.Translation` arguments now use a list of assemblies to create
  cross-assembly translations
- `Ozds.Translation` by types now does grouping over class hierarchies and
  across assemblies
- `Ozds.Assets` localization queries translation caching
- `Ozds.Assets` localization queries key overrides from class hierarchies
- translations to better match business requirements

### Removed

- `Ozds.Assets` localization key queries
- gages for now so that the pages looked nicer

## [1.3.1] - 2025-06-05

### Added

- `Ozds.Client.Test` project for end-to-end testing of the client
- justfile recipe for setting up CI for the new `Ozds.Client.Test` project
- network user overview
- Column implementations for a bunch of models and interfaces
- tool tip logic for measurement selection dropdown
- text display logic for measurement selection dropdown
- all formatting methods for numbers and dates from `Ozds.Client` and
  `Ozds.Document` to `Ozds.Assets`
- MudBlazor overrides in ThemeStateProvider

### Changed

- `check` CI workflow to support new `Ozds.Client.Test` project
- `check` shell script to support new `Ozds.Client.Test` project
- test `.editorconfig` location
- fix analysis basis fetch for network users
- bumped `Xunit.DependencyInjection` version
- pagination logic
- list/card table view on mobile
- all paging to work with tables
- queries for filtering deleted content so you can see only deleted or only not
  deleted content
- all selection elements work on mobile with new SelectField component
- turned on detailed errors for blazor while debugging
- use proper navigation icons

## [1.3.0] - 2025-04-23

### Added

- A default section if chart has no parameters
- scope state provider in `Ozds.Client` to manage lifetime of scoped services
  per client session
- disposable base component in `Ozds.Client` for root components
- from date parameter update when measurement locations change on the chart
- `Ozds.Report` project for creating reports
- report models and entities and their activation and conversion
- report translations
- report queries for retrieving reports from `Ozds.Data`
- report mutations for creating reports via `Ozds.Report`
- hand-written csv serialization because `CsvHelper` decided to fight too much
  when trying to translate headers (deserialization still happens via
  `CsvHelper`)

### Changed

- get scoped services from cascading parameter rather than managing service
  scope inside `OzdsComponentBase`
- use `-dev` suffix for service bus endpoints in dev
- use `DisposableBaseComponent` as base for root components
- recursively unwrap conversions for member expression translations
- use fully qualified translation keys in `Ozds.Translation` and `Ozds.Assets`
- move `Ozds.Business.Queries.DocumentQueries` to
  `Ozds.Business.Mutations.DocumentMutations` because it will use blob storage
  for documents at some point
- `Ozds.Business.Queries.LocalizationQueries` to only have `Translate` and no
  `Key` methods so it can check with both a short key and a fully qualified key
- upload field uses new report mutations
- adjust download links to include culture for correct translation of report
  header

### Removed

- old brush chart code that didn't do anything
- download field
- everything regarding reports from `Ozds.Client`

## [1.2.1] - 2025-04-09

### Changed

- Made table content selectable
- Add more definition to the time stamp info for mouse hovering chart info
- Made charts at meter and measurement location pages normal charts not brushes

## [1.2.0] - 2025-04-08

### Added

- Initial analysis basis fetching in
  `Ozds.Data.Queries.MeasurementLocationQueries`
- `aggregate` buffer behavior that skips flushing measurements for faster
  seeding times
- 30 new meters and measurements locations in the dev database
- VS Code launches and tasks for new migration CLI and fake insert command
- Fake insert command that inserts fake data directly into the database
  circumventing the API that the old push and seed commands use
- Cloners in Ozds.Fake that clone generated data for a specific meter model for
  requested meters
- Meter identification class specifically for the Ozds.Fake project
- Optimize loaders in Ozds.Fake for batch generation
- Ozds.Fake workers that can be shared between services
- Ozds.Migration CLI tool that will be used from now on to migrate the database
  manually to latest and to generate database function migrations
- Conversion and aggregation functions that optimize for batch processing using
  enumerables
- MigrationMutations and MigrationQueries to all projects that need migrations
  and added a MigrationService in Ozds.Business that prevents the site from
  running if any migrations are pending
- JsonParameter that allows for passing JSON objects to database functions via
  Dapper
- functions in ExpressionExtensions in Ozds.Data that drive the new database
  function builders
- migration that stores the new function-based measurement mutations in the
  database
- Ozds.Data.Procedures namespace that is responsible for building and compiling
  SQL for creating, deleting and calling function-based queries/mutations
- migration justfile command that uses the new Ozds.Migration CLI tool
- GetStartOfMonthLastYear function and tests and apply where needed
- substituters to Raspberry PI nixos configuration
- initial `rzls` (Razor language server) for better devex
- invoice approval fields on network users and network user invoice states and
  their migrations
- add saga state machine states `Approved` and `Disapproved` for network user
  invoice state machine in `Ozds.Fake` and `Ozds.Messaging`
- `Approved` network user invoice state message `Ozds.Fake` and `Ozds.Messaging`
- `StatefulNetworkUserInvoiceModel` composite model that contains the invoice
  and its state
- boolean fields for edit and details components
- invoice creation number on network user page
- network user invoice state on network user invoice page
- measurement location options for chart
- tooltips for chart options
- `Ozds.Translation` project that handles translating the whole solution via
  regex matches and reflection
- ollama docker container, dev shell app, and `isready.nu` script
- `Ozds.Assets` project that handles all assets currently used for localization
  and fetching embedded document assets like fonts and images
- Translated the whole solution into the new `en.xml` and `hr.xml` files in
  hopes that XML is easier to edit than JSON for translations
- justfile commands for translating the whole solution and for running a single
  instance of the `Ozds.Translation` project

### Changed

- Progressively fetch analysis bases in `AnalysisStateProvider`
- Financial queries so they mirror the interface of measurement queries
- Refactored Ozds.Fake namespaces that implement the Visitor pattern to better
  align with the rest of the project
- Refactored Ozds.Fake Program.cs and IServiceCollection extensions to inject as
  many Ozds services as possible for future-proofing and for current use cases
- Measurement generation methods in Ozds.Fake that optimize for batch generation
  using IAsyncEnumerable
- Refactored Ozds.Fake services to optimize for batch generation
- Refactored MeasurementMutations to use database functions
- Refactored all IServiceCollectionExtensions to only depend on
  IServiceCollection and use the proper configure options pattern for
  configuration
- Move NetworkUserInvoiceIssuer to Ozds.Business.Mutations to keep the
  Ozds.Business.Finance namespace pure and contained only to financial
  calculations
- Adjusted the Ozds.Business.Mutations.MeasurementMutations to the new
  Ozds.Data.Mutations.MeasurementMutations
- Move all DataDbContext stuff to the Ozds.Data.Context namespace as some of
  that stuff uses internal EF Core API
- Adjusted aggregate entity types to use the new Ozds.Data.Procedures.Builders
- Adjusted DapperCommand function to support new function-based measurement
  mutations
- Simplify Ozds.Business.Test and Ozds.Data.Test Startup.cs to use their own
  appsettings.json
- Fix bug in GetStartOfQuarterHour that accounts for Croatian clock rewind on
  27.10.
- Fix vpn connection function
- Flake update
- Fix location selection sending users to location details
- Fix `LocationStateProvider` not fetching locations after exit from location
- Use the new [`deps.json`] instead of the obsolete `deps.nix`
- Fix playwright browser dependency
- Fix scripts requiring timescale docker id/name by fetching the id/name from
  docker
- calculation financial remark to nullable as it should have been
- extract messaging part away from the `NetworkUserInvoiceIssuer` into a reactor
- send invoice email only when it was approved
- Chart control layout
- the way chart fetches data by changing parameters
- starting chart settings
- `Ozds.Fake` service namespaces
- Use the new `Ozds.Assets` in `Ozds.Fake` and `Ozds.Migration`
- Extracted all localization into the new `Ozds.Assets` because it was actually
  needed in three different projects - `Ozds.Business`, `Ozds.Document` and
  `Ozds.Client`
- Adjusted affected projects by localization to use the new localization from
  `Ozds.Assets`
- Adjusted model component fields to use the new localization
- Adjusted calculation item name translation in `Ozds.Document` to use
  hard-coded acronyms where appropriate
- Added the new `Ozds.Assets` into DI container
- Slight `CONTRIBUTING.md` changes
- `format-and-deps` workflow to `generate` and use `just translate` to
  automatically translate the solution on each PR

### Removed

- Leftover analysis queries and tests
- Deleted all IApplicationBuilderExtensions as they were empty because I thought
  we would use them at some point

## [1.1.1] - 2025-03-20

### Added

- HTML remark fields on financials, network users, network user measurement
  locations
- `HtmlSanitizer` via `HtmlSanitizer` package to sanitize HTML fields
- Invoice preview functionality to `INetworkUserInvoiceIssuer`
- New `GetMonthRange` overload that takes a year and month
- `NetworkUserValidator` and `NetworkUserMeasurementLocationValidator` that
  sanitize the new remark fields
- Invoice preview picker to `NetworkUserPage`
- `NetworkUserInvoicePreviewDocumentPage` that previews documents via
  `INetworkUserInvoiceIssuer` and `DocumentQueries`
- Links to view and download invoices on `NetworkUserInvoicePage`
- `HtmlField` on edit components via WYSIWYG `Quill.JS`-based
  `Tizzani.MudBlazor.HtmlEditor` editor
- `HtmlField` on details components via `MarkupString`
- Remarks on network user invoice documents
- Support for collocated JS scripts in `AppController`
- `DownloadController` for network user invoices (previews)
- Migration to use correct column types for numeric values
- `DerivedAggregateMeasureModel` and `DerivedAggregateMeasureEntity` that better
  represent derived power measures on aggregate measurements
- Added new primitive conversion functions for floats and longs
- `IRecord` for all the Records that need to be converted from Entities
- `ModelRecordConversion` for the conversion pipeline
- All the needed implementations for the many Records added
- `CsvExporter` so models / record / aggregates can be converted to csv files
- `ReadMeasurementLocationByNetworkUser` and `ReadMeasurementLocationByLocation`
  needed for the download of aggregates
- `MudDatePicker` components to `LocationPage`, `NetworkUserPage`,
  `MeasurementLocationPage` and `MeterPage` for selecting dates for extraction
- Export to all the above mentioned pages

### Changed

- `Ozds.Business.Queries.DocumentQueries` API to accept
  `CalculatedNetworkUserInvoiceModel` rather than network user invoice id
- `ModelValidator` now always validates via the builtin ASP.NET Core validation
- Moved `link` tags to `ThemeStateProvider` since the `Ozds.Server` shouldn't
  care about the different libraries present
- Removed `MudBlazor` from `ErrorBoundary`
- Changed types of numeric properties in `Ozds.Data` to more precise ones
  (`decimal(19, 4)` for monetary values, `bigint` for cumulative measurement
  values, `float` for instantaneous measurement values)
- Fix support for collocated JS scripts in `AppController`

### Removed

- leftover print components

## [1.0.1] - 2025-03-07

## Changed

- Fix `quarter_hour_count` edge cases for daily, monthly aggregate mutations
- Made buffer hint optional
- Pass `CancellationToken.None` to `BeforeStopAsync` reactor handler for now
- Rollback measurement transactions only when a transaction is present
- Separate `check` workflow into `formant-and-deps` and `check` workflows to
  allow `auto-commit-action` to trigger `check` workflow re-runs

## Added

- Build caching

## [1.0.0] - 2025-03-05

### Added

- init

[1.4.2]: https://github.com/altibiz/ozds/compare/1.4.1...1.4.2
[1.4.1]: https://github.com/altibiz/ozds/compare/1.4.0...1.4.1
[1.4.0]: https://github.com/altibiz/ozds/compare/1.3.1...1.4.0
[1.3.1]: https://github.com/altibiz/ozds/compare/1.3.0...1.3.1
[1.3.0]: https://github.com/altibiz/ozds/compare/1.2.1...1.3.0
[1.2.1]: https://github.com/altibiz/ozds/compare/1.2.0...1.2.1
[1.2.0]: https://github.com/altibiz/ozds/compare/1.1.1...1.2.0
[1.1.1]: https://github.com/altibiz/ozds/compare/1.0.1...1.1.1
[1.0.1]: https://github.com/altibiz/ozds/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/altibiz/ozds/releases/tag/1.0.0
[`deps.json`]:
  https://github.com/NixOS/nixpkgs/blob/master/doc/languages-frameworks/dotnet.section.md#generating-and-updating-nuget-dependencies
