namespace CertGerador.Models;

public class CertificateRecord
{
    // Todos os campos do CSV, chave = nome da coluna, valor = conteúdo da linha
    public Dictionary<string, string> Fields { get; init; } = new();

    public string? Get(string fieldName) =>
        Fields.TryGetValue(fieldName, out var value) ? value : null;
}