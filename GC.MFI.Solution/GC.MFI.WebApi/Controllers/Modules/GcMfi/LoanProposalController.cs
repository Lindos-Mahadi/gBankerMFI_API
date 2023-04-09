
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Implementations;
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
        private readonly IPurposeService _purposeService;
        private readonly IProductService _productService;


        public LoanProposalController(
            ILogger<LoanProposalController> logger,
            IStoredProcedureService storedProcedureService,
            IPurposeService purposeService,
            IProductService productService
            )
        {
            _logger= logger;
            _storedProcedureService = storedProcedureService; 
            _purposeService= purposeService;
            _productService= productService;
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
        [Route("GetSubMainProductList")]
        public async Task<List<SubMainProduct>> GetSubMainProductList(string MainProductCode, string freq)
        {
            try
            {
                return await _storedProcedureService.GetSubMainProdutList(MainProductCode, freq);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //[HttpGet]
        //[Route("GetProductList")]
        //public async Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int officeId)
        //{
        //    try
        //    {
        //        return await _storedProcedureService.GetProductList(MainProductCode, freq, officeId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        [HttpGet]
        [Route("GetProductList")]
        public async Task<List<ProductList>> GetProductList(string freq, int officeId)
        {
            try
            {
                return await _storedProcedureService.GetProductList(freq, officeId);
            }
            catch (Exception ex)
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

        [HttpGet]
        [Route("GetLoanRepaymentScheduleAE")]
        public async Task<List<RepaymentScheduleReportAE>> GetLoanRepaymentSchedule(
            int officeId,
            int memberId,
            int productId,
            int loanTerm)
        {
            try
            {
                return await _storedProcedureService.GetRepaymentScheduleAE(officeId, memberId, productId, loanTerm);
            }
            catch(Exception ex) 
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetLoanRepaymentScheduleD")]
        public async Task<List<RepaymentScheduleReportD>> GetLoanRepaymentScheduleD(
           int officeId,
           int memberId,
           int productId,
           int loanTerm)
        {
            try
            {
                return await _storedProcedureService.GetRepaymentScheduleD(officeId, memberId, productId, loanTerm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetLoanRepaymentScheduleF")]
        public async Task<List<RepaymentScheduleReportF>> GetLoanRepaymentScheduleF(
          int officeId,
          int memberId,
          int productId,
          int loanTerm)
        {
            try
            {
                return await _storedProcedureService.GetRepaymentScheduleF(officeId, memberId, productId, loanTerm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetLoanLedger")]
        public async Task<List<LoanLedger>> GetLoanLedger(
           string officeId, 
           string loanee1, 
           string loanee2, 
           string productId, 
           string qType)
        {
            try
            {
                return await _storedProcedureService.GetLoanLedger(officeId, loanee1, loanee2, productId, qType);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllPurposeByName")]
        public async Task<IEnumerable<Purpose>> GetAllPurposeByName(string? search)
        {
            var purposeList = await _purposeService.GetAllPurpose(search);
            return purposeList;
        }
    }
}
