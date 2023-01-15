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
        public OfficeRepository( GBankerDbContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Office>> GetAll()
        {
            var getOffice = _context.Office.AsEnumerable();
            return getOffice;
        }
    }
}
