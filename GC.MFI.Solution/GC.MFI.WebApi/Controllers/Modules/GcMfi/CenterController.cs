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
    [Route("api/GcMfi/Center")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ILogger<CenterController> _logger;
        private readonly GBankerDbContext _context;
        private readonly IStoredProcedureService _CenterService;

        public CenterController(
            IStoredProcedureService CenterService,
            ILogger<CenterController> logger, 
            GBankerDbContext context)
        {
            _CenterService = CenterService;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [Route("getCenterlistbycountry")]
        public async Task<List<Center>> GetCenterByOffice(int officeId)
        {
            try
            {
                return await _CenterService.GetCenterListByOffice(officeId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
