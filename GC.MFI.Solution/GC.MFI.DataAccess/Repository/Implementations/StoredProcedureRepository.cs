using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.MFI.Models.Models;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly GBankerDbContext _context;
        public StoredProcedureRepository(GBankerDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Division>> GetDivisionByCountry(string countryId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@SearchByCode", countryId));
                parameter.Add(new SqlParameter("@SearchBy", "con"));
                parameter.Add(new SqlParameter("@SearchType", "div"));

                var result = await Task.Run(() => _context.Division
               .FromSqlRaw(@"exec Proc_GetLocationList @SearchByCode, @SearchBy, @SearchType", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DistrictList>> GetAllDistrict()
        {
            try
            {

                var result = await Task.Run(() => _context.DistrictList
               .FromSqlRaw(@"exec Proc_GetAllDistricts"));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DistrictList>> GetDistrictByDivision(string divisionId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@SearchByCode", divisionId));
                parameter.Add(new SqlParameter("@SearchBy", "div"));
                parameter.Add(new SqlParameter("@SearchType", "dis"));

                var result = await Task.Run(() => _context.DistrictList
               .FromSqlRaw(@"exec Proc_GetLocationList @SearchByCode, @SearchBy, @SearchType", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UpozillaList>> GetUpozillaByDistrict(string districtId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@SearchByCode", districtId));
                parameter.Add(new SqlParameter("@SearchBy", "dis"));
                parameter.Add(new SqlParameter("@SearchType", "upo"));

                var result = await Task.Run(() => _context.UpozillaList
               .FromSqlRaw(@"exec Proc_GetLocationList @SearchByCode, @SearchBy, @SearchType", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Center>> GetCenterListByOffice(int OfficeId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeId", OfficeId));

                var result = await Task.Run(() => _context.Center
                .FromSqlRaw(@"exec GetOnlyCenter @OfficeId", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<MainProduct>> GetMainProductList(string PaymentFrequecy, int officeId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@PaymentFrq", PaymentFrequecy));
                parameter.Add(new SqlParameter("@OfficeID", officeId));

                var result = await Task.Run(() => _context.MainProduct
                .FromSqlRaw(@"exec getMainProductListAccordingToOffice @PaymentFrq, @OfficeID", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<SubMainProduct>> GetSubMainProdutList(string MainProductCode, string freq)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@MainProductCode", MainProductCode));
                parameter.Add(new SqlParameter("@freq", freq));

                var result = await Task.Run(() => _context.SubMainProduct
                .FromSqlRaw(@"exec getSubMainProductList @MainProductCode, @freq", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductList>> GetProductList(string freq, int OfficeID)
        {
            //try
            //{
            //    var parameter = new List<SqlParameter>();
            //    parameter.Add(new SqlParameter("@MainProductCode", MainProductCode));
            //    parameter.Add(new SqlParameter("@freq", freq));
            //    parameter.Add(new SqlParameter("@OfficeID", OfficeID));

            //    var result = await Task.Run(() => _context.ProductList
            //    .FromSqlRaw(@"exec getSubMainProductListTAccordingTOOffice @MainProductCode, @freq, @OfficeID", parameter.ToArray()));

            //    return result.ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@freq", freq));
                parameter.Add(new SqlParameter("@OfficeID", OfficeID));

                var result = await Task.Run(() => _context.ProductList
                .FromSqlRaw(@"exec getProductListSubMainAccordingTOOffice @freq, @OfficeID", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductListForSavingSummary>> GetProductListForSavingAccount(int porductType, int orgId, string itemType, int officeId)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Prodtype", porductType));
            parameter.Add(new SqlParameter("@OrgID", orgId));
            parameter.Add(new SqlParameter("@ItemType", itemType));
            parameter.Add(new SqlParameter("@OfficeID", officeId));

            var result = await Task.Run(() => _context.ProductListForSavingSummary
            .FromSqlRaw(@"exec Proc_GetProductAccordingtoOfficeForSavingAccount @Prodtype, @OrgID, @ItemType, @OfficeID", parameter.ToArray()));
            return result.ToList();
        }

        public async Task<List<RepaymentScheduleReportAE>> GetRepaymentScheduleAE(int officeID, int memberId, int productId, int loanTerm)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeId", officeID));
                parameter.Add(new SqlParameter("@MemberID", memberId));
                parameter.Add(new SqlParameter("@ProductID", productId));
                parameter.Add(new SqlParameter("@Loanterm", loanTerm));

                var result = await Task.Run(() => _context.RepaymentScheduleReportAE
                .FromSqlRaw(@"exec getRepaymentScheduleReportHistory @OfficeId, @MemberID, @ProductID, @loanTerm", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<RepaymentScheduleReportD>> GetRepaymentScheduleD(int officeID, int memberId, int productId, int loanTerm)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeId", officeID));
                parameter.Add(new SqlParameter("@MemberID", memberId));
                parameter.Add(new SqlParameter("@ProductID", productId));
                parameter.Add(new SqlParameter("@Loanterm", loanTerm));

                var result = await Task.Run(() => _context.RepaymentScheduleReportD
                .FromSqlRaw(@"exec getRepaymentScheduleReportHistory @OfficeId, @MemberID, @ProductID, @loanTerm", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<RepaymentScheduleReportF>> GetRepaymentScheduleF(int officeID, int memberId, int productId, int loanTerm)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeId", officeID));
                parameter.Add(new SqlParameter("@MemberID", memberId));
                parameter.Add(new SqlParameter("@ProductID", productId));
                parameter.Add(new SqlParameter("@Loanterm", loanTerm));

                var result = await Task.Run(() => _context.RepaymentScheduleReportF
                .FromSqlRaw(@"exec getRepaymentScheduleReportHistory @OfficeId, @MemberID, @ProductID, @loanTerm", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<VillageList>> GetVillageListByUnion(string SearchByCode)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@SearchByCode", SearchByCode));
                parameter.Add(new SqlParameter("@SearchBy", "uni"));
                parameter.Add(new SqlParameter("@SearchType", "vil"));

                var result = await Task.Run(() => _context.VillageList
               .FromSqlRaw(@"exec Proc_GetLocationList @SearchByCode, @SearchBy, @SearchType", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnionList>> GetUnionListByUpozilla(string SearchByCode)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@SearchByCode", SearchByCode));
                parameter.Add(new SqlParameter("@SearchBy", "upo"));
                parameter.Add(new SqlParameter("@SearchType", "uni"));

                var result = await Task.Run(() => _context.UnionList
               .FromSqlRaw(@"exec Proc_GetLocationList @SearchByCode, @SearchBy, @SearchType", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<LoanLedger>> GetLoanLedger(string officeId, string loanee1, string loanee2, string productId, string qType)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeID", officeId));
                parameter.Add(new SqlParameter("@LoaneeNo1", loanee1));
                parameter.Add(new SqlParameter("@LoaneeNo2", loanee2));
                parameter.Add(new SqlParameter("@ProductID", productId));
                parameter.Add(new SqlParameter("@Qtype", qType));

                var result = await Task.Run(() => _context.LoanLedger
               .FromSqlRaw(@"exec Proc_GetRpt_LoanLedger @OfficeID, @LoaneeNo1, @LoaneeNo2, @ProductID, @Qtype", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<SavingLedger>> GetSavingLedger(string officeId, string loanee1, string loanee2, string productId, string qType)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeID", officeId));
                parameter.Add(new SqlParameter("@LoaneeNo1", loanee1));
                parameter.Add(new SqlParameter("@LoaneeNo2", loanee2));
                parameter.Add(new SqlParameter("@ProductID", productId));
                parameter.Add(new SqlParameter("@Qtype", qType));

                var result = await Task.Run(() => _context.SavingLedger
               .FromSqlRaw(@"exec Proc_GetRpt_SavingsLedger  @OfficeID, @LoaneeNo1, @LoaneeNo2, @ProductID, @Qtype", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
