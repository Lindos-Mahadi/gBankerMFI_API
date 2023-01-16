using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _service;
        public CountryController(ILogger<CountryController> logger, ICountryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Country>> GetAll(string? search)
        {
            try
            {
                var countryList = await _service.GetAll(search);
                return countryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}