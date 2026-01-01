using Ecac.Certs.Api.Models;

namespace Ecac.Certs.Api.Services;

public interface ISpreadsheetParser
{
    ParsedSpreadsheet Parse(CertsRequest request);
}