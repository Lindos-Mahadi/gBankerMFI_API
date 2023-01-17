using GC.MFI.DataAccess;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/GcMfi/Division")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly ILogger<DivisionController> _logger;
        private readonly GBankerDbContext _context;
        private readonly IDivisionService _divisionService;

        public DivisionController(
            IDivisionService divisionService,
            ILogger<DivisionController> logger, 
            GBankerDbContext context)
        {
            _divisionService = divisionService;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<List<Division>> GetDivisionList(string? countryId)
        {
            try
            {
                return await _divisionService.GetDivisionByCountry(countryId);
                // List<Division> divisionList = new List<Division> {
                //                    new Division() { DivisionCode = "1", DivisionName = "1 Barishal" },
                //                    new Division() { DivisionCode = "2", DivisionName = "2 Chattogram" },
                //                    new Division() { DivisionCode = "3", DivisionName = "3 Dhaka" },
                //                    new Division() { DivisionCode = "4", DivisionName = "4 Khulna" },
                //                    new Division() { DivisionCode = "5", DivisionName = "5 Rajshahi" },
                //                    new Division() { DivisionCode = "6", DivisionName = "6 Sylhet" },
                //                    new Division() { DivisionCode = "7", DivisionName = "7 Rangpur" },
                //                    new Division() { DivisionCode = "8", DivisionName = "8 Mymensingh" }
                //                    };
                //if(!String.IsNullOrEmpty(search))
                //{
                //    var list = divisionList.Where(t=> t.DivisionName.ToUpper()!.Contains(search.ToUpper()));
                //    return list.ToList();
                //}
                //return divisionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
