<!-- markdownlint-disable MD024 -->

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and adheres to [Semantic Versioning](https://semver.org/).

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
