# Changelog

## [2.0.0] - 2026-09-02
### Changed
- [Breaking Change] Replaced `ErrorMessage` (string) in the result object with a structured `Error` object containing `Message` and `AdditionalInfo`.
- Added standard failure handling options `ErrorMessageOnFailure`.
- Updated the task to target .NET 8.


## [1.1.0] - 2026-01-22
### Fixed
- Open Excel files with FileAccess.Read to support read-only files

## [1.0.2] - 2023-08-01
### Fixed
- Fixed Options.ShouldReadSheet method to check if the Sheet name equals to the given name.

## [1.0.1] - 2022-08-05
### Change
- Documentation updates

## [1.0.0] - 2022-02-24
### Added
- Initial implementation