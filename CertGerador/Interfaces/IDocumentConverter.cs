namespace CertGerador.Interfaces;

public interface IDocumentConverter
{
    void ConvertToPdf(string sourcePath, string destinationPath);
}