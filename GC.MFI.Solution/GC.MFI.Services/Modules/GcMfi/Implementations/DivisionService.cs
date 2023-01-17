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
    public class DivisionService: IDivisionService
    {
        private readonly IDivisionRepository divisionRepository;

        public DivisionService(IDivisionRepository divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        public async Task<List<Division>> GetDivisionByCountry(string countryId)
        {
            return await divisionRepository.GetDivisionByCountry(countryId);
        }
    }
}
