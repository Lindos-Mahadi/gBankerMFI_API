using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IOfficeRepository 
    {
        Task<IEnumerable<Office>> GetAll(string search);
        Task<IEnumerable<Office>> GetOfficeByUnionId(int unionId);
    }
}
