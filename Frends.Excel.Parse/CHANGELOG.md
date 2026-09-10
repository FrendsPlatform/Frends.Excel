# Changelog

## [2.0.0] - 2026-09-02
### Changed
- [Breaking Change] Replaced `ErrorMessage` (string) in the result object with a structured `Error` object containing `Message` and `AdditionalInfo`.
- Added standard failure handling options `ErrorMessageOnFailure`.
- Updated the task to target .NET 8.

### Fixed
- Added standardized task error handling and documentation for task inputs, options, and results.

## [1.2.0] - 2026-02-23
### Fixed
- Ensure FrendsTaskMetadata.json is included in NuGet package

## [1.1.0] - 2026-01-22
### Fixed
- Open Excel files with FileAccess.Read to support read-only files

## [1.0.0] - 2022-03-17
### Added
- Initial implementation