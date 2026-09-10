using CertGerador.Models;

namespace CertGerador.Interfaces;

public interface ICertificateDocumentBuilder
{
    // Gera o .docx preenchido a partir do template + dados de uma linha, devolve o caminho gerado
    string Build(string templatePath, CertificateRecord record, string outputFolder, string fileName);
}