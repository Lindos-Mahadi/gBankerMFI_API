using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;
        public CountryService(ICountryRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Country>> GetAll(string search)
        {
            var countryList = await _repository.GetAll(search);
            return countryList;
        }
    }
}
