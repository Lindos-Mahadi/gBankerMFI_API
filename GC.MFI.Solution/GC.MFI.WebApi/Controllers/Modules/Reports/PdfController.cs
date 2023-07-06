using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.Reports;

[Route("[controller]")]
public class PdfController : Controller
{
    private readonly IStoredProcedureService _storedProcedureService;

    public PdfController(IStoredProcedureService storedProcedureService)
    {
        _storedProcedureService = storedProcedureService;
    }
    // GET
    public async Task<IActionResult> Index(string officeId, string loanee1, string loanee2, string productId, string qType)
    {
        var result= await _storedProcedureService.GetSavingLedger(officeId, loanee1, loanee2, productId, qType);
        return View(result);
    }
}