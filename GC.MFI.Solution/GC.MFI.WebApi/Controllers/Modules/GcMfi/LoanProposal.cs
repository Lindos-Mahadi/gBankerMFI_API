
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/member")]
    [ApiController]
    public class LoanProposalController : ControllerBase
    {
        private readonly ILogger<LoanProposalController> _logger;
        private readonly IStoredProcedureService _storedProcedureService;

        public LoanProposalController(
            ILogger<LoanProposalController> logger,
            IStoredProcedureService storedProcedureService)
        {
            _logger= logger;
            _storedProcedureService = storedProcedureService; 
        }
        
        [HttpGet]
        [Route("GetFrequency")]
        public List<dynamic> GetFrequency()
        {
            try
            {
                var frequency = new List<dynamic>
                {
                    new
                    {
                        Text = "Weekly",
                        Value = "W"
                    },
                    new
                    {
                        Text = "Monthly",
                        Value = "M"

                    },
                    new
                    {
                        Text = "Fortnightly",
                        Value = "F"

                    },
                    new
                    {
                        Text = "Daily",
                        Value = "D"

                    }
                };
                return frequency;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetMainProductList")]
        public async Task<List<MainProduct>> GetMainProductList(string FreqId, int OfficeId)
        {
            try
            {
                return await _storedProcedureService.GetMainProductList(FreqId, OfficeId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
