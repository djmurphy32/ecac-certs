using Ecac.Certs.Api.Models;

namespace Ecac.Certs.Api.Services;

public interface ICertGenerator
{
    byte[] CreateCert(ParsedSpreadsheet parsedSpreadsheet);
}