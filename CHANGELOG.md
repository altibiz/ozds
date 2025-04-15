<!-- markdownlint-disable MD024 -->

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

### Added

- scope state provider in `Ozds.Client` to manage lifetime of scoped services
  per client session
- disposable base component in `Ozds.Client` for root components

### Changed

- get scoped services from cascading parameter rather than managing service
  scope inside `OzdsComponentBase`
- use `-dev` suffix for service bus endpoints in dev
- use `DisposableBaseComponent` as base for root components
- recursively unwrap conversions for member expression translations

## [1.2.1]

### Changed

- Made table content selectable
- Add more definition to the time stamp info for mouse hovering chart info
- Made charts at meter and measurement location pages normal charts not brushes

## [1.2.0]

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

## [1.1.1]

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

[1.2.1]: https://github.com/altibiz/ozds/compare/1.2.0...1.2.1
[1.2.0]: https://github.com/altibiz/ozds/compare/1.1.1...1.2.0
[1.1.1]: https://github.com/altibiz/ozds/compare/1.0.1...1.1.1
[1.0.1]: https://github.com/altibiz/ozds/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/altibiz/ozds/releases/tag/1.0.0
[`deps.json`]:
  https://github.com/NixOS/nixpkgs/blob/master/doc/languages-frameworks/dotnet.section.md#generating-and-updating-nuget-dependencies
