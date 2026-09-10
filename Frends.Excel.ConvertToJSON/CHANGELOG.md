# Changelog

## [3.0.0] - 2026-09-02
### Changed
- [Breaking Change] Replaced `ErrorMessage` (string) in the result object with a structured `Error` object containing `Message` and `AdditionalInfo`.
- Added standard failure handling options `ErrorMessageOnFailure`.
- Updated the task to target .NET 8.

## [2.2.0] - 2026-01-22
### Changed
- Open Excel files with FileAccess.Read to support read-only files
 
## [2.1.0] - 2024-08-21
### Changed
- Updated the Newtonsoft.Json package to the latest version.

## [2.0.0] - 2023-03-21
### Changed
- Change how the Json is constructed.
- Added CancellationToken to the Task.
- Refactored code to be more simple.
- Added two helper classes Row and Cell.

## [1.0.1] - 2022-08-02
### Changed
- Updated the documentation with better descriptions and examples.

### Fixed
- Nuget package should not contain the FrendsTasksMetadata.json files.

## [1.0.0] - 2022-03-03
### Added
- Initial implementation