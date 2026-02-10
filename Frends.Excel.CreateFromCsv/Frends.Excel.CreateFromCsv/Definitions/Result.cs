namespace Frends.Excel.CreateFromCsv.Definitions;

/// <summary>
/// Result of the task.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; init; } = true;

    /// <summary>
    /// Path to the output file.
    /// </summary>
    /// <example>C:/results/MyNewData.xlsx</example>
    public string OutputPath { get; init; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; init; }
}
