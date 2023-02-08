using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class OfficeRepository :  IOfficeRepository
    {
        private readonly GBankerDbContext _context;
        public OfficeRepository(GBankerDbContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Office>> GetAll(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
               var officeList =  _context.Office.Where(t=> t.OfficeCode!.Contains(search) || t.OfficeName.Trim().Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
               return officeList.Skip(0).Take(10);
            }
            return _context.Office.Skip(0).Take(10);
        }

        public async Task<IEnumerable<Office>> GetOfficeByUnionId(int unionId)
        {
            var getOffice = _context.Office.Where(t => t.UnionID == unionId);
            return getOffice;
        }
    }
}
