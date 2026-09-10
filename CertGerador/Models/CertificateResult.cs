namespace CertGerador.Models;

public class CertificateResult
{
    public required bool Success { get; init; }
    public string? OutputPath { get; init; }
    public string? ErrorMessage { get; init; }

    public static CertificateResult Ok(string path) => new() { Success = true, OutputPath = path };
    public static CertificateResult Fail(string error) => new() { Success = false, ErrorMessage = error };
}