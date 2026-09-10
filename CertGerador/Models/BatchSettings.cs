namespace CertGerador.Models;

public class BatchSettings
{
    public required string CsvPath { get; init; }
    public required string TemplatePath { get; init; }
    public required string OutputFolder { get; init; }
    public required string FileNameField { get; init; } 
}