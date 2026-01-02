using Ecac.Certs.Api.Models;
using Ecac.Certs.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecac.Certs.Api.Controllers;

[Route("api/certs")]
[ApiController]
public class CertsController(
    ISpreadsheetParser spreadsheetParser,
    ICertGenerator certGenerator) : Controller
{
    
    [HttpPost("create")]
    public IActionResult Create([FromForm] CertsRequest request)
    {
        var parsedSpreadsheet = spreadsheetParser.Parse(request);

        var docBytes = certGenerator.CreateCert(parsedSpreadsheet);
        return File(docBytes, "application/zip", "Certs.zip");
    }
}