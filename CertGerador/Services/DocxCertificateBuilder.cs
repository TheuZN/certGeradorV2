using CertGerador.Interfaces;
using CertGerador.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CertGerador.Services;

public class DocxCertificateBuilder : ICertificateDocumentBuilder
{
    public string Build(string templatePath, CertificateRecord record, string outputFolder, string fileName)
    {
        var outputPath = Path.Combine(outputFolder, $"{fileName}.docx");
        File.Copy(templatePath, outputPath, overwrite: true);

        using (var doc = WordprocessingDocument.Open(outputPath, isEditable: true))
        {
            var body = doc.MainDocumentPart?.Document?.Body
                ?? throw new InvalidOperationException("Template inválido: corpo do documento não encontrado.");

            foreach (var paragraph in body.Descendants<Paragraph>())
            {
                ReplacePlaceholdersInParagraph(paragraph, record);
            }

            doc.MainDocumentPart!.Document.Save();
        }

        return outputPath;
    }

    private static void ReplacePlaceholdersInParagraph(Paragraph paragraph, CertificateRecord record)
    {
        var runs = paragraph.Elements<Run>().ToList();
        if (runs.Count == 0) return;

        // Junta o texto de todos os runs do parágrafo
        var fullText = string.Concat(runs.Select(r => r.InnerText));

        var replaced = fullText;
        foreach (var (key, value) in record.Fields)
        {
            replaced = replaced.Replace(key, value);
        }

        if (replaced == fullText) return; // nada mudou, não mexe no XML

        // Remove todos os Text existentes e concentra o resultado no primeiro run,
        // preservando a formatação (RunProperties) desse primeiro run
        var firstRun = runs[0];
        var textElement = firstRun.GetFirstChild<Text>();
        if (textElement is null)
        {
            textElement = new Text();
            firstRun.AppendChild(textElement);
        }
        textElement.Text = replaced;
        textElement.Space = DocumentFormat.OpenXml.SpaceProcessingModeValues.Preserve;

        // Limpa o texto dos runs seguintes (mantém a formatação/estrutura, só esvazia o conteúdo)
        for (int i = 1; i < runs.Count; i++)
        {
            var t = runs[i].GetFirstChild<Text>();
            if (t is not null) t.Text = string.Empty;
        }
    }
}