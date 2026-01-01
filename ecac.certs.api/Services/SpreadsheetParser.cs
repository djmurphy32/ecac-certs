using ClosedXML.Excel;
using Ecac.Certs.Api.Models;

namespace Ecac.Certs.Api.Services;

public class SpreadsheetParser : ISpreadsheetParser
{
    private const int StartRow = 3;
    private const int CandidateNumberCol = 1;
    private const int FirstNameCol = 2;
    private const int LastNameCol = 3;
    private const int IssueDateCol = 4;
    public ParsedSpreadsheet Parse(CertsRequest request)
    {
        using var workbook = new XLWorkbook(request.File.OpenReadStream());
        var worksheet = workbook.Worksheets.First();
        return ParseRows(worksheet, request.FromDate);
    }

    private ParsedSpreadsheet ParseRows(IXLWorksheet worksheet, DateOnly fromDate)
    {
        var endRow = worksheet.RowsUsed().Last().RowNumber();
        List<ParsedSpreadsheet.CandidatesModel> candidates = [];
        
        for (var i = StartRow; i <= endRow; i++)
        {
            var issueDateString = worksheet.Cell(i, IssueDateCol).GetString();
            if (string.IsNullOrEmpty(issueDateString))
            {
                continue;
            }
            var issueDate = DateOnly.FromDateTime(worksheet.Cell(i, IssueDateCol).GetDateTime());
            if (issueDate < fromDate)
            {
                continue;
            }
            
            candidates.Add(new ParsedSpreadsheet.CandidatesModel
            {
                IssueDate = issueDate,
                CandidateNumber = worksheet.Cell(i, CandidateNumberCol).GetString(),
                FirstName = worksheet.Cell(i, FirstNameCol).GetString(),
                LastName = worksheet.Cell(i, LastNameCol).GetString(),
            });
        }

        return new ParsedSpreadsheet
        {
            Candidates = candidates,
        };
    }
}