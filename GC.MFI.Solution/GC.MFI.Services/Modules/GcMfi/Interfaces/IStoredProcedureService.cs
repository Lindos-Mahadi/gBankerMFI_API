﻿using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
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
        Task<List<SubMainProduct>> GetSubMainProdutList(string MainProductCode, string freq);
        //Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int officeId);
        Task<List<ProductList>> GetProductList(string freq, int officeId);

        Task<List<ProductListForSavingSummary>> GetProductListForSavingAccount(int porductType, int orgId, string itemType, int officeId);
        Task<List<RepaymentScheduleReportAE>> GetRepaymentScheduleAE(int officeID, int memberId, int productId, int loanTerm);
        Task<List<RepaymentScheduleReportD>> GetRepaymentScheduleD(int officeID, int memberId, int productId, int loanTerm);
        Task<List<RepaymentScheduleReportF>> GetRepaymentScheduleF(int officeID, int memberId, int productId, int loanTerm);
        Task<List<DistrictList>> GetDistrictByDivision(string divisionId);
        Task<List<UpozillaList>> GetUpozillaByDistrict(string districtId);
        Task<List<VillageList>> GetVillageListByUnion(string SearchByCode);
        Task<List<UnionList>> GetUnionListByUpozilla(string SearchByCode);
       Task<IEnumerable<DistrictList>> GetAllDistrict();
        Task<List<LoanLedger>> GetLoanLedger(string officeId, string loanee1, string loanee2, string productId, string qType);
        Task<List<SavingLedger>> GetSavingLedger(string officeId, string loanee1, string loanee2, string productId, string qType);
    }
}
