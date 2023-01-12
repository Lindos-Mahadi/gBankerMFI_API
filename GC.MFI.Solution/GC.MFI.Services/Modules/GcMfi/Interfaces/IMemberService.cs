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
        IEnumerable<Member> GetAll();
        //Task<IEnumerable<Member>> GetAll();
        Member GetById(long id);
        Member Create(Member objectToCreate);
        void Update(Member objectToUpdate);
        void Delete(long id);
        bool Inactivate(long id, DateTime? inactiveDate);
        bool IsContinued(long id);
        void Save();
    }
}
