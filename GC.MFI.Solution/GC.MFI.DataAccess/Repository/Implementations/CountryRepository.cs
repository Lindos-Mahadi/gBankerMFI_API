using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class CountryRepository : ICountryRepository
    {
        private readonly GBankerDbContext _context;
        public CountryRepository(GBankerDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Country>> GetAll(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var countryList = _context.Country.Where(t => t.CountryName!.Contains(search) || t.CountryName.Trim().Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
                return countryList;
            }
            return _context.Country.AsEnumerable();
        }
    }
}
