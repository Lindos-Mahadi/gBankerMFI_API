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
        public async Task<List<ProductListForSavingSummary>> GetProductListForSavingAccount(int porductType, int orgId, string itemType, int officeId)
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

        public async Task<List<RepaymentScheduleReportF>> GetRepaymentScheduleF(int officeId, int memberId, int productId, int loanTerm)
        {
            return await storedProcedureRepository.GetRepaymentScheduleF(officeId, memberId, productId, loanTerm);
        }

        public async Task<List<VillageList>> GetVillageListByUnion(string SearchByCode)
        {
            return await storedProcedureRepository.GetVillageListByUnion(SearchByCode);
        }

        public async Task<List<UnionList>> GetUnionListByUpozilla(string SearchByCode)
        {
            return await storedProcedureRepository.GetUnionListByUpozilla(SearchByCode);
        }

        public async Task<IEnumerable<DistrictList>> GetAllDistrict()
        {
            return await storedProcedureRepository.GetAllDistrict();
        }

        public async Task<List<LoanLedger>> GetLoanLedger(string officeId, string loanee1, string loanee2, string productId, string qType)
        {
            return await storedProcedureRepository.GetLoanLedger(officeId, loanee1, loanee2, productId, qType);
        }
        public async Task<List<SavingLedger>> GetSavingLedger(string officeId, string loanee1, string loanee2, string productId, string qType)
        {
            return await storedProcedureRepository.GetSavingLedger(officeId, loanee1, loanee2, productId, qType);
        }
    }
}
