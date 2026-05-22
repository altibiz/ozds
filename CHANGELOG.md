<!-- markdownlint-disable MD024 -->

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and adheres to [Semantic Versioning](https://semver.org/).

## Unreleased

### Changed

- `SearchableSelectField<T>` now combines its dropdown list with `MudVirtualize`
  instead of a `MudList` + `@foreach`, so dropdowns with thousands of items
  render only the visible window plus an additional buffer of 6 rows for more
  smooth transitions without loading
- `SearchableSelectField<T>` filtered-items cache now stores
  `List<(int Index, T Value)>` tuples directly, removing the per-render list
  projection in the razor
- internal text field now adds `AutoFocus` parameter so the user can start
  typing as soon as the dropdown opens
- `.ozds-searchable-select__list-wrap` now sets `overscroll-behavior: none` so
  wheel scrolling inside the dropdown no longer chains to the page beneath
- `SearchableSelectField<T>` keyboard scroll alignment no longer relies on
  `document.getElementById` against per-item ids, since `MudVirtualize` is used,
  now it uses fixed list item heights times index of the next highlighted item
- `SearchableSelectField<T>` now inherits `OzdsComponentBase` instead of
  implementing `IAsyncDisposable` directly

## [1.8.5] - 2026-05-13

### Changed

- Refactored `ShouldClamp` helper in `MeasurementProcedureCompiler` to check
  positively for floating-point CLR types (`decimal`, `float`, `double`) instead
  of negatively excluding the eight integer types (`byte`, `sbyte`, `short`,
  `ushort`, `int`, `uint`, `long`, `ulong`) for improved readability
- `GroupedColumn` on `OzdsColumnsComponentBase` no longer takes a top-level
  `searchable` flag and is no longer generic; sub-columns are now passed as
  `GroupedSubColumn` items via the new `SubColumn(...)` helpers, each with its
  own optional `searchable` flag and an optional separate label expression
- `MeterAnalysisColumns` and `MeasurementLocationAnalysisColumns` now render
  T1/T2 active energy sub-columns through `SubColumn(...)` with 2-decimal
  formatting via `NumericString(..., 2)` for display while keeping the raw
  decimal expression as the label source
- `MeasurementLineChart` reorders its conditional rendering to check for an
  empty selection (no measurement locations and no meters) first and otherwise
  renders the `ApexChart`, relying on ApexCharts' built-in no-data state when
  `Measurements.Items` is empty; the previous `if (items.Count == 0)` fallback
  in `GetItems()` has been removed, and the `Series` fallback to
  `Parameters.Measurements.Items` when no meter/location is supplied has been
  dropped (the empty-selection branch makes that case unreachable)
- `MeasurementChartHeader` now displays a translated empty-state message in
  place of the measure/unit/time-span text when no measurement location or meter
  is selected
- `MeasurementChartControls` disables the Measure, Phase, Multiplier,
  Resolution, and Refresh fields when no measurement location or meter is
  selected via a local `isNothingSelected` flag
- `MeasurementChartControls` now uses the new `SearchableSelectField` instead of
  plain `MudSelect` for both meters and measurement locations, with consistent
  `MudTooltip` wrappers showing the full list of current selections; the
  `OnMeasurementLocationsChanged` and `OnMetersChanged` callbacks now receive
  `IMeasurementLocation`/`IMeter` instances directly instead of resolving them
  from string ids
- `MeasurementLineChart` wraps the chart and its empty-state placeholder in a
  fixed-height container so chart re-renders no longer shift the chart-control
  selection fields above it
- `MudTooltip` overlays in `MeasurementChartControls` no longer block pointer
  events on the underlying fields; added a global
  `.mud-tooltip { pointer-events: none; }` rule and set `ShowOnFocus="false"` on
  the measure, phase, and multiplier tooltips so they no longer intercept clicks
  or steal focus
- `SelectField` dropdown no longer opens and immediately closes when clicked;
  removed the wrapper `<div @onpointerup="() => _inner.OpenMenu()">` and the
  `MudSelect _inner` ref that double-toggled the menu against `MudSelect`'s own
  click handling

### Added

- `GroupedSubColumn` record on `OzdsColumnsComponentBase` carrying separate
  value and label expressions plus a per-sub-column `searchable` flag
- `SubColumn(...)` helper overloads on `OzdsColumnsComponentBase` for building
  `GroupedSubColumn` items either from a single expression or from a separate
  value/label expression pair
- `Disabled` parameter on `EnumPicker` and `MultiEnumPicker`, forwarded to the
  inner `SelectField` and propagated to MudBlazor's `MudSelect` via captured
  attributes
- Translations `No measurement locations/meters are selected for display` and
  `No measurement location or meter selected` in `en.xml` and `hr.xml`
- `SearchableSelectField<T>` component in `Ozds.Client.Components.Fields`, a
  custom dropdown built on `MudPopover`, `MudList`, and `MudTextField`,
  providing search-as-you-type filtering with a case-sensitivity toggle, single-
  and multi-selection over arbitrary `ICollection<T>` collections, optional
  `ItemTemplate`, full keyboard navigation (ArrowUp/Down/Home/End to move the
  highlight, Enter to select, Escape to close, Tab to swap focus between search
  and list), a highlighted-row state with primary-tinted background and
  auto-scroll-into-view, plus per-render caching of the filtered list
  (invalidated on search/case/`Items` changes) and a `HashSet`-backed
  selected-value lookup for O(n) multi-selection rendering
- Collocated `searchable-select-field.js` ES module loaded on first render via
  `IJSRuntime.InvokeAsync<IJSObjectReference>("import", ...)` and released on
  `IAsyncDisposable.DisposeAsync`, exposing a single `scrollItemIntoView(id)`
  helper that calls `scrollIntoView({ block: 'nearest', behavior: 'instant' })`

## [1.8.4] - 2026-04-24

### Changed

- Update, MailKit to newer version 4.16.0, all tests pass
- `MeasurementProcedureCompiler` upsert/derive/delta SQL builders
  (`UpsertAverage`, `UpsertMin`, `UpsertMax`, `DerivativePower`,
  `DeriveAverage`, `DeriveMin`, `DeriveMax`, `DeltaSum`, `DeltaMin`) now route
  through the new assignment helpers and apply near-zero clamping for
  floating-point columns only, leaving integer/long columns untouched

### Added

- Float/double underflow guards on measurement upsert procedures via a new
  `ClampNearZeroValues` helper in `MeasurementProcedureCompiler` that clamps
  aggregate values with absolute magnitude below `1e-6` to `0` to prevent
  underflow accumulation across upserts
- `AssignValue` and `SelectValue` helpers in `MeasurementProcedureCompiler` that
  wrap aggregate column expressions with optional clamping for cleaner procedure
  SQL generation
- `GetPropertyClrType` and `ShouldClamp` helpers in
  `MeasurementProcedureCompiler` that resolve a property's CLR type (including
  nested complex properties and nullables) and skip clamping for integer types
  (`byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`) so
  cumulative integer values are not clamped
