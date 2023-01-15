using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using GC.MFI.DataAccess.InfrastructureBase;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IMemberRepository 
    {
        Task<IEnumerable<Member>> GetAll();
        //IEnumerable<Member> GetAll();
        Member GetById(long id);
        Member Create(Member mModel);
        void Update(Member mModel);
        void Delete(long id);
        void Save();
    }
}
