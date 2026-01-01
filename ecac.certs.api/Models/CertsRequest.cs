namespace Ecac.Certs.Api.Models;

public record CertsRequest
{
    public required IFormFile File { get; init; } 
    public required DateOnly FromDate { get; init; }
}