- `UpsertUnderflowTest` in `Ozds.Data.Test` covering float/double underflow
  scenarios for measurement upsert procedures
- EF migration `AddedFloatUnderflowGuardsOnUpsertProcedures` regenerating
  measurement upsert procedures with the new clamping logic

## [1.8.3] - 2026-04-17

### Changed

- bumped MudBlazor version from 8.1.0 to 8.6.0
- moved MudBlazor, Tizzani HtmlEditor, Quill and Google Fonts Roboto
  `<link>`/`<script>` tags from `ThemeStateProvider.razor` into
  `_AppLayout.cshtml` so they load at the server layout level rather than inside
  the client theme provider
- Split single total active energy column in analysis views into two sub-columns
  showing higher tariff (T1) and lower tariff (T2) separately
- `MeterAnalysisColumns` and `MeasurementLocationAnalysisColumns` now use
  `GroupedColumn` with T1/T2 instead of single `ActiveEnergy_kWh` column
- `MeterAnalysisDetails` and `MeasurementLocationAnalysisDetails` now use
  `FieldSection` groups with T1/T2 fields instead of single energy field
- `FieldSection` title typography is now configurable via `TitleTypo` parameter
  (defaults to `Typo.h5`), title text wrapped in bold
- `Analyzer.AnalyzeConsumption` uses named parameters for readability
- Fix, Playwright `.local-browsers` folder missing from GitHub Actions artifact
  creating it without the dot prefix during publish and renaming it on startup
