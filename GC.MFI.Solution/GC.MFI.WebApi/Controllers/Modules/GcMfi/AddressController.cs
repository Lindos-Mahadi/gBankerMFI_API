using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IStoredProcedureService _storedProcedureService;
        private readonly IOfficeService _service;
        public AddressController(ILogger<AddressController> logger,
            IStoredProcedureService storedProcedureService,
            IOfficeService service)
        {
            _storedProcedureService = storedProcedureService;
            this._logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("getdistrictbydivisionid")]
        public async Task<IEnumerable<DistrictList>> GetDistrictByDivisionId(string divisionId)
        {
            var districts = await _storedProcedureService.GetDistrictByDivision(divisionId);
            return districts;
        }

        [HttpGet]
        [Route("getdivisionlistbycountry")]
        public async Task<List<Division>> GetDivisionListByCountry(string? countryId)
        {
            try
            {
                return await _storedProcedureService.GetDivisionByCountry(countryId);
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
        [HttpGet]
        [Route("getpostofficecodeList")]
        public IActionResult GetPostOfficeCodeList(string postOffice, string postCode)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PostOffice.json");

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                string jsonData = System.IO.File.ReadAllText(filePath);
                var locationDataList = JsonConvert.DeserializeObject<PostOfficeViewModel[]>(jsonData);

                if (!String.IsNullOrEmpty(postOffice))
                {
                    locationDataList = locationDataList?.Where(d => postOffice.Contains(d.postOffice) || d.postOffice.Trim().Replace(" ", "").ToUpper()!.Contains(postOffice.Trim().Replace(" ", "").ToUpper())).ToArray();
                }

                if (!String.IsNullOrEmpty(postCode))
                {

                    locationDataList = locationDataList?.Where(d => postCode.Contains(d.postCode) || d.postCode.Trim().Replace(" ", "").ToUpper()!.Contains(postCode.Trim().Replace(" ", "").ToUpper())).ToArray();
                }

                var result = locationDataList?.Select(d => new PostOfficeViewModel
                {
                    postCode = d.postCode,
                    postOffice = d.postOffice
                }).Take(10).ToArray();

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("getbydistrictId")]
        public async Task<IEnumerable<UpozillaList>> GetByDistrictId(string Id)
        {
            try
            {
                var record = await _storedProcedureService.GetUpozillaByDistrict(Id);
                return record;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }



        [HttpGet]
        [Route("getvillageListbyunion")]
        public async Task<IEnumerable<VillageList>> GetVillageListByUnion(string SearchByCode)
        {
            try
            {
                var record = await _storedProcedureService.GetVillageListByUnion(SearchByCode);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("getunionListbyupozilla")]
        public async Task<IEnumerable<UnionList>> GetUnionListByUpozilla(string SearchByCode)
        {
            try
            {
                var record = await _storedProcedureService.GetUnionListByUpozilla(SearchByCode);
                return record;
            }
            catch (Exception ex)
            {
                //LogError(ex, null);
                throw ex;
            }
        }

        [HttpGet]
        [Route("getofficebyunionId")]
        public async Task<IEnumerable<Office>> getOfficeByUnionId(int unionId)
        {
            var office = await _service.GetOfficeByUnionId(unionId);
            return office;
        }

        #region Error Handling
        protected void LogError(Exception ex, ViewModelBase viewModel)
        {
            //Log error and return customized error request...

        }
        #endregion
    }
}
