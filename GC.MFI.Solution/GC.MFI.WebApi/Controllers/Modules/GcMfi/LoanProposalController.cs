﻿
using GC.MFI.Models.DbModels;
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


        public LoanProposalController(
            ILogger<LoanProposalController> logger,
            IStoredProcedureService storedProcedureService,
            IPurposeService purposeService
            )
        {
            _logger= logger;
            _storedProcedureService = storedProcedureService; 
            _purposeService= purposeService;
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
        [HttpGet]
        [Route("GetProductList")]
        public async Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int officeId)
        {
            try
            {
                return await _storedProcedureService.GetProductList(MainProductCode, freq, officeId);
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
        [Route("GetAllPurposeByName")]
        public async Task<IEnumerable<Purpose>> GetAllPurposeByName(string? search)
        {
            var purposeList = await _purposeService.GetAllPurpose(search);
            return purposeList;
        }
    }
}