- Changed so that both `ozds-server.sh` and `ozds-server-dev.sh` scripts change
  `local-browsers` back to `.local-browsers`
- Fix, centralized `fromDate` recalculation into `Fetch` method with a
  `forcedRefresh` parameter, removing duplicated date calculation logic from
  `OnMeasurementLocationsChanged`,
  `OnMetersChanged`,`OnRefreshChanged`,`OnResolutionChanged`, and
  `OnMultiplierChanged`
- Optimized `BillingQueries` to group aggregates by measurement location ID
  upfront using a dictionary instead of filtering per-location in a loop
- Refactored `AggregateWindowQueries` report fetching to be more like
  `BillingQueries` by fetching next-boundary and blackout-location aggregates
  separately for more accurate energy card report calculations

### Added

- `ActiveEnergy_Tariff1_kWh` and `ActiveEnergy_Tariff2_kWh` fields on
  `Consumption` record, computed via `.TariffBinary().T1` / `.T2`
- `GroupedColumn<T>` method on `OzdsColumnsComponentBase` that renders multiple
  sub-columns under a shared header title using CSS grid layout
- Translations for grouped column headers and tariff sub-columns in both English
  and Croatian (`ActiveEnergyLastMonth`, `ActiveEnergyThisMonth`,
  `ActiveEnergy_Tariff1_kWh`, `ActiveEnergy_Tariff2_kWh`)

## [1.8.2] - 2026-04-03

### Changed

- Optimized `ReadReportBasesByLocation` EF query in `ReportQueries` to fetch
  network users directly by location foreign key
- Fix, `ApiV1Href` property on `OzdsComponentBase` is now just a string that
  points from root of url to the swagger API
- update mailkit
- updated sonar analyzer package
- updated azure identity package
- suppressed specific warnings because either they are new or .razor warning
  package suppression does not work
- added new script `./scripts/database/migrate-generated.nu` for creating
  migration specific to generated SQL code (for procedures and non EF stuff)
- Fix, `rollback.nu` missing db context fix
- `rollback.nu` script uses new where instead of deprecated filter
- Fix, missing mapping field in `EnergyCardModelReportEntityConverter` for field
  `ActiveEnergyTotalImportT0_kWh`
- Ozds.Caching.Test tests now have Repeat attribute which is used to catch more
  flaky non deterministic failures
- Removed polling program parts since Ozds.Caching.Test should now be
  deterministic
- Fix network energy card CSV/report queries to use proper aggregate window
  boundaries instead of raw measurement queries, matching invoice calculation
  behavior
- Refactored ReportQueries to delegate aggregate fetching to
  AggregateWindowQueries, eliminating duplicated mapping logic across
  ReadEnergyCardReportBasis, ReadEnergyCardReportBasisByNetworkUser, and
  ReadEnergyCardReportBasisByLocation
- In `MeasurementChartControls.cs` changed `Fetch` method to use new overloads
  which do not cut measurements with pagination
- Changed Meter details to show the measurement validator by its title/name
  instead of its identifier number
- Fix `ModelIValidator` method `Validate` for `IEnumerable` pass skips first
  element for validation
- Fix `ModelIValidator` omits default model check with `Validate` method and
  `ValidationContext`

### Added

- Added small button onto `MappedTable` which toggles case sensitive search (on
  by default)
- In `Table.cs` added private property and logic to define case sensitivity on
  table search
