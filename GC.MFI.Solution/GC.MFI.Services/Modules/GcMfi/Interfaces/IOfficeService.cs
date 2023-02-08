using GC.MFI.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IOfficeService
    {
        Task<IEnumerable<Office>> GetAll(string search);
        Task<IEnumerable<Office>> GetOfficeByUnionId(int unionId);
    }
}
