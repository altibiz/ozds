<!-- markdownlint-disable MD024 -->

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

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

### Changed

- `Ozds.Business.Queries.DocumentQueries` API to accept
  `CalculatedNetworkUserInvoiceModel` rather than network user invoice id
- `ModelValidator` now always validates via the builtin ASP.NET Core validation
- Moved `link` tags to `ThemeStateProvider` since the `Ozds.Server` shouldn't
  care about the different libraries present
- Removed `MudBlazor` from `ErrorBoundary`
- Layout issues on network user invoice PDF-s

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

[1.0.1]: https://github.com/altibiz/ozds/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/altibiz/ozds/releases/tag/1.0.0
