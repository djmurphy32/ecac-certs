namespace Ecac.Certs.Api.Models;

public record ParsedSpreadsheet
{
    public required List<CandidatesModel> Candidates { get; init; }

    public record CandidatesModel 
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string CandidateNumber {get; init; }
        public required DateOnly IssueDate { get; init; }
    }
}