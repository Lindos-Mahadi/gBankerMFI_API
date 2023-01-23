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
    public class StoredProcedureService: IStoredProcedureService
    {
        private readonly IStoredProcedureRepository divisionRepository;

        public StoredProcedureService(IStoredProcedureRepository divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        public async Task<List<Division>> GetDivisionByCountry(string countryId)
        {
            return await divisionRepository.GetDivisionByCountry(countryId);
        }

        public async Task<List<Center>> GetCenterListByOffice(int officeId)
        {
            return await divisionRepository.GetCenterListByOffice(officeId);
        }

        public async Task<List<MainProduct>> GetMainProductList(string PaymentFrequecy, int officeId)
        {
            return await divisionRepository.GetMainProductList(PaymentFrequecy, officeId);
        }

        public async Task<List<SubMainProduct>> GetSubMainProdutList(string MainProductCode, string freq)
        {
            return await divisionRepository.GetSubMainProdutList(MainProductCode, freq);
        }

        //public async Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int officeId)
        //{
        //    return await divisionRepository.GetProductList(MainProductCode, freq, officeId);
        //}
        public async Task<List<ProductList>> GetProductList(string freq, int officeId)
        {
            return await divisionRepository.GetProductList(freq, officeId);
        }
    }
}
