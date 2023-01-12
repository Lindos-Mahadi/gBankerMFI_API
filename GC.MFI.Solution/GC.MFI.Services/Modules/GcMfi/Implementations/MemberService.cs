using GC.MFI.DataAccess;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly BntPOSContext _context;

        public MemberService(BntPOSContext context)
        {
            _context = context;
        }

        public Member Create(Member objectToCreate)
        {
            _context.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Delete(long id)
        {
            var d = _context.Member.Find(id);
            _context.Remove(d);
        }

        public IEnumerable<Member> GetAll()
        {
            var getMember = _context.Member.AsEnumerable();
            return getMember;
        }

        public Member GetById(long id)
        {
            return _context.Member.Find(id);
        }

        public bool Inactivate(long id, DateTime? inactiveDate)
        {
            throw new NotImplementedException();
        }

        public bool IsContinued(long id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Member objectToUpdate)
        {
            _context.Update(objectToUpdate);
        }
    }
}