- add `ShortenedEnergyCardReportEntity` in Ozds.Report and
  `ShortenedEnergyCardReportModel` in Ozds.Entity
- energy report generator now uses shortened models for generating energy card
  reports
- add another button `Shortened energy card report (CSV)` with specific use for
  generating requested shortened version
- New AggregateWindowQueries class with optimized Dapper queries for fetching
  aggregate window boundaries by measurement location - New
  AggregateWindowBoundaryBasisEntity DTO for aggregate window query results
- Tests for ReadEnergyCardReportBasisByNetworkUser and ReadLoadCurveReportBasis
  aggregate window queries
- Add new unpaginated query methods in `MeasurementQueries` which are overloads
  of methods `ReadByMeterIds` and `ReadByMeasurementLocationIds`, they do not
  paginate and return back normal List
- Add `TitleLinkField` method component that displays a model's title instead of
  its raw identifier, with a loading fallback that shows the ID while the title
  loads

## [1.8.1] - 2026-02-26

### Removed

- duplicated change log left from .NET 10 rebase

### Added

- add extensions in business now have `PrimitiveRoundingExtensions` which
  contain methods `Round` which round and return double and decimal respectfully
- add sentinel injector which is used to overload specific values of invoice
  model or calculator to fail tests on use of wrong rounding method
- add every type of column can now register it's value mapper
- added parameter 'searchable' to define columns that can be searched on table

### Changed

- fix problem with duplicate inserts on tests in ApiV1
  `MeasurementControllerTest`
- fix filtering on API V1 test inserts by date so they match DB query result
- change all calculators and calculator tests for business now use rounding type
  `MidpointRounding.AwayFromZero` defined in `PrimitiveRoundingExtensions`
- fix mapped tables did not use toFilter for searching leading to using non
  mapped records without titles
- update dotnet repl tool

## [1.8.0] - 2025-02-19

### Added

- new specific test for API returning latest measurements by meter
- added new specific test for API returning latest measurements by meter

### Changed

- updated HtmlSanitizer to version 9.0.892
- fix ReadLastByMeasurementLocationIds now returns latest measurement info for
  all meters that have measurements
- fixed unintended reset of the selected measurement points or meters on each
  property change (for now it's local fix but it could be done so that the state
  and params of chart control persist after reloading the page)
- fix ReadLastByMeasurementLocationIds now returns latest measurement info for
  all meters that have measurements
- updated .NET version from 8 to 10
- updated Azure.Identity package
- replaced deprecated implementation on ServiceCryptography
- update System.Linq.Async package to version 7.0.0
- fixed problem with removed async functions from package System.Linq.Async on
  RegexService.cs and MeasurementMutations.cs
- fix more decimal places on Column definitions for Regulatory catalogue
- fix more decimal places on Column definitions for Network User catalogue
- use csharpier over jetbrains formatter

### Removed

- deprecated/unused packages and dependencies

## [1.7.1] - 2025-12-14

### Changed

- billing query optimization

## [1.7.0] - 2025-12-04

### Added

- blackout and metered network user calculation models, entities, and logic
- database migrations for new calculation types
- distinct UI components for blackout and metered calculation details
- document generation templates for blackout and metered calculations
- `InvoiceQueries` implementation for dedicated invoice fetching
- `PostgresContainer` and `TestInfrastructureFixture` for robust integration
  testing
- massive update to translation files (`en.xml`, `hr.xml`) covering new domain
  terms
- `IMeteredNetworkUserCalculation` abstraction

### Changed

- refactored `NetworkUserCalculation` architecture to support polymorphic
  implementations (metered vs blackout)
- updated `BillingQueries` to handle new calculation types and logic
- migrated test infrastructure to use containerized postgres instances
- split generic calculation model activators and converters into specific
  implementations
- updated invoice document generation to support new calculation types
- `NetworkUserInvoiceMutations` logic to accommodate new calculation bases

### Removed

- generic `NetworkUserCalculation` activators and converters in favor of
  specific ones
