using CertGerador.Interfaces;
using Spire.Doc;

namespace CertGerador.Services;

public class SpireDocumentConverter : IDocumentConverter
{
    public void ConvertToPdf(string sourcePath, string destinationPath)
    {
        using var doc = new Document();
        doc.LoadFromFile(sourcePath);

        var options = new ToPdfParameterList
        {
            IsEmbeddedAllFonts = true,
            DisableLink = true,
        };
        doc.JPEGQuality = 100;

        doc.SaveToFile(destinationPath, options);
    }
}