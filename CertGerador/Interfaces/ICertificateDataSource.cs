using CertGerador.Models;

namespace CertGerador.Interfaces;

public interface ICertificateDataSource
{
    IReadOnlyList<CertificateRecord> Read(string csvPath);
}