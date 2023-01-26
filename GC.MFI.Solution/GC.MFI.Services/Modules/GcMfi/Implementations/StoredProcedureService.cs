using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
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
        private readonly IStoredProcedureRepository storedProcedureRepository;

        public StoredProcedureService(IStoredProcedureRepository storedProcedureRepository)
        {
            this.storedProcedureRepository = storedProcedureRepository;
        }

        public async Task<List<Division>> GetDivisionByCountry(string countryId)
        {
            return await storedProcedureRepository.GetDivisionByCountry(countryId);
        }

        public async Task<List<Center>> GetCenterListByOffice(int officeId)
        {
            return await storedProcedureRepository.GetCenterListByOffice(officeId);
        }

        public async Task<List<MainProduct>> GetMainProductList(string PaymentFrequecy, int officeId)
        {
            return await storedProcedureRepository.GetMainProductList(PaymentFrequecy, officeId);
        }

        public async Task<List<SubMainProduct>> GetSubMainProdutList(string MainProductCode, string freq)
        {
            return await storedProcedureRepository.GetSubMainProdutList(MainProductCode, freq);
        }

        //public async Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int officeId)
        //{
        //    return await storedProcedureRepository.GetProductList(MainProductCode, freq, officeId);
        //}
        public async Task<List<ProductList>> GetProductList(string freq, int officeId)
        {
            return await storedProcedureRepository.GetProductList(freq, officeId);
        }

        public async Task<List<DistrictList>> GetDistrictByDivision(string divisionId)
        {
            return await storedProcedureRepository.GetDistrictByDivision(divisionId);
        }
        public async Task<List<UpozillaList>> GetUpozillaByDistrict(string districtId)
        {
            return await storedProcedureRepository.GetUpozillaByDistrict(districtId);
        }
        public async Task<List<ProductList>> GetProductListForSavingAccount(int porductType, int orgId, string itemType, int officeId)
        {
            return await storedProcedureRepository.GetProductListForSavingAccount(porductType, orgId, itemType, officeId);
        }

        public async Task<List<RepaymentScheduleReportAE>> GetRepaymentScheduleAE(int officeId, int memberId, int productId, int loanTerm)
        {
            return await storedProcedureRepository.GetRepaymentScheduleAE(officeId, memberId, productId, loanTerm);
        }
        public async Task<List<RepaymentScheduleReportD>> GetRepaymentScheduleD(int officeId, int memberId, int productId, int loanTerm)
        {
            return await storedProcedureRepository.GetRepaymentScheduleD(officeId, memberId, productId, loanTerm);
        }
    }
}
