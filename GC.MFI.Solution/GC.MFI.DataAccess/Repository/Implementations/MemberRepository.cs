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
    public class MemberRepository : IMemberRepository
    {
        private readonly GBankerDbContext _context;
        public MemberRepository(GBankerDbContext context)
        {
            this._context = context;
        }

        public Member Create(Member mModel)
        {
            _context.Add(mModel);
            _context.SaveChanges();
            return mModel;
        }

        public void Delete(long id)
        {
            _context.Remove(id);
        }

        public async Task<IEnumerable<Member>> GetAll(string search)
        {
            if(!string.IsNullOrEmpty(search))
            {
                var memberList = _context.Member.Where(t => t.MemberCode!.Contains(search));
                return memberList.Skip(0).Take(10);
            }
            return _context.Member.Skip(0).Take(10);
        }

        public Member GetById(long id)
        {
            return _context.Member.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Member mModel)
        {
            _context.Attach(mModel);
            _context.Entry(mModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
