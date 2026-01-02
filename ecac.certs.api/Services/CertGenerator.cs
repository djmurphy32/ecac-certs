using System.IO.Compression;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Ecac.Certs.Api.Models;

namespace Ecac.Certs.Api.Services;

public class CertGenerator : ICertGenerator
{
    public byte[] CreateCert(ParsedSpreadsheet parsedSpreadsheet)
    {
        var zipStream = new MemoryStream();
        using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
        {
            parsedSpreadsheet.Candidates.ForEach(candidate =>
            {
                var docStream = CreateCandidateCertificate(candidate);
                var fileName = $"{candidate.FirstName} {candidate.LastName} - Flammable Refrigerants Cert.docx";
                
                var zipEntry = zip.CreateEntry(fileName, CompressionLevel.Optimal);
            
                using var entryStream = zipEntry.Open();
                docStream.Position = 0;
                
                docStream.CopyTo(entryStream);
            });
        }

        zipStream.Position = 0;
        return zipStream.ToArray();
    }

    private MemoryStream CreateCandidateCertificate(ParsedSpreadsheet.CandidatesModel candidate)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{{NAME}}", $"{candidate.FirstName} {candidate.LastName}" },
            { "{{CANDIDATE_NUMBER}}", candidate.CandidateNumber },
            { "{{ISSUE_DATE}}", candidate.IssueDate.ToString("dd/MM/yyyy") },
        };
        var stream = new MemoryStream();

        using (var fileStream = System.IO.File.OpenRead("Assets/Cert_Template.docx"))
        {
            fileStream.CopyTo(stream);
        }

        using (var doc = WordprocessingDocument.Open(stream, true))
        {
            var body = doc.MainDocumentPart!.Document.Body!;
            foreach (var text in body.Descendants<Text>())
            {
                foreach (var replacement in replacements)
                {
                    if (text.Text.Contains(replacement.Key))
                    {
                        text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                    }
                }
            }

            doc.MainDocumentPart.Document.Save();
        }

        stream.Position = 0;
        return stream;
    }
}