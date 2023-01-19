using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IStoredProcedureService
    {
        Task<List<Division>> GetDivisionByCountry(string countryId);
        Task<List<Center>> GetCenterListByOffice(int officeId);
        Task<List<MainProduct>> GetMainProductList(string PaymentFrequecy, int officeId);
    }
}
