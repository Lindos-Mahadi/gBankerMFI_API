using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/GcMfi/Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IStoredProcedureService _storedProcedureService;

        public AddressController(IStoredProcedureService storedProcedureService)
        {
            _storedProcedureService = storedProcedureService;
        }

        [HttpGet]
        [Route("GetVillageListByUnion")]
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
        [Route("GetUnionListByUpozilla")]
        public async Task<IEnumerable<UnionList>> GetUnionListByUpozilla(string SearchByCode)
        {
            try
            {
                var record = await _storedProcedureService.GetUnionListByUpozilla(SearchByCode);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
