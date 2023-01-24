using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IStoredProcedureRepository
    {
        Task<List<Division>> GetDivisionByCountry(string countryId);

        Task<List<Center>> GetCenterListByOffice(int officeId);

        Task<List<MainProduct>> GetMainProductList(string PaymentFrequecy, int officeId);

        Task<List<SubMainProduct>> GetSubMainProdutList(string MainProductCode, string freq);
        //Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int officeId);
        Task<List<ProductList>> GetProductList(string freq, int officeId);
        Task<List<ProductList>> GetProductListForSavingAccount(int porductType,int orgId, string itemType,int officeId);
    }
}
