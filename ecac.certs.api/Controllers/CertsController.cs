using Ecac.Certs.Api.Models;
using Ecac.Certs.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecac.Certs.Api.Controllers;

[Route("api/certs")]
[ApiController]
public class CertsController(ISpreadsheetParser spreadsheetParser) : Controller
{
    
    [HttpPost("create")]
    public IActionResult Create([FromForm] CertsRequest request)
    {
        var model = spreadsheetParser.Parse(request);
        return Json(model);
        // var response = employeeEmployeeEmployeeEmployeeTimesheetProcessor.ProcessTimesheet(request.File);
        //
        // return response.Match<IActionResult>(
        //     results =>
        //     {
        //         using var stream = new MemoryStream();
        //         return File(results.WorkbookBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //             results.Name);
        //     },
        //     failReason =>
        //     {
        //         return failReason.FailureReason switch
        //         {
        //             ProcessingFailureReasons.UnsupportedFileType => BadRequest(
        //                 failReason.Message),
        //             _ => throw new Exception("Unhandled fail reason getting results")
        //         };
        //     });
    }
}