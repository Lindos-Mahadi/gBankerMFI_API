using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.Reports;

[Route("[controller]")]
public class PdfController : Controller
{
    private readonly IStoredProcedureService _storedProcedureService;
    private readonly IProductService _productService;

    public PdfController(IStoredProcedureService storedProcedureService, IProductService productService)
    {
        _storedProcedureService = storedProcedureService;
        _productService = productService;
    }
    // GET
    [HttpGet("saving")]
    public async Task<IActionResult> Index(string officeId, string loanee1, string loanee2, string productId, string qType)
    {
        var result= await _storedProcedureService.GetSavingLedger(officeId, loanee1, loanee2, productId, qType);
        return View(result);
    }

    [HttpGet("loan")]
    public async Task<IActionResult> RepaymentScheduleReport(int officeID, int memberId, int productId, int loanTerm)
    {
        short id = Convert.ToInt16(productId);
        var getProduct = _productService.GetByIdShort(id);
        switch (getProduct.InterestCalculationMethod)
        {
            case "A":
                var res = await _storedProcedureService.GetRepaymentScheduleAE(officeID, memberId, productId, loanTerm);
                return View(res);
            case "D":
                var res1 = await _storedProcedureService.GetRepaymentScheduleD(officeID, memberId, productId, loanTerm);
                return View(res1);
            case "F":
                var res3 = await _storedProcedureService.GetRepaymentScheduleF(officeID, memberId, productId, loanTerm);
                return View(res3);
            default:
                return null;

        }
       // var result = await _storedProcedureService.GetRepaymentScheduleF(officeID, memberId, productId, loanTerm);
       // return View(result);
    }

}