using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAll(string search);
        Member GetById(long id);
        Member Create(Member mModel);
        void Update(Member mModel);
        void Delete(long id);
        void Save();
    }
}
