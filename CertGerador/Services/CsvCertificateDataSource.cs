using System.Globalization;
using CertGerador.Interfaces;
using CertGerador.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CertGerador.Services;

public class CsvCertificateDataSource : ICertificateDataSource
{
    public IReadOnlyList<CertificateRecord> Read(string csvPath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";",
        };

        using var reader = new StreamReader(csvPath, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        using var csv = new CsvReader(reader, config);

        if (!csv.Read())
            throw new InvalidOperationException("O arquivo CSV está vazio.");

        csv.ReadHeader();
        var headers = csv.HeaderRecord ?? throw new InvalidOperationException("Não foi possível ler o cabeçalho do CSV.");

        var records = new List<CertificateRecord>();

        while (csv.Read())
        {
            var fields = new Dictionary<string, string>();
            foreach (var header in headers)
                fields[header] = csv.GetField(header) ?? string.Empty;

            records.Add(new CertificateRecord { Fields = fields });
        }

        return records;
    }
}