- `NetworkUserCalculationModel` base class (replaced by specific models)
- `INetworkUserCalculation` interface
- deprecated test factories (`MeasurementEntityFactory`,
  `OzdsDataTestContextFactory`)

## [1.6.1] - 2025-11-19

### Added

- `Ozds.Caching` project for cache implementation
- caching behavior to business queries
- separate reactor for cache entity deletion
- time state provider component extracted from culture state provider
- API for fetching quarter hourly aggregates by network user and measurement
  location
- `Ozds.Caching.Test` project for cache testing
- `System.Runtime.Caching` package for first `InMemoryCache` implementation

### Changed

- group common host behavior into generic `OzdsBusinessHost`
- invert order of magnitude multiplier
- split up component base class
- standardized culture serialization
- fix N+1 when sending network user invoice acknowledgements
- fix business conversion and activation tests

### Removed

- `AssetConstants` in favor of properties from `ICultureQueries`
- `TimeConstants` in favor of properties from `ITimeQueries`
- old translation files
- all classes in `Ozds.Business.Caching` in favor of the new `Ozds.Caching`
  project
- reactors that only did cache mutations
- `Ozds.Business` dependency from `Ozds.Data.Test`
- leftover uses of `CancellationToken.None`

## [1.6.0] - 2025-10-20

### Added

- API key, scopes, registers table, entities, models, components, pages
- `Ozds.Sdk` project with NSwag generated client for OZDS API V1
- API V1 related controllers in `/api/v1`
- `Trackable`/`Auditable` distinction
- auditing of join models
- `csharp-ls` for development
- `Model`/`Entity` reflection classes that play nicely with translation strings
- `ApiKeyManager` for hashing, verification and tokenization
- `ApiKeyAuthAttribute` for authentication via API key leaving controllers
  responsible for authorization
- `ModelUserEntityConverter` for user-related conversions
- password model and mutations for specific password-related actions
- password field
- polymorphic id field
- analysis state reset
- get time zone from user browser
- heading and title components with title state
- string, empty, nested fragment components
- mapping for streaming components
- password change in user page

### Changed

- All app-related controllers to `/app` and
- API key authentication to `Authorization` header with `Bearer` scheme
- Auditable -> Trackable
- old `ConcurrentDictionaryCacheBase` to new
  `BatchedConcurrentDictionaryCacheBase`
- old `ConcurrentDictionaryCacheBase` now doesn't do batching
- nicer `Empty`/`NotFound` components
- more explicit controls over streaming components
- add auditing events after mutation so that create ids match
- better test logging

### Removed

## [1.5.1] - 2025-08-06

### Added

- search functionality for Analysis and Identifiable's in Tables
- choosing page count

## [1.5.0] - 2025-07-28

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
- cleaned up some N + 1 reactors

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

[1.8.5]: https://github.com/altibiz/ozds/compare/1.8.4...1.8.5
[1.8.4]: https://github.com/altibiz/ozds/compare/1.8.3...1.8.4
[1.8.3]: https://github.com/altibiz/ozds/compare/1.8.2...1.8.3
[1.8.2]: https://github.com/altibiz/ozds/compare/1.8.1...1.8.2
[1.8.1]: https://github.com/altibiz/ozds/compare/1.8.0...1.8.1
[1.8.0]: https://github.com/altibiz/ozds/compare/1.7.1...1.8.0
[1.7.1]: https://github.com/altibiz/ozds/compare/1.7.0...1.7.1
[1.7.0]: https://github.com/altibiz/ozds/compare/1.6.1...1.7.0
[1.6.1]: https://github.com/altibiz/ozds/compare/1.6.0...1.6.1
[1.6.0]: https://github.com/altibiz/ozds/compare/1.5.1...1.6.0
[1.5.1]: https://github.com/altibiz/ozds/compare/1.5.0...1.5.1
[1.5.0]: https://github.com/altibiz/ozds/compare/1.4.2...1.5.0
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
